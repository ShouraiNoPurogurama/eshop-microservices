using Microsoft.AspNetCore.Builder;

namespace Ordering.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedCustomerAsync(context);
        await SeedProductAsync(context);
        await SeedOrderAndItemsAsync(context);
    }

    private static async Task SeedOrderAndItemsAsync(ApplicationDbContext context)
    {
        if (!await context.OrderItems.AnyAsync())
        {
            context.Orders.AddRange(InitialData.OrderAndItems);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedProductAsync(ApplicationDbContext context)
    {
        if (!await context.Products.AnyAsync())
        {
            context.Products.AddRange(InitialData.Products);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedCustomerAsync(ApplicationDbContext context)
    {
        if (!await context.Customers.AnyAsync())
        {
            context.Customers.AddRange(InitialData.Customers);
            await context.SaveChangesAsync();
        }
    }
}