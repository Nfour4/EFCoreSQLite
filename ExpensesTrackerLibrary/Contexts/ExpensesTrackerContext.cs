using Microsoft.EntityFrameworkCore;
using ExpensesTrackerLibrary.Models;

namespace ExpensesTrackerLibrary.Contexts
{
    public class ExpensesTrackerContext : DbContext
    {
        //DbSet для категории расходов
        public DbSet<Category> Categories { get; set; }
        //DbSet для самих расходов
        public DbSet<Expense> Expenses { get; set; }

        //Конструктор класса 
        public ExpensesTrackerContext()
        {
            //Удаляем существующую базу данных, если она есть; если нет - удалять не будет
            //Database.EnsureDeleted();

            //Создаем новую базу данных, если ее нет; если есть - создавать не будет
            Database.EnsureCreated();
        }

        //Метод для настройки подключения к базе данных
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Используем SQLite базу данных с указанным именем файла
            optionsBuilder.UseSqlite("Data Source=Expenses.db");
        }
    }
}
