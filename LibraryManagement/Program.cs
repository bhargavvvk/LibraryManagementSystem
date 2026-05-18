using LibraryManagement.Helpers;
using LibraryManagementBLLibrary.Exceptions;
using LibraryManagementBLLibrary.Services;
using LibraryManagementModelLibrary.Models;

namespace LibraryManagement
{

    internal class Program
    {
        static void Main(string[] args)
        {
            UserAccountService userAccountService =new UserAccountService();
            MemberService memberService = new MemberService();
            BookService bookService = new BookService();
            BorrowingService borrowingService = new BorrowingService();
            ReportService reportService = new ReportService();
            BookCategoryService bookCategoryService =new BookCategoryService();
            //main loop
            bool appRunning = true;
            while(appRunning)
            {
                Console.WriteLine();
                Console.WriteLine("===== LIBRARY MANAGEMENT SYSTEM =====");
                Console.WriteLine();
                Console.Write("Username : ");
                string username = Console.ReadLine()!;
                Console.Write("Password : ");
                string password = Console.ReadLine()!;
                try
                {
                    var account = userAccountService.Login(username, password);
                    if(account.Role == UserRole.Admin)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Welcome Admin");
                        bool adminLoggedIn = true;
                        while(adminLoggedIn)
                        {
                            try
                            {
                                int adminChoice = MenuHelper.AdminMenu();
                                switch(adminChoice)
                                {
                                    case 1:
                                        bool membermanagementMenu = true;
                                        while(membermanagementMenu)
                                        {
                                            int memberChoice =  MenuHelper.MemberManagementMenu();
                                            switch(memberChoice)
                                            {
                                                case 1:
                                                    Console.Write("Enter member name : ");
                                                    string name =Console.ReadLine()!;
                                                    Console.Write("Enter email : ");
                                                    string email = Console.ReadLine()!;
                                                    Console.Write("Enter phone number : ");
                                                    string phoneNumber = Console.ReadLine()!;
                                                    Console.WriteLine();
                                                    Console.WriteLine("Select Membership Type");
                                                    Console.WriteLine( "1. Basic");
                                                    Console.WriteLine(
                                                        "2. Student"
                                                    );

                                                    Console.WriteLine(
                                                        "3. Premium"
                                                    );

                                                    Console.Write(
                                                        "Enter choice : "
                                                    );

                                                    int membershipChoice;

                                                    while(
                                                        !int.TryParse(
                                                            Console.ReadLine(),
                                                            out membershipChoice
                                                        )
                                                        || membershipChoice < 1
                                                        || membershipChoice > 3
                                                    )
                                                    {
                                                        Console.Write(
                                                            "Invalid input. Please enter a number : "
                                                        );
                                                    }
                                                    MemberShipType memberType =
                                                    membershipChoice switch
                                                    {
                                                        1 => MemberShipType.Basic,

                                                        2 => MemberShipType.Student,

                                                        3 => MemberShipType.Premium,

                                                        _ => MemberShipType.Basic
                                                    };
                                                    Member member = new Member
                                                    {
                                                        Name = name,

                                                        Email = email,

                                                        PhoneNumber = phoneNumber,

                                                        MemberType = memberType,

                                                        IsActive = true
                                                    };
                                                    var createdMember = memberService.AddMember(member);
                                                    var createdAccount = userAccountService.CreateMemberAccount(createdMember);
                                                    Console.WriteLine();
                                                    Console.WriteLine(
                                                        "Member created successfully."
                                                    );

                                                    Console.WriteLine(
                                                        $"Username : {createdAccount.Username}"
                                                    );

                                                    Console.WriteLine(
                                                        $"Password : {createdAccount.PassWord}"
                                                    );
                                                    break;
                                                case 2:
                                                    var members = memberService.GetAllMembers();
                                                    if(members == null || members.Count == 0)
                                                    {
                                                        Console.WriteLine( "No members found.");
                                                        break;
                                                    }
                                                    foreach(var eachmember in members)
                                                    {
                                                        DisplayHelper.PrintMember(
                                                            eachmember
                                                        );
                                                    }
                                                    break;
                                                case 3:
                                                    Console.Write("Enter member email : ");
                                                    string searchemail = Console.ReadLine()!;

                                                    var memberByEmail = memberService.GetMemberByEmail(searchemail);

                                                    DisplayHelper.PrintMember(memberByEmail);
                                                    break;
                                                case 4:
                                                    Console.Write("Enter phone number : ");
                                                    string SearchphoneNumber = Console.ReadLine()!;
                                                    var memberByPhone = memberService.GetMemberByPhoneNumber(SearchphoneNumber);
                                                    DisplayHelper.PrintMember(memberByPhone);
                                                    break;
                                                case 5:
                                                    Console.Write("Enter member id : ");
                                                    int memberId;
                                                    while(!int.TryParse(Console.ReadLine(), out memberId))
                                                    {
                                                        Console.Write("Invalid input. Please enter a number : ");
                                                    }
                                                    Console.Write("Enter new name : ");
                                                    string updatedName = Console.ReadLine()!;

                                                    Console.Write("Enter new email : ");
                                                    string updatedEmail = Console.ReadLine()!;

                                                    Console.Write("Enter new phone number : ");
                                                    string updatedPhone = Console.ReadLine()!;

                                                    Console.WriteLine();
                                                    Console.WriteLine("Select Membership Type");
                                                    Console.WriteLine("1. Basic");
                                                    Console.WriteLine("2. Student");
                                                    Console.WriteLine("3. Premium");
                                                    Console.Write("Enter choice : ");
                                                    int updatedMembershipChoice;
                                                    while(!int.TryParse(Console.ReadLine(), out updatedMembershipChoice) || updatedMembershipChoice < 1 || updatedMembershipChoice > 3)
                                                    {
                                                        Console.Write("Invalid input. Please enter a number : ");
                                                    }

                                                    Member updatedMemberData = new Member
                                                    {
                                                        Id = memberId,
                                                        Name = updatedName,
                                                        Email = updatedEmail,
                                                        PhoneNumber = updatedPhone,
                                                        MemberType = updatedMembershipChoice switch
                                                        {
                                                            1 => MemberShipType.Basic,
                                                            2 => MemberShipType.Student,
                                                            3 => MemberShipType.Premium,
                                                            _ => MemberShipType.Basic
                                                        },
                                                        IsActive = true
                                                    };

                                                    var updatedMember = memberService.UpdateMember(updatedMemberData);

                                                    Console.WriteLine("Member updated successfully.");

                                                    DisplayHelper.PrintMember(updatedMember);

                                                    break;
                                                case 6:
                                                     Console.Write("Enter member id : ");
                                                    int deactivateMemberId;
                                                    while(!int.TryParse(Console.ReadLine(), out deactivateMemberId))
                                                    {
                                                        Console.Write("Invalid input. Please enter a number : ");
                                                    }
                                                    var deactivatedMember = memberService.DeactivateMember(deactivateMemberId);
                                                    Console.WriteLine("Member deactivated successfully.");
                                                    DisplayHelper.PrintMember(deactivatedMember);
                                                    break;
                                                case 7:
                                                    membermanagementMenu = false;
                                                    break;
                                            }
                                        }
                                        break;
                                    case 2:
                                        bool bookManagementMenu = true;
                                        while(bookManagementMenu)
                                        {
                                            int bookChoice =
                                                MenuHelper.BookManagementMenu();

                                            switch(bookChoice)
                                            {
                                               case 1:
                                                Console.Write("Enter title : ");
                                                string title = Console.ReadLine()!;

                                                Console.Write("Enter author : ");
                                                string author = Console.ReadLine()!;

                                                Console.WriteLine();
                                                Console.WriteLine("Select Category");

                                                var categories = bookCategoryService.GetAllCategories();

                                                foreach(var category in categories)
                                                {
                                                    Console.WriteLine($"{category.Id}. {category.CategoryName}");
                                                }

                                                Console.Write("Enter choice : ");
                                                int categoryChoice;
                                                while(!int.TryParse(Console.ReadLine(), out categoryChoice) || !categories.Any(c => c.Id == categoryChoice))
                                                {
                                                    Console.Write("Invalid input. Please enter a valid category id : ");
                                                }
                                                Book book = new Book
                                                {
                                                    Title = title,
                                                    Author = author,
                                                    CategoryId = categoryChoice
                                                };
                                                var createdBook = bookService.AddBook(book);
                                                Console.WriteLine();
                                                Console.WriteLine("Book added successfully.");
                                                int availableCount = bookService.GetAvailableBookCount(createdBook.BookId);
                                                DisplayHelper.PrintBook(createdBook,availableCount);
                                                break;
                                                case 2:
                                                    Console.Write("Enter book id : ");

                                                    int bookId;

                                                    while(!int.TryParse(Console.ReadLine(), out bookId))
                                                    {
                                                        Console.Write("Invalid input. Please enter a number : ");
                                                    }

                                                    Console.Write("Enter number of copies : ");

                                                    int copyCount;

                                                    while(!int.TryParse(Console.ReadLine(), out copyCount) || copyCount <= 0)
                                                    {
                                                        Console.Write("Invalid input. Please enter a valid number : ");
                                                    }

                                                    bookService.AddMultipleCopies(bookId, copyCount);

                                                    Console.WriteLine();
                                                    Console.WriteLine("Book copies added successfully.");

                                                    break;
                                                case 3:
                                                Console.Write("Enter title : ");
                                                string titleSearch = Console.ReadLine()!;

                                                var bookByTitle = bookService.GetBookByTitle(titleSearch);

                                                int availableCountByTitle = bookService.GetAvailableBookCount(bookByTitle.BookId);

                                                DisplayHelper.PrintBook(bookByTitle, availableCountByTitle);
                                                    break;

                                                case 4:
                                                Console.Write("Enter author : ");
                                                string authorSearch = Console.ReadLine()!;

                                                var booksByAuthor = bookService.GetBooksByAuthor(authorSearch);

                                                foreach(var eachbook in booksByAuthor)
                                                {
                                                   int iavailableCount = bookService.GetAvailableBookCount(eachbook.BookId);

                                                    DisplayHelper.PrintBook(eachbook, iavailableCount);
                                                }
                                                break;

                                                case 5:

                                                    Console.WriteLine("Select Category");

                                                    var excategories = bookCategoryService.GetAllCategories();

                                                    foreach(var category in excategories)
                                                    {
                                                        Console.WriteLine($"{category.Id}. {category.CategoryName}");
                                                    }

                                                    Console.Write("Enter category id : ");

                                                    int categoryId;

                                                    while(!int.TryParse(Console.ReadLine(), out categoryId) || !excategories.Any(c => c.Id == categoryId))
                                                    {
                                                        Console.Write("Invalid input. Please enter a valid category id : ");
                                                    }

                                                    string categoryName = excategories.First(c => c.Id == categoryId).CategoryName;

                                                    var booksByCategory = bookService.GetBooksByCategory(categoryName);

                                                    foreach(var foreachbook in booksByCategory)
                                                    {
                                                        int aavailableCount = bookService.GetAvailableBookCount(foreachbook.BookId);
                                                        DisplayHelper.PrintBook(foreachbook,aavailableCount);
                                                    }

                                                    break;
                                                case 6:

                                                    Console.Write("Enter copy id : ");

                                                    int copyId;

                                                    while(!int.TryParse(Console.ReadLine(), out copyId))
                                                    {
                                                        Console.Write("Invalid input. Please enter a number : ");
                                                    }

                                                    var damagedCopy = bookService.MarkBookCopyDamaged(copyId);

                                                    Console.WriteLine();
                                                    Console.WriteLine("Book copy marked as damaged.");

                                                    Console.WriteLine($"Copy Id : {damagedCopy.Id}");
                                                    Console.WriteLine($"Status  : {damagedCopy.CopyStatus}");

                                                    break;

                                                case 7:

                                                    bookManagementMenu = false;

                                                    break;
                                            }
                                        }

                                        break;

                                    case 3:

                                        bool reportMenu = true;
                                        while(reportMenu)
                                        {
                                            int reportChoice = MenuHelper.ReportMenu();

                                            switch(reportChoice)
                                            {
                                                case 1:
                                                var activeBorrowings = reportService.GetCurrentlyBorrowedBooks();

                                                if(activeBorrowings == null || activeBorrowings.Count == 0)
                                                {
                                                    Console.WriteLine("No active borrowings found.");
                                                    break;
                                                }

                                                foreach(var borrowing in activeBorrowings)
                                                {
                                                    DisplayHelper.PrintBorrowing(borrowing);
                                                }
                                                break;
                                                case 2:
                                                    var overdueBooks = reportService.GetOverdueBooks();

                                                    if(overdueBooks == null || overdueBooks.Count == 0)
                                                    {
                                                        Console.WriteLine("No overdue books found.");
                                                        break;
                                                    }

                                                    foreach(var overdueBook in overdueBooks)
                                                    {
                                                        DisplayHelper.PrintOverdueReport(overdueBook);
                                                    }
                                                    break;

                                                case 3:
                                                    var membersWithPendingFines = reportService.GetMembersWithPendingFines();

                                                    if(membersWithPendingFines == null || membersWithPendingFines.Count == 0)
                                                    {
                                                        Console.WriteLine("No members with pending fines found.");
                                                        break;
                                                    }

                                                    foreach(var member in membersWithPendingFines)
                                                    {
                                                        DisplayHelper.PrintMember(member);
                                                    }
                                                    break;
                                                case 4:
                                                     var mostBorrowedBooks = reportService.GetMostBorrowedBooks();
                                                    if(mostBorrowedBooks == null || mostBorrowedBooks.Count == 0)
                                                    {
                                                        Console.WriteLine("No borrowed books data found.");
                                                        break;
                                                    }

                                                    foreach(var borrowedBook in mostBorrowedBooks)
                                                    {
                                                        DisplayHelper.PrintMostBorrowedBook(borrowedBook);
                                                    }
                                                    break;
                                                case 5:

                                                    Console.WriteLine("Select Category");

                                                    var categories = bookCategoryService.GetAllCategories();

                                                    foreach(var category in categories)
                                                    {
                                                        Console.WriteLine($"{category.Id}. {category.CategoryName}");
                                                    }

                                                    Console.Write("Enter category id : ");

                                                    int categoryId;

                                                    while(!int.TryParse(Console.ReadLine(), out categoryId) || !categories.Any(c => c.Id == categoryId))
                                                    {
                                                        Console.Write("Invalid input. Please enter a valid category id : ");
                                                    }

                                                    string categoryName = categories.First(c => c.Id == categoryId).CategoryName;

                                                    var availableBooks = reportService.GetAvailableBooksByCategory(categoryName);

                                                    if(availableBooks == null || availableBooks.Count == 0)
                                                    {
                                                        Console.WriteLine("No available books found.");
                                                        break;
                                                    }

                                                    foreach(var book in availableBooks)
                                                    {
                                                        int cavailableCount = bookService.GetAvailableBookCount(book.BookId);
                                                       DisplayHelper.PrintBook(book, cavailableCount);
                                                    }

                                                    break;
                                                case 6:
                                                    Console.Write("Enter member id : ");

                                                    int memberId;

                                                    while(!int.TryParse(Console.ReadLine(), out memberId))
                                                    {
                                                        Console.Write("Invalid input. Please enter a number : ");
                                                    }

                                                    var borrowingHistory = reportService.GetMemberBorrowingHistory(memberId);

                                                    if(borrowingHistory == null || borrowingHistory.Count == 0)
                                                    {
                                                        Console.WriteLine("No borrowing history found.");
                                                        break;
                                                    }

                                                    foreach(var borrowing in borrowingHistory)
                                                    {
                                                        
                                                        DisplayHelper.PrintBorrowing(borrowing);
                                                    }

                                                    break;

                                                case 7:

                                                    reportMenu = false;

                                                    break;
                                            }
                                        }

                                        break;

                                    case 4:

                                        adminLoggedIn = false;

                                        Console.WriteLine(
                                            "Logged out successfully."
                                        );

                                        break;
                                }
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine();
                                Console.WriteLine(ex.Message);
                                if(ex.InnerException != null)
                                {
                                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine( $"Welcome Member {account.Username}");
                        bool memberLoggedIn = true;
                        while(memberLoggedIn)
                        {
                            try
                            {
                                int memberChoice =
                                    MenuHelper.MemberMenu();

                                switch(memberChoice)
                                {
                                    case 1:
                                    bool viewBooksMenu = true;
                                    while(viewBooksMenu)
                                    {
                                        int viewChoice = MenuHelper.ViewBooksMenu();

                                        switch(viewChoice)
                                        {
                                            case 1:
                                                Console.Write("Enter title : ");
                                                string titleSearch = Console.ReadLine()!;
                                                var bookByTitle = bookService.GetBookByTitle(titleSearch);
                                                int availableCountByTitle = bookService.GetAvailableBookCount(bookByTitle.BookId);
                                                DisplayHelper.PrintBook(bookByTitle,availableCountByTitle);
                                                break;
                                            case 2:
                                                Console.Write("Enter author : ");
                                                string authorSearch = Console.ReadLine()!;
                                                var booksByAuthor = bookService.GetBooksByAuthor(authorSearch);
                                                foreach(var book in booksByAuthor)
                                                {
                                                    int availableCount = bookService.GetAvailableBookCount(book.BookId);

                                                    DisplayHelper.PrintBook(book, availableCount);
                                                }
                                                break;
                                            case 3:
                                                Console.WriteLine("Select Category");

                                                var categories = bookCategoryService.GetAllCategories();

                                                foreach(var category in categories)
                                                {
                                                    Console.WriteLine($"{category.Id}. {category.CategoryName}");
                                                }

                                                Console.Write("Enter category id : ");

                                                int categoryId;

                                                while(!int.TryParse(Console.ReadLine(), out categoryId) || !categories.Any(c => c.Id == categoryId))
                                                {
                                                    Console.Write("Invalid input. Please enter a valid category id : ");
                                                }

                                                string categoryName = categories.First(c => c.Id == categoryId).CategoryName;

                                                var booksByCategory = bookService.GetBooksByCategory(categoryName);

                                                foreach(var book in booksByCategory)
                                                {
                                                    int availableCount = bookService.GetAvailableBookCount(book.BookId);
                                                    DisplayHelper.PrintBook(book, availableCount);

                                                 }

                                                break;

                                            case 4:

                                                viewBooksMenu = false;

                                                break;
                                        }
                                    }
                                    break;
                                    case 2:

                                        Console.WriteLine("Available Books");

                                        var books = bookService.GetAllBooks();

                                        foreach(var book in books)
                                        {
                                            int availableCount = bookService.GetAvailableBookCount(book.BookId);

                                            if(availableCount > 0)
                                            {
                                                DisplayHelper.PrintBook(book, availableCount);
                                            }
                                        }

                                        Console.Write("Enter book id to borrow : ");

                                        int borrowBookId;

                                        while(!int.TryParse(Console.ReadLine(), out borrowBookId))
                                        {
                                            Console.Write("Invalid input. Please enter a number : ");
                                        }

                                        var borrowing = borrowingService.BorrowBook(account.MemberId!.Value, borrowBookId);

                                        Console.WriteLine();
                                        Console.WriteLine("Book borrowed successfully.");

                                        DisplayHelper.PrintBorrowing(borrowing);

                                        break;

                                    case 3:

                                        var activeBorrowings = borrowingService.GetActiveBorrowingsByMemberId(account.MemberId.Value);

                                        if(activeBorrowings == null || activeBorrowings.Count == 0)
                                        {
                                            Console.WriteLine("No active borrowings found.");
                                            break;
                                        }

                                        Console.WriteLine("Your Active Borrowings");

                                        foreach(var iborrowing in activeBorrowings)
                                        {
                                            DisplayHelper.PrintBorrowing(iborrowing);
                                        }

                                        Console.Write("Enter borrowing id to return : ");

                                        int borrowingId;

                                        while(!int.TryParse(Console.ReadLine(), out borrowingId))
                                        {
                                            Console.Write("Invalid input. Please enter a number : ");
                                        }

                                        Console.Write("Pay fine now? (Y/N) : ");

                                        string choice = Console.ReadLine()!.ToUpper();

                                        bool payFineNow = choice == "Y";

                                        var returnedBorrowing = borrowingService.ReturnBook(borrowingId, payFineNow);

                                        Console.WriteLine();
                                        Console.WriteLine("Book returned successfully.");

                                        DisplayHelper.PrintBorrowing(returnedBorrowing);

                                        break;

                                    case 4:
                                        bool fineMenu = true;

                                        while(fineMenu)
                                        {
                                            int fineChoice = MenuHelper.FineManagementMenu();

                                            switch(fineChoice)
                                            {
                                                case 1:
                                                  decimal pendingFine = borrowingService.GetPendingFineByMemberId(account.MemberId.Value);

                                                Console.WriteLine();
                                                Console.WriteLine($"Pending Fine : {pendingFine}");

                                                break;

                                                case 2:
                                                    var unpaidBorrowings = borrowingService
                                                            .GetBorrowingHistoryByMemberId(account.MemberId.Value)
                                                            .Where(b => b.FineSettled == false)
                                                            .ToList();

                                                        if(unpaidBorrowings.Count == 0)
                                                        {
                                                            Console.WriteLine("No pending fines found.");
                                                            break;
                                                        }

                                                        Console.WriteLine("Pending Fine Borrowings");

                                                        foreach(var iborrowing in unpaidBorrowings)
                                                        {
                                                            DisplayHelper.PrintBorrowing(iborrowing);
                                                        }

                                                        Console.Write("Enter borrowing id to pay fine : ");

                                                        int eborrowingId;

                                                        while(!int.TryParse(Console.ReadLine(), out eborrowingId))
                                                        {
                                                            Console.Write("Invalid input. Please enter a number : ");
                                                        }

                                                        borrowingService.PayFine(eborrowingId);

                                                        Console.WriteLine("Fine paid successfully.");
                                                    break;
                                                case 3:

                                                var borrowingHistory = borrowingService.GetBorrowingHistoryByMemberId(account.MemberId.Value);

                                                var finePayments = borrowingHistory
                                                    .Where(b => b.FinePayment != null)
                                                    .Select(b => b.FinePayment)
                                                    .ToList();

                                                if(finePayments.Count == 0)
                                                {
                                                    Console.WriteLine("No fine payment history found.");
                                                    break;
                                                }

                                                foreach(var payment in finePayments)
                                                {
                                                    DisplayHelper.PrintFinePayment(payment!);
                                                }

                                                break;

                                                case 4:

                                                    fineMenu = false;

                                                    break;
                                            }
                                        }

                                        break;
                                    case 5:

                                        memberLoggedIn = false;

                                        Console.WriteLine(
                                            "Logged out successfully."
                                        );

                                        break;
                                }
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine();
                                Console.WriteLine(ex.Message);
                                if(ex.InnerException != null)
                                {
                                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}