using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class MoneyItem
{
    public string Title;
    public decimal Amount;
    public string Month;
    public string Type;
}

class Program
{
    static List<MoneyItem> itemList = new List<MoneyItem>();
    static string filePath = "lokalData.txt";
    static decimal balance = 0;

    static int GetMonthNumber(string month)
    {
        switch (month.ToLower())
        {
            case "januari": return 1;
            case "februari": return 2;
            case "mars": return 3;
            case "april": return 4;
            case "maj": return 5;
            case "juni": return 6;
            case "juli": return 7;
            case "augusti": return 8;
            case "september": return 9;
            case "oktober": return 10;
            case "november": return 11;
            case "december": return 12;
            default: return 0;
        }
    }

    static void Main(string[] args)
    {
        LoadFromFile();

        while (true)
        {
            Console.Clear();
            balance = itemList.Sum(x => x.Type == "Income" ? x.Amount : -x.Amount);

            Console.WriteLine("Welcome to TrackMoney");
            Console.WriteLine($"You have currently {balance}kr on your account.\n");
            Console.WriteLine("Pick an option:");
            Console.WriteLine("(1) Show items (All/Expense(s)/Income(s))");
            Console.WriteLine("(2) Add New Expense/Income");
            Console.WriteLine("(3) Edit Item (edit, remove)");
            Console.WriteLine("(4) Save and Quit");

            string choice = Console.ReadKey().KeyChar.ToString();

            switch (choice)
            {
                case "1":
                    ShowItems();
                    break;
                case "2":
                    AddNewItem();
                    break;
                case "3":
                    EditItem();
                    break;
                case "4":
                    SaveToFile();
                    Console.Clear();
                    Console.WriteLine("Thank you for using TrackMoney");
                    Console.WriteLine($"Your data has now been saved locally in {filePath}");
                    Console.WriteLine($"You can now safely close the application.");
                    return;
                default:
                    break;
            }
        }
    }

    static void ShowItems()
    {
        Console.Clear();
        balance = itemList.Sum(x => x.Type == "Income" ? x.Amount : -x.Amount);
        Console.WriteLine("Welcome to TrackMoney");
        Console.WriteLine($"You have currently {balance}kr on your account.\n");
        Console.WriteLine("Pick an option:");
        Console.WriteLine("(1) Show All Items");
        Console.WriteLine("(2) Show Expenses Only");
        Console.WriteLine("(3) Show Incomes Only");
        string filterChoice = Console.ReadKey().KeyChar.ToString();

        List<MoneyItem> filteredList = itemList;
        if (filterChoice == "2")
        {
            filteredList = itemList.Where(x => x.Type == "Expense").ToList();
        }
        else if (filterChoice == "3")
        {
            filteredList = itemList.Where(x => x.Type == "Income").ToList();
        }

        Console.Clear();
        balance = itemList.Sum(x => x.Type == "Income" ? x.Amount : -x.Amount);
        Console.WriteLine("Welcome to TrackMoney");
        Console.WriteLine($"You have currently {balance}kr on your account.\n");
        Console.WriteLine("Sort by month, amount or title:");
        Console.WriteLine("(1) Month");
        Console.WriteLine("(2) Amount");
        Console.WriteLine("(3) Title");
        string sortChoice = Console.ReadKey().KeyChar.ToString();

        Console.Clear();
        balance = itemList.Sum(x => x.Type == "Income" ? x.Amount : -x.Amount);
        Console.WriteLine("Welcome to TrackMoney");
        Console.WriteLine($"You have currently {balance}kr on your account.\n");
        Console.WriteLine("Sort by ascending or descending order:");
        Console.WriteLine("(1) Ascending");
        Console.WriteLine("(2) Descending");
        string orderChoice = Console.ReadKey().KeyChar.ToString();

        if (sortChoice == "1")
        {
            filteredList = orderChoice == "1"
                ? filteredList.OrderBy(x => GetMonthNumber(x.Month)).ToList()
                : filteredList.OrderByDescending(x => GetMonthNumber(x.Month)).ToList();
        }
        else if (sortChoice == "2")
        {
            filteredList = orderChoice == "1"
                ? filteredList.OrderBy(x => x.Amount).ToList()
                : filteredList.OrderByDescending(x => x.Amount).ToList();
        }
        else if (sortChoice == "3")
        {
            filteredList = orderChoice == "1"
                ? filteredList.OrderBy(x => x.Title).ToList()
                : filteredList.OrderByDescending(x => x.Title).ToList();
        }

        Console.Clear();
        balance = itemList.Sum(x => x.Type == "Income" ? x.Amount : -x.Amount);
        Console.WriteLine("Welcome to TrackMoney");
        Console.WriteLine($"You have currently {balance}kr on your account.\n");
        Console.WriteLine($"{"Type",-10} {"Title",-20} {"Amount",10} {"Month",15}");
        Console.WriteLine(new string('-', 60));

        foreach (var item in filteredList)
        {
            ConsoleColor color = item.Type == "Income" ? ConsoleColor.Green : ConsoleColor.Red;
            Console.ForegroundColor = color;
            Console.WriteLine($"{item.Type,-10} {item.Title,-20} {item.Amount,10:F2} {item.Month,15}");
            Console.ResetColor();
        }

        Console.WriteLine("\nPress any key to return to main menu...");
        Console.ReadKey();
    }

    static void AddNewItem()
    {
        Console.Clear();
        balance = itemList.Sum(x => x.Type == "Income" ? x.Amount : -x.Amount);
        Console.WriteLine("Welcome to TrackMoney");
        Console.WriteLine($"You have currently {balance}kr on your account.\n");
        Console.WriteLine("Pick an option:");

        MoneyItem item = new MoneyItem();

        Console.WriteLine("(1) Expense");
        Console.WriteLine("(2) Income");
        string typeChoice = Console.ReadKey().KeyChar.ToString();
        item.Type = typeChoice == "1" ? "Expense" : "Income";

        Console.Clear();
        balance = itemList.Sum(x => x.Type == "Income" ? x.Amount : -x.Amount);
        Console.WriteLine("Welcome to TrackMoney");
        Console.WriteLine($"You have currently {balance}kr on your account.\n");
        Console.WriteLine($"Please input the details for the {item.Type}:");
        Console.Write("Title: ");
        item.Title = Console.ReadLine();

        Console.Write("Amount: ");
        item.Amount = decimal.Parse(Console.ReadLine());

        while (true)
        {
            Console.Write("Month: ");
            item.Month = Console.ReadLine();

            if (GetMonthNumber(item.Month) == 0)
            {
                Console.WriteLine("Invalid month! Please use month names (Eg. Januari).");
            }
            else
            {
                break;
            }
        }

        itemList.Add(item);

        Console.Clear();
        balance = itemList.Sum(x => x.Type == "Income" ? x.Amount : -x.Amount);
        Console.WriteLine("Welcome to TrackMoney");
        Console.WriteLine($"You have currently {balance}kr on your account.\n");
        Console.WriteLine("Item successfully added!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void EditItem()
    {
        Console.Clear();
        balance = itemList.Sum(x => x.Type == "Income" ? x.Amount : -x.Amount);
        Console.WriteLine("Welcome to TrackMoney");
        Console.WriteLine($"You have currently {balance}kr on your account.\n");
        Console.WriteLine("EDIT/REMOVE ITEM\n");

        if (itemList.Count == 0)
        {
            Console.WriteLine("No items to edit.");
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"{"#",-5} {"Type",-10} {"Title",-20} {"Amount",10} {"Month",-15}");
        Console.WriteLine(new string('-', 65));

        for (int i = 0; i < itemList.Count; i++)
        {
            var item = itemList[i];
            ConsoleColor color = item.Type == "Income" ? ConsoleColor.Green : ConsoleColor.Red;
            Console.ForegroundColor = color;
            Console.WriteLine($"{i + 1,-5} {item.Type,-10} {item.Title,-20} {item.Amount,10:F2} {item.Month,-15}");
            Console.ResetColor();
        }

        Console.Write("\nEnter an item number to edit or 0 to cancel: ");
        int itemNumber = int.Parse(Console.ReadLine());

        if (itemNumber == 0 || itemNumber > itemList.Count)
        {
            return;
        }

        MoneyItem selectedItem = itemList[itemNumber - 1];

        Console.Clear();
        balance = itemList.Sum(x => x.Type == "Income" ? x.Amount : -x.Amount);
        Console.WriteLine("Welcome to TrackMoney");
        Console.WriteLine($"You have currently {balance}kr on your account.\n");
        Console.WriteLine("(1) Edit Item");
        Console.WriteLine("(2) Remove Item");
        string editChoice = Console.ReadKey().KeyChar.ToString();

        if (editChoice == "1")
        {
            Console.Clear();
            balance = itemList.Sum(x => x.Type == "Income" ? x.Amount : -x.Amount);
            Console.WriteLine("Welcome to TrackMoney");
            Console.WriteLine($"You have currently {balance}kr on your account.\n");
            Console.WriteLine("Pick an option:");
            Console.WriteLine("(1) Expense");
            Console.WriteLine("(2) Income");
            string typeChoice = Console.ReadKey().KeyChar.ToString();
            string newType = typeChoice == "1" ? "Expense" : "Income";

            Console.Clear();
            balance = itemList.Sum(x => x.Type == "Income" ? x.Amount : -x.Amount);
            Console.WriteLine("Welcome to TrackMoney");
            Console.WriteLine($"You have currently {balance}kr on your account.\n");
            Console.WriteLine($"Please input the updated details for the {selectedItem.Type}:");
            Console.Write("Title: ");
            string newTitle = Console.ReadLine();

            Console.Write("Amount: ");
            decimal newAmount = decimal.Parse(Console.ReadLine());

            string newMonth = "";
            while (true)
            {
                Console.Write("Month: ");
                newMonth = Console.ReadLine();

                if (GetMonthNumber(newMonth) == 0)
                {
                    Console.WriteLine("Invalid month! Please use month names (Eg. Januari).");
                }
                else
                {
                    break;
                }
            }

            selectedItem.Type = newType;
            selectedItem.Title = newTitle;
            selectedItem.Amount = newAmount;
            selectedItem.Month = newMonth;

            Console.Clear();
            balance = itemList.Sum(x => x.Type == "Income" ? x.Amount : -x.Amount);
            Console.WriteLine("Welcome to TrackMoney");
            Console.WriteLine($"You have currently {balance}kr on your account.\n");
            Console.WriteLine("Item was successfully updated!");
        }
        else if (editChoice == "2")
        {
            itemList.RemoveAt(itemNumber - 1);
            Console.Clear();
            balance = itemList.Sum(x => x.Type == "Income" ? x.Amount : -x.Amount);
            Console.WriteLine("Welcome to TrackMoney");
            Console.WriteLine($"You have currently {balance}kr on your account.\n");
            Console.WriteLine("Item was successfully removed!");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void SaveToFile()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var item in itemList)
            {
                writer.WriteLine($"{item.Type}|{item.Title}|{item.Amount}|{item.Month}");
            }
        }
    }

    static void LoadFromFile()
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 4)
                {
                    MoneyItem item = new MoneyItem
                    {
                        Type = parts[0],
                        Title = parts[1],
                        Amount = decimal.Parse(parts[2]),
                        Month = parts[3]
                    };
                    itemList.Add(item);
                }
            }
        }
    }
}