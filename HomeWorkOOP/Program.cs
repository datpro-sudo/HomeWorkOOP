using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkOOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LibraryCatalog catalog = new LibraryCatalog();

            // Thêm các mục vào danh mục
            catalog.AddItem(new Book
            {
                Title = "1984",
                Author = "George Orwell",
                PublicationDate = "1949",
                Genre = "Dystopian",
                Available = true
            });

            catalog.AddItem(new DVD
            {
                Title = "Inception",
                Author = "Christopher Nolan",
                PublicationDate = "2010",
                Runtime = 148,
                Available = true
            });

            while (true)
            {
                Console.WriteLine("\n=== Library System ===");
                Console.WriteLine("1. Display items");
                Console.WriteLine("2. Borrow an item");
                Console.WriteLine("3. Return an item");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        catalog.DisplayItems();
                        break;

                    case "2":
                        Console.Write("Enter the title of the item to borrow: ");
                        string borrowTitle = Console.ReadLine();
                        LibraryItem borrowItem = catalog.FindItem(borrowTitle);

                        if (borrowItem != null)
                        {
                            borrowItem.Checkout();
                        }
                        else
                        {
                            Console.WriteLine("Item not found.");
                        }
                        break;

                    case "3":
                        Console.Write("Enter the title of the item to return: ");
                        string returnTitle = Console.ReadLine();
                        LibraryItem returnItem = catalog.FindItem(returnTitle);

                        if (returnItem != null)
                        {
                            returnItem.ReturnItem();
                        }
                        else
                        {
                            Console.WriteLine("Item not found.");
                        }
                        break;

                    case "4":
                        Console.WriteLine("Exiting the system. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        abstract class LibraryItem
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string PublicationDate { get; set; }
            public bool Available { get; set; }

            public abstract void Checkout();
            public abstract void ReturnItem();
        }

        class Book : LibraryItem
        {
            public string Genre { get; set; }

            public override void Checkout()
            {
                if (Available)
                {
                    Available = false;
                    Console.WriteLine($"Book '{Title}' has been checked out.");
                }
                else
                {
                    Console.WriteLine($"Book '{Title}' is not available.");
                }
            }

            public override void ReturnItem()
            {
                Available = true;
                Console.WriteLine($"Book '{Title}' has been returned.");
            }
        }

        class DVD : LibraryItem
        {
            public int Runtime { get; set; } 

            public override void Checkout()
            {
                if (Available)
                {
                    Available = false;
                    Console.WriteLine($"DVD '{Title}' has been checked out.");
                }
                else
                {
                    Console.WriteLine($"DVD '{Title}' is not available.");
                }
            }

            public override void ReturnItem()
            {
                Available = true;
                Console.WriteLine($"DVD '{Title}' has been returned.");
            }
        }

        class LibraryCatalog
        {
            private List<LibraryItem> items = new List<LibraryItem>();

            public void AddItem(LibraryItem item)
            {
                items.Add(item);
            }

            public void DisplayItems()
            {
                Console.WriteLine("\nCurrent items in the catalog:");
                foreach (var item in items)
                {
                    Console.WriteLine($"- {item.Title} by {item.Author} ({(item.Available ? "Available" : "Checked out")})");
                }
            }

            public LibraryItem FindItem(string title)
            {
                foreach (var item in items)
                {
                    if (item.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                    {
                        return item;
                    }
                }
                return null;
            }
        }
    }
}
