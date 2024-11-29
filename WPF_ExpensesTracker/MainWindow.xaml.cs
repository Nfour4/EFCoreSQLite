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

            buttonAddCategory.Click += ButtonAddCategory_Click;
            buttonDeleteCategories.Click += ButtonDeleteCategories_Click;
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