
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. REGISTER SERVICES (Dependency Injection)
            builder.Services.AddDbContext<ExpenseDbContext>(options =>
                options.UseInMemoryDatabase("ExpenseList"));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // 2. DEFINE MINIMAL API ENDPOINTS
            app.MapGet("/api/expenses", async (ExpenseDbContext db) =>
                await db.Expenses.ToListAsync());

            app.MapGet("/api/expenses/{id}", async (int id, ExpenseDbContext db) =>
                await db.Expenses.FindAsync(id) is Expense expense
                    ? Results.Ok(expense)
                    : Results.NotFound($"Expense with ID {id} not found."));

            app.MapPost("/api/expenses", async (Expense expense, ExpenseDbContext db) =>
            {
                db.Expenses.Add(expense);
                await db.SaveChangesAsync();
                return Results.Created($"/api/expenses/{expense.Id}", expense);
            });

            app.MapPut("/api/expenses/{id}", async (int id, Expense updatedExpense, ExpenseDbContext db) =>
            {
                var expense = await db.Expenses.FindAsync(id);
                if (expense is null) return Results.NotFound();

                expense.Title = updatedExpense.Title;
                expense.Amount = updatedExpense.Amount;
                expense.Category = updatedExpense.Category;
                expense.Date = updatedExpense.Date;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapDelete("/api/expenses/{id}", async (int id, ExpenseDbContext db) =>
            {
                if (await db.Expenses.FindAsync(id) is Expense expense)
                {
                    db.Expenses.Remove(expense);
                    await db.SaveChangesAsync();
                    return Results.Ok(expense);
                }
                return Results.NotFound();
            });

            app.Run();
        }
    }
    // 3. DEFINE MODEL AND DATA CONTEXT
public class Expense
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Category { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }

    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options) { }
        public DbSet<Expense> Expenses => Set<Expense>();
    }
}
