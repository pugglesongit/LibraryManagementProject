// See https://aka.ms/new-console-template for more information

using System;

class LibraryManagementSystem
{
    static string[] books = new string[5];
    static int bookCount = 0;
    static string[] borrowed = new string[3];
    static int borrowedCount = 0;

    static void Main(string[] args)
    {
        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBooks();
                    break;
                case "2":
                    RemoveBook();
                    break;
                case "3":
                    DisplayBooks();
                    break;
                case "4":
                    SearchBook();
                    break;
                case "5":
                    BorrowBooks();
                    break;
                case "6":
                    DisplayBorrowedBooks();
                    break;
                case "7":
                    ReturnBooks();
                    break;
                case "8":
                    Console.WriteLine("Exiting the program.");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\nLibrary Management System");
        Console.WriteLine("1. Add a book");
        Console.WriteLine("2. Remove a book by title");
        Console.WriteLine("3. Display all books");
        Console.WriteLine("4. Search for a book");
        Console.WriteLine("5. Borrow a book");
        Console.WriteLine("6. Display borrowed books");
        Console.WriteLine("7. Return a book");
        Console.WriteLine("8. Exit");
        Console.Write("Choose an option: ");
    }

    static void AddBooks()
    {
        while (true)
        {
            if (bookCount >= 5)
            {
                Console.WriteLine("All slots are full. No more books can be added.");
                break;
            }

            string title = GetValidBookTitle("Enter the book title to add: ");
            books[bookCount] = title;
            bookCount++;
            Console.WriteLine("Book added successfully.");

            if (!GetYesNoConfirmation("Would you like to add another book? (y/n): "))
            {
                break;
            }
        }
    }

    static void RemoveBook()
    {
        Console.Write("Enter the book title to remove: ");
        string titleToRemove = Console.ReadLine();
        
        if (SearchAndRemoveFromArray(books, ref bookCount, titleToRemove))
        {
            Console.WriteLine("Book removed successfully.");
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
    }

    static void DisplayBooks()
    {
        Console.WriteLine("Current books in the library:");
        DisplayArray(books, bookCount, "No books available.");
    }

    static void SearchBook()
    {
        if (bookCount == 0)
        {
            Console.WriteLine("No books are available to search.");
            return;
        }

        Console.Write("Enter the book title to search: ");
        string titleToSearch = Console.ReadLine();
        
        if (SearchInArray(books, bookCount, titleToSearch))
        {
            Console.WriteLine("Book found!");
        }
        else
        {
            Console.WriteLine("Book not found, please check title and try again.");
        }
    }

    static void BorrowBooks()
    {
        while (borrowedCount < 3 && bookCount > 0)
        {
            Console.WriteLine("Available books to borrow:");
            DisplayArray(books, bookCount, "No books available.");
            
            Console.Write("Enter the book title to borrow: ");
            string borrowTitle = Console.ReadLine();

            if (SearchAndRemoveFromArray(books, ref bookCount, borrowTitle))
            {
                borrowed[borrowedCount] = borrowTitle;
                borrowedCount++;
                Console.WriteLine("Book borrowed! Enjoy your reading!");

                if (!GetYesNoConfirmation("Would you like to borrow another book? (y/n): "))
                {
                    break;
                }
            }
            else
            {
                Console.WriteLine("Book not found in the library.");
            }
        }

        if (borrowedCount >= 3)
        {
            Console.WriteLine("You have reached the maximum of 3 borrowed books.");
        }
        else if (bookCount == 0)
        {
            Console.WriteLine("No more books available to borrow.");
        }
    }

    static void DisplayBorrowedBooks()
    {
        Console.WriteLine("Borrowed books:");
        DisplayArray(borrowed, borrowedCount, "No books borrowed.");
    }

    static void ReturnBooks()
    {
        while (true)
        {
            if (borrowedCount == 0)
            {
                Console.WriteLine("No books to return.");
                break;
            }

            Console.Write("Enter the book title to return: ");
            string returnTitle = Console.ReadLine();

            if (SearchAndRemoveFromArray(borrowed, ref borrowedCount, returnTitle))
            {
                books[bookCount] = returnTitle;
                bookCount++;
                Console.WriteLine($"The book '{returnTitle}' has been successfully checked in.");

                if (!GetYesNoConfirmation("Would you like to return another book? (y/n): "))
                {
                    break;
                }
            }
            else
            {
                Console.WriteLine($"The book '{returnTitle}' has already been checked in.");
            }
        }
    }

    // Helper Methods
    static string GetValidBookTitle(string prompt)
    {
        string title;
        do
        {
            Console.Write(prompt);
            title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Book title cannot be empty. Please enter a valid title.");
            }
        } while (string.IsNullOrWhiteSpace(title));
        return title;
    }

    static bool GetYesNoConfirmation(string prompt)
    {
        string response;
        do
        {
            Console.Write(prompt);
            response = Console.ReadLine();
            if (response.ToLower() != "y" && response.ToLower() != "yes" && 
                response.ToLower() != "n" && response.ToLower() != "no")
            {
                Console.WriteLine("Invalid selection. Please select y or n.");
            }
        } while (response.ToLower() != "y" && response.ToLower() != "yes" && 
                 response.ToLower() != "n" && response.ToLower() != "no");
        
        return response.ToLower() == "y" || response.ToLower() == "yes";
    }

    static bool SearchInArray(string[] array, int count, string searchTerm)
    {
        for (int i = 0; i < count; i++)
        {
            if (array[i] == searchTerm)
            {
                return true;
            }
        }
        return false;
    }

    static bool SearchAndRemoveFromArray(string[] array, ref int count, string searchTerm)
    {
        for (int i = 0; i < count; i++)
        {
            if (array[i] == searchTerm)
            {
                for (int bookSearch = i; bookSearch < count - 1; bookSearch++)
                {
                    array[bookSearch] = array[bookSearch + 1];
                }
                array[count - 1] = "";
                count--;
                return true;
            }
        }
        return false;
    }

    static void DisplayArray(string[] array, int count, string emptyMessage)
    {
        if (count == 0)
        {
            Console.WriteLine(emptyMessage);
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{i + 1}. {array[i]}");
            }
        }
    }
}