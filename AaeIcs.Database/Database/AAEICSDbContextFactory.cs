using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using AAEICS.Database.Context;

namespace AAEICS.Database.Database;

// Цей клас Entity Framework знайде автоматично під час роботи в консолі
public class AAEICSDbContextFactory : IDesignTimeDbContextFactory<AAEICSDbContext>
{
    public AAEICSDbContext CreateDbContext(string[] args)
    {
        // 1. Створюємо налаштування (Builder)
        var optionsBuilder = new DbContextOptionsBuilder<AAEICSDbContext>();

        // 2. Вказуємо рядок підключення ТІЛЬКИ для створення міграцій.
        // Це не вплине на реальну програму. Реальна програма візьме шлях з AppConfigService.
        // Можна вказати будь-який шлях, головне щоб синтаксис був правильний.
        optionsBuilder.UseSqlite("Data Source=designTime.db");

        // 3. Повертаємо готовий контекст
        return new AAEICSDbContext(optionsBuilder.Options);
    }
}