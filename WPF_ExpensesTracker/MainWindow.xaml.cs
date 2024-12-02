using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExpensesTrackerLibrary.Contexts;
using ExpensesTrackerLibrary.Models;
using ExpensesTrackerLibrary.Repositories;
using System.Collections.Generic;

namespace WPF_ExpensesTracker
{
    public partial class MainWindow : Window
    {
        private ExpensesTrackerContext _context;
        private ExpenseRepository _repository;

        public MainWindow()
        {
            InitializeComponent();

            _context = new ExpensesTrackerContext();
            _repository = new ExpenseRepository(_context);

            dataGridCategories.ItemsSource = _repository.GetAllCategoriesObservable();
            dataGridExpenses.ItemsSource = _repository.GetAllExpensesObservable();
            dataGridDayOfWeek.ItemsSource = _repository.GetAverageExpensesByDayOfWeek();

            comboBoxExpenseCategory.ItemsSource = _repository.GetAllCategoriesObservable();

            buttonAddCategory.Click += ButtonAddCategory_Click;
            buttonDeleteCategories.Click += ButtonDeleteCategories_Click;
            buttonAddExpense.Click += ButtonAddExpense_Click;
            buttonDeleteExpenses.Click += ButtonDeleteExpenses_Click;
        }

        private void ButtonDeleteExpenses_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridExpenses.SelectedItems != null)
            {
                var expensesToDelete = new List<Expense>();
                foreach (var item in dataGridExpenses.SelectedItems)
                {
                    expensesToDelete.Add(item as Expense);
                }
                _repository.DeleteExpenses(expensesToDelete);
            }
            else
            {
                ShowError("No Categories Selected");
            }
        }

        private void ButtonAddExpense_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime? dateTime = datePickerExpenseDate.SelectedDate;
                Category category = comboBoxExpenseCategory.SelectedItem as Category;
                decimal amount = Convert.ToDecimal(textBoxExpenseAmount.Text);
                string description = textBoxExpenseDescription.Text;

                _repository.AddExpense(new Expense()
                {
                    Date = dateTime != null ? (DateTime) dateTime : DateTime.Now,
                    Category = category,
                    Amount = amount,
                    Description = description
                });
            }
            catch (Exception ex) 
            {
                ShowError(ex.Message);
            }
        }

        private void ButtonDeleteCategories_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCategories.SelectedItems != null)
            {
                var categoriesToDelete = new List<Category>();
                foreach(var item in dataGridCategories.SelectedItems)
                {
                    categoriesToDelete.Add(item as Category);
                }
                _repository.DeleteCategories(categoriesToDelete);
            }
            else
            {
                ShowError("No Categories Selected");
            }
        }

        private void ButtonAddCategory_Click(object sender, RoutedEventArgs e)
        {
            var categoryName = textBoxCategoryName.Text.Trim();
            if (categoryName != string.Empty)
            {
                _repository.AddCategory(new Category()
                {
                    Name = categoryName
                });
            }
            else
            {
                ShowError("Category Name can't be empty!");
            }
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}