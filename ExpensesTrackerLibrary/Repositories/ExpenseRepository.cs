﻿using Microsoft.EntityFrameworkCore;
using ExpensesTrackerLibrary.Contexts;
using ExpensesTrackerLibrary.Models;
using System.Collections.ObjectModel;

namespace ExpensesTrackerLibrary.Repositories
{
    public class ExpenseRepository
    {
        private readonly ExpensesTrackerContext _context;

        public ExpenseRepository(ExpensesTrackerContext context)
        {
            _context = context;
        }
        public void AddCategory(Category category) 
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
        public void DeleteCategories(List<Category> categories)
        {
            _context.Categories.RemoveRange(categories);
            _context.SaveChanges();
        }    
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
        public ObservableCollection<Category> GetAllCategoriesObservable()
        {
            _context.Categories.Load();
            return _context.Categories.Local.ToObservableCollection();
        }
        public void AddExpense(Expense expense) 
        {
            _context.Expenses.Add(expense);
            _context.SaveChanges(true);
        }
        public void DeleteExpense(int expenseId) 
        {
            var expense = _context.Expenses.Find(expenseId);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                _context.SaveChanges();
            }
        }
        public void DeleteExpenses(List<Expense> expenses)
        {
            _context.Expenses.RemoveRange(expenses);
            _context.SaveChanges();
        }
        public IEnumerable<Expense> GetAllExpenses()
        {
            return _context.Expenses.ToList();
        }
        public ObservableCollection<Expense> GetAllExpensesObservable()
        {
            _context.Expenses.Include(e=>e.Category).Load();
            return _context.Expenses.Local.ToObservableCollection();
        }
        public ICollection<Expense> GetExpensesByCategoryName(string categoryName)
        {
            return _context.Expenses.Where(e=>e.Category.Name.Contains(categoryName)).ToList();
        }
        public IEnumerable<Expense> GetExpensesByCategory(int categoryId)
        {
            return _context.Expenses
                .Include(e=>e.Category)
                .Where(e=>e.CategoryId == categoryId)
                .ToList();
        }
        public IEnumerable<Expense> GetExpensesByMounthYear(int mounth, int year)
        {
            return _context.Expenses
                .Include(e=>e.Category)
                .Where(e=>e.Date.Month == mounth && e.Date.Year == year)
                .ToList();
        }

        public IEnumerable<AmountByDayOfWeek> GetAverageExpensesByDayOfWeek()
        {
            return _context.Expenses
                .GroupBy(e=>e.Date.DayOfWeek)
                .Select(g=> new AmountByDayOfWeek(g.Key,g.Average(e=>(double)e.Amount)))
                .ToList();
        }
        public IEnumerable<SumByCategoryName> GetSumByCategoryName()
        {
            return _context.Expenses
                .GroupBy(e => e.Category.Name)
                .Select(g => new SumByCategoryName(g.Key, g.Sum(e => (double)e.Amount)))
                .ToList();
        }
        public IEnumerable<SumByDayAndYear> GetSumByDayAndYear()
        {
            return _context.Expenses
                .GroupBy(e => e.Date.DayOfWeek)
                .Select(g => new SumByDayAndYear(g.Key,DateTime.Now.Year,g.Sum(e => (double)e.Amount)))
                .ToList();
        }
    }
}
