using SQL_Lesson_6__EF_Core___SQLite_.Contexts;
using SQL_Lesson_6__EF_Core___SQLite_.Models;

public class Program
{
    private static void Main(string[] args)
    {
        using (var context = new ExpensesTrackerContext()) 
        {
            //context.Categories.Add(new Category { Name = "Food" });
            //context.Categories.Add(new Category { Name = "Transportation" });

            context.SaveChanges();

            context.Expenses.Add(new Expense
            {
                Date = DateTime.Now,
                Amount = 120m,
                Description = "Breakfast",
                Category = context.Categories.FirstOrDefault()
            });

            context.SaveChanges();

            //Rertive and display all expenses
            var expenses = context.Expenses.ToList();
            foreach (var expense in expenses) 
            {
                Console.WriteLine($"Date: {expense.Date}, Amount: {expense.Amount}, " +
                    $"Description: {expense.Description}, Category: {expense.Category.Name}");
            }
        }
    }
}