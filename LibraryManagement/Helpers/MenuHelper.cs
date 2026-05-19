namespace LibraryManagement.Helpers;

public static class MenuHelper
{
    public static int AdminMenu()
    {
        Console.WriteLine();

        Console.WriteLine("===== ADMIN MENU =====");

        Console.WriteLine("1. Member Management");

        Console.WriteLine("2. Book Management");

        Console.WriteLine(
            "3. Reports"
        );

        Console.WriteLine(
            "4. Logout"
        );

        Console.WriteLine();

        Console.Write(
            "Enter choice : "
        );

        int choice;

        while(!int.TryParse(Console.ReadLine(),out choice)
            || choice < 1
            || choice > 4
        )
        {
            Console.Write(
                "Invalid input. Please enter a number : "
            );
        }

        return choice;
    }
    public static int MemberManagementMenu()
    {
        Console.WriteLine();

        Console.WriteLine(
            "===== MEMBER MANAGEMENT ====="
        );

        Console.WriteLine(
            "1. Add Member"
        );

        Console.WriteLine(
            "2. View All Members"
        );

        Console.WriteLine(
            "3. Search Member By Email"
        );

        Console.WriteLine(
            "4. Search Member By Phone"
        );

        Console.WriteLine(
            "5. Update Member"
        );

        Console.WriteLine(
            "6. Deactivate Member"
        );

        Console.WriteLine(
            "7. Back"
        );

        Console.WriteLine();

        Console.Write(
            "Enter choice : "
        );

        int choice;

        while(
            !int.TryParse(
                Console.ReadLine(),
                out choice
            )
            || choice < 1
            || choice > 7
        )
        {
            Console.Write(
                "Invalid input. Please enter a number : "
            );
        }

        return choice;
    }
    public static int MemberMenu()
    {
        Console.WriteLine();

        Console.WriteLine(
            "===== MEMBER MENU ====="
        );

        Console.WriteLine(
            "1. View Books"
        );

        Console.WriteLine(
            "2. Borrow Book"
        );

        Console.WriteLine(
            "3. Return Book"
        );

        Console.WriteLine(
            "4. Fine Management"
        );

        Console.WriteLine(
            "5. Logout"
        );

        Console.WriteLine();

        Console.Write(
            "Enter choice : "
        );

        int choice;

        while(
            !int.TryParse(
                Console.ReadLine(),
                out choice
            )
            || choice < 1
            || choice > 5
        )
        {
            Console.Write(
                "Invalid input. Please enter a number : "
            );
        }

        return choice;
    }
    public static int ViewBooksMenu()
    {
        Console.WriteLine();

        Console.WriteLine(
            "===== VIEW BOOKS ====="
        );
        Console.WriteLine(
            "1. View all books"
        );
        Console.WriteLine(
            "2. View By Title"
        );

        Console.WriteLine(
            "3. View By Author"
        );

        Console.WriteLine(
            "4. View By Category"
        );

        Console.WriteLine(
            "5. Back"
        );

        Console.WriteLine();

        Console.Write(
            "Enter choice : "
        );

        int choice;

        while(
            !int.TryParse(
                Console.ReadLine(),
                out choice
            )
            || choice < 1
            || choice > 5
        )
        {
            Console.Write(
                "Invalid input. Please enter a number : "
            );
        }

        return choice;
    }
    public static int FineManagementMenu()
    {
        Console.WriteLine();

        Console.WriteLine(
            "===== FINE MANAGEMENT ====="
        );

        Console.WriteLine(
            "1. View Pending Fine"
        );

        Console.WriteLine(
            "2. Pay Fine"
        );

        Console.WriteLine(
            "3. View Fine History"
        );

        Console.WriteLine(
            "4. Back"
        );

        Console.WriteLine();

        Console.Write(
            "Enter choice : "
        );

        int choice;

        while(
            !int.TryParse(
                Console.ReadLine(),
                out choice
            )
            || choice < 1
            || choice > 4
        )
        {
            Console.Write(
                "Invalid input. Please enter a number : "
            );
        }

        return choice;
    }
    public static int ReportMenu()
    {
        Console.WriteLine();

        Console.WriteLine(
            "===== REPORTS ====="
        );

        Console.WriteLine(
            "1. Currently Borrowed Books"
        );

        Console.WriteLine(
            "2. Overdue Books"
        );

        Console.WriteLine(
            "3. Members With Pending Fines"
        );

        Console.WriteLine(
            "4. Most Borrowed Books"
        );

        Console.WriteLine(
            "5. Available Books By Category"
        );

        Console.WriteLine(
            "6. Member Borrowing History"
        );

        Console.WriteLine(
            "7. Back"
        );

        Console.WriteLine();

        Console.Write(
            "Enter choice : "
        );

        int choice;

        while(
            !int.TryParse(
                Console.ReadLine(),
                out choice
            )
            || choice < 1
            || choice > 7
        )
        {
            Console.Write(
                "Invalid input. Please enter a number : "
            );
        }

        return choice;
    }
    public static int BookManagementMenu()
    {
        Console.WriteLine();

        Console.WriteLine(
            "===== BOOK MANAGEMENT ====="
        );

        Console.WriteLine(
            "1. Add Book"
        );

        Console.WriteLine(
            "2. Add Book Copies"
        );

        Console.WriteLine(
            "3. View Books By Title"
        );

        Console.WriteLine(
            "4. View Books By Author"
        );

        Console.WriteLine(
            "5. View Books By Category"
        );

        Console.WriteLine(
            "6. Mark Copy Damaged"
        );

        Console.WriteLine(
            "7. Back"
        );

        Console.WriteLine();

        Console.Write(
            "Enter choice : "
        );

        int choice;

        while(
            !int.TryParse(
                Console.ReadLine(),
                out choice
            )
            || choice < 1
            || choice > 7
        )
        {
            Console.Write(
                "Invalid input. Please enter a number : "
            );
        }

        return choice;
    }
}
