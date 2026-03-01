using System;
using AAEICS.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace AAEICS.Database.Database;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        // 1. Одиниці виміру (MeasureUnit)
        modelBuilder.Entity<MeasureUnit>().HasData(
            new MeasureUnit { UnitId = 1, Name = "шт." },
            new MeasureUnit { UnitId = 2, Name = "кг" },
            new MeasureUnit { UnitId = 3, Name = "л" },
            new MeasureUnit { UnitId = 4, Name = "компл." },
            new MeasureUnit { UnitId = 5, Name = "т" }
        );

        // 2. Категорії майна (Category)
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Озброєння" },
            new Category { Id = 2, Name = "Боєприпаси" },
            new Category { Id = 3, Name = "Амуніція та екіпірування" },
            new Category { Id = 4, Name = "Медикаменти" },
            new Category { Id = 5, Name = "Паливно-мастильні матеріали" },
            new Category { Id = 6, Name = "Продовольство" },
            new Category { Id = 7, Name = "Транспортні засоби" }
        );

        // 3. Військові звання (Rank)
        modelBuilder.Entity<Rank>().HasData(
            new Rank { RankId = 1, Name = "Солдат" },
            new Rank { RankId = 2, Name = "Молодший сержант" },
            new Rank { RankId = 3, Name = "Сержант" },
            new Rank { RankId = 4, Name = "Старший сержант" },
            new Rank { RankId = 5, Name = "Лейтенант" },
            new Rank { RankId = 6, Name = "Капітан" },
            new Rank { RankId = 7, Name = "Майор" }
        );

        // 4. Посади (Position)
        modelBuilder.Entity<Position>().HasData(
            new Position { PositionId = 1, Name = "Стрілець" },
            new Position { PositionId = 2, Name = "Водій" },
            new Position { PositionId = 3, Name = "Бойовий медик" },
            new Position { PositionId = 4, Name = "Командир відділення" },
            new Position { PositionId = 5, Name = "Командир взводу" },
            new Position { PositionId = 6, Name = "Начальник складу" },
            new Position { PositionId = 7, Name = "Командир роти" }
        );

        // 5. Підстави / Накази (Reason)
        modelBuilder.Entity<Reason>().HasData(
            new Reason { ReasonId = 1, Name = "Наказ командира ВЧ", Date = new DateTime(2023, 1, 15) },
            new Reason { ReasonId = 2, Name = "Розпорядження штабу", Date = new DateTime(2023, 2, 10) },
            new Reason { ReasonId = 3, Name = "Акт прийому-передачі", Date = new DateTime(2023, 3, 5) },
            new Reason { ReasonId = 4, Name = "Гуманітарна допомога", Date = new DateTime(2023, 4, 20) }
        );

        // 6. Суб'єкти передачі (TransferInstance) - Донори, Отримувачі, Перевізники
        modelBuilder.Entity<TransferInstance>().HasData(
            new TransferInstance { InstanceId = 1, Name = "Військова частина А1111" },
            new TransferInstance { InstanceId = 2, Name = "Військова частина А2222" },
            new TransferInstance { InstanceId = 3, Name = "Склад матеріального забезпечення №1" },
            new TransferInstance { InstanceId = 4, Name = "БФ «Повернись живим»" },
            new TransferInstance { InstanceId = 5, Name = "ТОВ «Нова Пошта»" }
        );

        // 7. Персонал (Personnel)
        modelBuilder.Entity<Personnel>().HasData(
            new Personnel 
            { 
                PersonId = 1, 
                FirstName = "Іван", 
                LastName = "Шевченко", 
                MiddleName = "Миколайович", 
                RankId = 6, // Капітан
                PositionId = 7 // Командир роти
            },
            new Personnel 
            { 
                PersonId = 2, 
                FirstName = "Петро", 
                LastName = "Коваленко", 
                MiddleName = "Олексійович", 
                RankId = 3, // Сержант
                PositionId = 6 // Начальник складу
            },
            new Personnel 
            { 
                PersonId = 3, 
                FirstName = "Олена", 
                LastName = "Бойко", 
                MiddleName = "Василівна", 
                RankId = 5, // Лейтенант
                PositionId = 5 // Командир взводу
            }
        );
    }
}