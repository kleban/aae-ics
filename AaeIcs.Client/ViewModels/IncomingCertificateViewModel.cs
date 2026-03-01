using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using AAEICS.Client.Messages;
using AAEICS.Client.ViewModels.Components;
using AAEICS.Client.Views;
using AAEICS.Core.Contracts.Services;
using AAEICS.Core.DTO.Certificates;
using AAEICS.Core.DTO.General;
using AAEICS.Database.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
// Твої UI моделі
// Тільки для вказівки TEntity у дженериках
using IncomingCertificate = AAEICS.Client.Models.IncomingCertificate;
using IncomingCertificateLine = AAEICS.Client.Models.IncomingCertificateLine;

namespace AAEICS.Client.ViewModels;

public partial class IncomingCertificateViewModel : ObservableObject
{
    private bool _isSyncing;
    
    // Сервіси
    private readonly IIncomingCertificateService _incomingCertificateService;
    private readonly IDictionaryDataService _dataService;
    private readonly IFuzzySearchService<ReasonDTO> _reasonSearch;
    private readonly IFuzzySearchService<PersonnelDTO> _personnelSearch;
    private readonly IFuzzySearchService<TransferInstanceDTO> _donorSearch;
    private readonly IFuzzySearchService<TransferInstanceDTO> _recipientSearch;
    private readonly IFuzzySearchService<TransferInstanceDTO> _deliveryCompanySearch;
    private readonly IFuzzySearchService<MeasureUnitDTO> _measureUnitSearch;
    private readonly IFuzzySearchService<CategoryDTO> _categorySentSearch;
    private readonly IFuzzySearchService<CategoryDTO> _categoryReceivedSearch;

    // ==========================================
    // ВЛАСТИВОСТІ ДЛЯ UI (BINDING)
    // ==========================================
    [ObservableProperty]
    private IncomingCertificate _incomingCertificate = new();
    
    [ObservableProperty]
    private ObservableCollection<IncomingCertificateLine> _incomingCertificateLines = [];

    [ObservableProperty]
    private bool _isAllConfirmed;
    
    public SearchBoxViewModel<ReasonDTO> ReasonSearchBox { get; }
    public SearchBoxViewModel<PersonnelDTO> ApprovePersonSearchBox { get; }
    public SearchBoxViewModel<TransferInstanceDTO> DonorSearchBox { get; }
    public SearchBoxViewModel<TransferInstanceDTO> RecipientSearchBox { get; }
    public SearchBoxViewModel<TransferInstanceDTO> DeliveryCompanySearchBox { get; }

    // ==========================================
    // КОНСТРУКТОР
    // ==========================================
    public IncomingCertificateViewModel(
        IIncomingCertificateService incomingCertificateService,
        IDictionaryDataService dataService,
        IFuzzySearchService<ReasonDTO> reasonSearch,
        IFuzzySearchService<PersonnelDTO> personnelSearch,
        IFuzzySearchService<TransferInstanceDTO> donorSearch,
        IFuzzySearchService<TransferInstanceDTO> recipientSearch,
        IFuzzySearchService<TransferInstanceDTO> deliveryCompanySearch,
        IFuzzySearchService<MeasureUnitDTO> measureUnitSearch,
        IFuzzySearchService<CategoryDTO> categorySentSearch,
        IFuzzySearchService<CategoryDTO> categoryReceivedSearch)
    {
        _incomingCertificateService = incomingCertificateService;
        _dataService = dataService;
        _reasonSearch = reasonSearch;
        _personnelSearch = personnelSearch;
        _donorSearch = donorSearch;
        _recipientSearch = recipientSearch;
        _deliveryCompanySearch = deliveryCompanySearch;
        _measureUnitSearch = measureUnitSearch;
        _categorySentSearch = categorySentSearch;
        _categoryReceivedSearch = categoryReceivedSearch;

        // Ініціалізуємо UI компоненти пошуку
        ReasonSearchBox = new SearchBoxViewModel<ReasonDTO>(_reasonSearch, r => r.Name);
        ApprovePersonSearchBox = new SearchBoxViewModel<PersonnelDTO>(_personnelSearch, p => p.LastName, "LastName");
        DonorSearchBox = new SearchBoxViewModel<TransferInstanceDTO>(_donorSearch, d => d.Name);
        RecipientSearchBox = new SearchBoxViewModel<TransferInstanceDTO>(_recipientSearch, r => r.Name);
        DeliveryCompanySearchBox = new SearchBoxViewModel<TransferInstanceDTO>(_deliveryCompanySearch, d => d.Name);

        // Підписуємося на події зміни колекції рядків
        IncomingCertificateLines.CollectionChanged += OnIncomingCertificateLinesCollectionChanged;

        // Реєструємо месенджери для створення нових довідників через DynamicDialog
        WeakReferenceMessenger.Default.Register<CreateNewItemMessage<ReasonDTO>>(this, async (r, m) => 
            await HandleCreateNewItemAsync<Reason, ReasonDTO>(m, _reasonSearch));
            
        WeakReferenceMessenger.Default.Register<CreateNewItemMessage<PersonnelDTO>>(this, async (r, m) => 
            await HandleCreateNewItemAsync<Personnel, PersonnelDTO>(m, _personnelSearch));
        
        WeakReferenceMessenger.Default.Register<CreateNewItemMessage<MeasureUnitDTO>>(this, async (r, m) => 
            await HandleCreateNewItemAsync<MeasureUnit, MeasureUnitDTO>(m, _measureUnitSearch));
        
        WeakReferenceMessenger.Default.Register<CreateNewItemMessage<CategoryDTO>>(this, async (r, m) => 
            await HandleCreateNewItemAsync<Category, CategoryDTO>(m, _categorySentSearch));
        
        // WeakReferenceMessenger.Default.Register<CreateNewItemMessage<CategoryDTO>>(this, async (r, m) => 
        //     await HandleCreateNewItemAsync<Category, CategoryDTO>(m, _categoryReceivedSearch));
    }

    // ==========================================
    // ІНІЦІАЛІЗАЦІЯ (Викликати з View.Loaded)
    // ==========================================
    public async Task InitializeAsync()
    {
        // Завантажуємо дані для пошуковиків
        var reasons = await _dataService.GetAllDataAsync<Reason, ReasonDTO>();
        _reasonSearch.LoadData(reasons);

        var personnel = await _dataService.GetAllDataAsync<Personnel, PersonnelDTO>();
        _personnelSearch.LoadData(personnel);

        // Завантажуємо одиниці виміру для ComboBox у DataGrid
        var units = await _dataService.GetAllDataAsync<MeasureUnit, MeasureUnitDTO>();
        _measureUnitSearch.LoadData(units);
        
        var categories = await _dataService.GetAllDataAsync<Category, CategoryDTO>();
        _categorySentSearch.LoadData(categories);
        _categoryReceivedSearch.LoadData(categories);
        
        var instances = await _dataService.GetAllDataAsync<TransferInstance, TransferInstanceDTO>();
        _donorSearch.LoadData(instances);
        
        _recipientSearch.LoadData(instances);
        
        _deliveryCompanySearch.LoadData(instances);
    }

    // ==========================================
    // ЛОГІКА СТВОРЕННЯ НОВИХ ДОВІДНИКІВ
    // ==========================================
    private async Task HandleCreateNewItemAsync<TEntity, TDto>(
        CreateNewItemMessage<TDto> message, 
        IFuzzySearchService<TDto> searchService) 
        where TEntity : class 
        where TDto : class, new()
    {
        var newDto = new TDto(); 

        // Відкриваємо динамічний діалог
        var dialog = new DynamicDialog(newDto, _dataService);
        
        if (dialog.ShowDialog() == true) 
        {
            // Зберігаємо через DataService (без UnitOfWork напряму!)
            await _dataService.AddDataAsync<TEntity, TDto>(newDto);

            // Оновлюємо кеш пошуку і вибираємо створений елемент у UI
            searchService.AddItemToCache(newDto);
            message.OnItemCreated?.Invoke(newDto);
        }
    }

    // ==========================================
    // ЛОГІКА СИНХРОНІЗАЦІЇ ЧЕКБОКСІВ
    // ==========================================
    partial void OnIsAllConfirmedChanged(bool value)
    {
        if (_isSyncing) return;
        try
        {
            _isSyncing = true;
            foreach (var actLine in IncomingCertificateLines)
            {
                actLine.IsConfirmed = value;
            }
        }
        finally { _isSyncing = false; }
    }

    private void OnCertificateLinePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IncomingCertificateLine.IsConfirmed))
            CheckHeaderState();
    }

    private void CheckHeaderState()
    {
        if (_isSyncing || !IncomingCertificateLines.Any()) return;
        try
        {
            _isSyncing = true;
            IsAllConfirmed = IncomingCertificateLines.All(x => x.IsConfirmed);
        }
        finally { _isSyncing = false; }
    }

    private void OnIncomingCertificateLinesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
            foreach (IncomingCertificateLine item in e.NewItems)
                item.PropertyChanged += OnCertificateLinePropertyChanged;

        if (e.OldItems != null)
            foreach (IncomingCertificateLine item in e.OldItems)
                item.PropertyChanged -= OnCertificateLinePropertyChanged;

        CheckHeaderState();
    }

    // ==========================================
    // КОМАНДИ РЕДАГУВАННЯ ТАБЛИЦІ
    // ==========================================
    [RelayCommand]
    private void AddRow()
    {
// Створюємо новий рядок і передаємо йому сервіси для створення його власних SearchBoxViewModel
        var newLine = new IncomingCertificateLine(
            _measureUnitSearch, 
            _categorySentSearch, 
            _categoryReceivedSearch
        );
    
        IncomingCertificateLines.Add(newLine);
    }

    [RelayCommand]
    private void DeleteRow(IncomingCertificateLine actLine)
    {
        if (actLine != null)
            IncomingCertificateLines.Remove(actLine); 
    }

    // ==========================================
    // ГОЛОВНА КОМАНДА ЗБЕРЕЖЕННЯ
    // ==========================================
    [RelayCommand]
    private async Task AddCertificateAsync() 
    {
        // if (IncomingCertificate == null || ReasonSearchBox.SelectedItem == null || ApprovePersonSearchBox.SelectedItem == null)
        // {
        //     System.Windows.MessageBox.Show("Заповніть усі обов'язкові поля (Підстава, Особа)!");
        //     return;
        // }
    
        // 1. Формуємо DTO рядків
        var linesDto = IncomingCertificateLines.Select((line, index) => new IncomingCertificateLineDTO
        {
            Name = line.Name,
            NomenclatureCode = line.NomenclatureCode,
            BatchNumber = line.BatchNumber,
            OrdinalNumber = index + 1,
            // Створюємо MeasureUnitDTO тільки з ID, AutoMapper на бекенді зрозуміє, що робити
            MeasureUnit = line.MeasureUnitSearchBox.SelectedItem, 
            PricePerUnit = line.PricePerUnit,
            QuantitySent = line.QuantitySent,
            CategorySent = line.CategorySentSearchBox.SelectedItem,
            QuantityReceived = line.QuantityReceived,
            CategoryReceived = line.CategoryReceivedSearchBox.SelectedItem,
            Notes = line.Notes,
            MadeIn = line.MadeIn
        }).ToList();
    
        // 2. Формуємо головне DTO акту
        var incomingCertificateDTO = new IncomingCertificateDTO
        {
            Edrpou = IncomingCertificate.Edrpou,
            ApproveDate = IncomingCertificate.ApproveDate,
            RegistrationDate = IncomingCertificate.RegistrationDate,
            RegistrationPlace = IncomingCertificate.RegistrationPlace,
            TransferDateStart = IncomingCertificate.TransferDateStart,
            TransferDateEnd = IncomingCertificate.TransferDateEnd,
            Donor = DonorSearchBox.SelectedItem,
            Recipient = RecipientSearchBox.SelectedItem,
            DeliveryCompany = DeliveryCompanySearchBox.SelectedItem,
            
            Reason = ReasonSearchBox.SelectedItem,
            ApprovePerson = ApprovePersonSearchBox.SelectedItem, 
            
            IncomingCertificateLines = linesDto
        };
    
        try
        {
            // 3. Відправляємо на бекенд (Сервіс сам викличе репозиторій і UnitOfWork.CompleteAsync)
            var result = await _incomingCertificateService.AddIncomingCertificateAsync(incomingCertificateDTO);
            
            // Повідомляємо іншим частинам програми про успіх
            WeakReferenceMessenger.Default.Send(new NewCertificateMessage(result));
            
            // Очищення форми
            IncomingCertificate = new IncomingCertificate();
            IncomingCertificateLines.Clear();
            
            System.Windows.MessageBox.Show("Акт успішно збережено!", "Успіх", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            System.Windows.MessageBox.Show($"Сталася помилка при збереженні:\n{errorMessage}", "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }
}