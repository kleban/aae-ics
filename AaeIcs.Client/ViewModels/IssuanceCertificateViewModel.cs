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

using IssuanceCertificate = AAEICS.Client.Models.IssuanceCertificate;
using IssueCertificateLine = AAEICS.Client.Models.IssueCertificateLine;

namespace AAEICS.Client.ViewModels;

public partial class IssuanceCertificateViewModel : ObservableObject
{
    private bool _isSyncing;
    
    private readonly IIssuanceCertificateService _issuanceCertificateService;
    private readonly IDictionaryDataService _dataService;
    private readonly IFuzzySearchService<ReasonDTO> _reasonSearch;
    private readonly IFuzzySearchService<PersonnelDTO> _personnelSearch;
    private readonly IFuzzySearchService<TransferInstanceDTO> _donorSearch;
    private readonly IFuzzySearchService<TransferInstanceDTO> _recipientSearch;
    private readonly IFuzzySearchService<TransferInstanceDTO> _deliveryCompanySearch;
    private readonly IFuzzySearchService<MeasureUnitDTO> _measureUnitSearch;
    private readonly IFuzzySearchService<CategoryDTO> _categorySentSearch;
    private readonly IFuzzySearchService<CategoryDTO> _categoryReceivedSearch;
    
    [ObservableProperty]
    private IssuanceCertificate _issuanceCertificate = new();
    
    [ObservableProperty]
    private ObservableCollection<IssueCertificateLine> _issueCertificateLines = [];

    [ObservableProperty]
    private bool _isAllConfirmed;
    
    public SearchBoxViewModel<ReasonDTO> ReasonSearchBox { get; }
    public SearchBoxViewModel<PersonnelDTO> ApprovePersonSearchBox { get; }
    public SearchBoxViewModel<TransferInstanceDTO> DonorSearchBox { get; }
    public SearchBoxViewModel<TransferInstanceDTO> RecipientSearchBox { get; }
    public SearchBoxViewModel<TransferInstanceDTO> DeliveryCompanySearchBox { get; }
    
    public IssuanceCertificateViewModel(
        IIssuanceCertificateService issuanceCertificateService,
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
        _issuanceCertificateService = issuanceCertificateService;
        _dataService = dataService;
        _reasonSearch = reasonSearch;
        _personnelSearch = personnelSearch;
        _donorSearch = donorSearch;
        _recipientSearch = recipientSearch;
        _deliveryCompanySearch = deliveryCompanySearch;
        _measureUnitSearch = measureUnitSearch;
        _categorySentSearch = categorySentSearch;
        _categoryReceivedSearch = categoryReceivedSearch;
        
        ReasonSearchBox = new SearchBoxViewModel<ReasonDTO>(_reasonSearch, r => r.Name);
        ApprovePersonSearchBox = new SearchBoxViewModel<PersonnelDTO>(_personnelSearch, p => p.LastName, "LastName");
        DonorSearchBox = new SearchBoxViewModel<TransferInstanceDTO>(_donorSearch, d => d.Name);
        RecipientSearchBox = new SearchBoxViewModel<TransferInstanceDTO>(_recipientSearch, r => r.Name);
        DeliveryCompanySearchBox = new SearchBoxViewModel<TransferInstanceDTO>(_deliveryCompanySearch, d => d.Name);
        
        IssueCertificateLines.CollectionChanged += OnIssueCertificateLinesCollectionChanged;
        
        WeakReferenceMessenger.Default.Register<CreateNewItemMessage<ReasonDTO>>(this, async (r, m) => 
            await HandleCreateNewItemAsync<Reason, ReasonDTO>(m, _reasonSearch));
            
        WeakReferenceMessenger.Default.Register<CreateNewItemMessage<PersonnelDTO>>(this, async (r, m) => 
            await HandleCreateNewItemAsync<Personnel, PersonnelDTO>(m, _personnelSearch));
        
        WeakReferenceMessenger.Default.Register<CreateNewItemMessage<MeasureUnitDTO>>(this, async (r, m) => 
            await HandleCreateNewItemAsync<MeasureUnit, MeasureUnitDTO>(m, _measureUnitSearch));
        
        WeakReferenceMessenger.Default.Register<CreateNewItemMessage<CategoryDTO>>(this, async (r, m) => 
            await HandleCreateNewItemAsync<Category, CategoryDTO>(m, _categorySentSearch));
    }
    
    public async Task InitializeAsync()
    {
        var reasons = await _dataService.GetAllDataAsync<Reason, ReasonDTO>();
        _reasonSearch.LoadData(reasons);

        var personnel = await _dataService.GetAllDataAsync<Personnel, PersonnelDTO>();
        _personnelSearch.LoadData(personnel);
        
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
    
    private async Task HandleCreateNewItemAsync<TEntity, TDto>(
        CreateNewItemMessage<TDto> message, 
        IFuzzySearchService<TDto> searchService) 
        where TEntity : class 
        where TDto : class, new()
    {
        var newDto = new TDto(); 
        
        var dialog = new DynamicDialog(newDto, _dataService);
        
        if (dialog.ShowDialog() == true) 
        {
            await _dataService.AddDataAsync<TEntity, TDto>(newDto);
            
            searchService.AddItemToCache(newDto);
            message.OnItemCreated?.Invoke(newDto);
        }
    }
    
    partial void OnIsAllConfirmedChanged(bool value)
    {
        if (_isSyncing) return;
        try
        {
            _isSyncing = true;
            foreach (var actLine in IssueCertificateLines)
            {
                actLine.IsConfirmed = value;
            }
        }
        finally { _isSyncing = false; }
    }

    private void OnCertificateLinePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IssueCertificateLine.IsConfirmed))
            CheckHeaderState();
    }

    private void CheckHeaderState()
    {
        if (_isSyncing || !IssueCertificateLines.Any()) return;
        try
        {
            _isSyncing = true;
            IsAllConfirmed = IssueCertificateLines.All(x => x.IsConfirmed);
        }
        finally { _isSyncing = false; }
    }

    private void OnIssueCertificateLinesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
            foreach (IssueCertificateLine item in e.NewItems)
                item.PropertyChanged += OnCertificateLinePropertyChanged;

        if (e.OldItems != null)
            foreach (IssueCertificateLine item in e.OldItems)
                item.PropertyChanged -= OnCertificateLinePropertyChanged;

        CheckHeaderState();
    }
    
    [RelayCommand]
    private void AddRow()
    {
        var newLine = new IssueCertificateLine(
            _measureUnitSearch, 
            _categorySentSearch, 
            _categoryReceivedSearch
        );
    
        IssueCertificateLines.Add(newLine);
    }

    [RelayCommand]
    private void DeleteRow(IssueCertificateLine actLine)
    {
        if (actLine != null)
            IssueCertificateLines.Remove(actLine); 
    }
    
    [RelayCommand]
    private async Task AddCertificateAsync() 
    {
        // if (IssuanceCertificate == null || ReasonSearchBox.SelectedItem == null || ApprovePersonSearchBox.SelectedItem == null)
        // {
        //     System.Windows.MessageBox.Show("Заповніть усі обов'язкові поля (Підстава, Особа)!");
        //     return;
        // }
        
        var linesDto = IssueCertificateLines.Select((line, index) => new IssueCertificateLineDTO
        {
            Name = line.Name,
            BatchNumber = line.BatchNumber,
            OrdinalNumber = index + 1,
            MeasureUnit = line.MeasureUnitSearchBox.SelectedItem, 
            PricePerUnit = line.PricePerUnit,
            QuantitySent = line.QuantitySent,
            CategorySent = line.CategorySentSearchBox.SelectedItem,
            QuantityReceived = line.QuantityReceived,
            CategoryReceived = line.CategoryReceivedSearchBox.SelectedItem,
            Notes = line.Notes,
            MadeIn = line.MadeIn
        }).ToList();
        
        var IssuanceCertificateDTO = new IssuanceCertificateDTO
        {
            Edrpou = IssuanceCertificate.Edrpou,
            ApproveDate = IssuanceCertificate.ApproveDate,
            RegistrationDate = IssuanceCertificate.RegistrationDate,
            RegistrationPlace = IssuanceCertificate.RegistrationPlace,
            TransferDateStart = IssuanceCertificate.TransferDateStart,
            TransferDateEnd = IssuanceCertificate.TransferDateEnd,
            Donor = DonorSearchBox.SelectedItem,
            Recipient = RecipientSearchBox.SelectedItem,
            DeliveryCompany = DeliveryCompanySearchBox.SelectedItem,
            
            Reason = ReasonSearchBox.SelectedItem,
            ApprovePerson = ApprovePersonSearchBox.SelectedItem, 
            
            IssueCertificateLines = linesDto
        };
    
        try
        {
            var result = await _issuanceCertificateService.AddIssuanceCertificateAsync(IssuanceCertificateDTO);
            
            WeakReferenceMessenger.Default.Send(new NewCertificateMessage(result));
            
            IssuanceCertificate = new IssuanceCertificate();
            IssueCertificateLines.Clear();
            
            System.Windows.MessageBox.Show("Акт успішно збережено!", "Успіх", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            System.Windows.MessageBox.Show($"Сталася помилка при збереженні:\n{errorMessage}", "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }
}