﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQL_Lesson_6__EF_Core___SQLite_.Models;

namespace SQL_Lesson_6__EF_Core___SQLite_.Contexts
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
