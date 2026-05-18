using LibraryManagementModelLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementDLLibrary.Context
{
    public class LibraryContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=librarydb;Username=postgres;Password=vbnm");
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<FinePayment> FinePayments { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(b =>
            {
                b.HasKey(b => b.BookId).HasName("Book_pk");
                b.Property(b => b.ISBN)
                    .IsRequired();
                b.HasIndex(b => b.ISBN)
                    .IsUnique();
                b.Property(b => b.Title).IsRequired();
                b.Property(b => b.Author).IsRequired();
                b.Property(b => b.PublisherName).IsRequired(false);
                b.Property(b => b.PublishedOn)
                        .IsRequired(false)
                         .HasColumnType("timestamp without time zone");


                b.HasOne(b => b.BookCategory)
                    .WithMany(bc => bc.Books)
                    .HasForeignKey(b => b.CategoryId)
                    .HasConstraintName("Book_CategoryId_FK")
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasData(
                    new Book
                    {
                        BookId = 1,
                        ISBN = "ISBN001",
                        Title = "Clean Code",
                        Author = "Robert Martin",
                        CategoryId = 3,
                        PublisherName = "Prentice Hall",
                        PublishedOn = new DateTime(2008, 1, 1)
                    },
                    new Book
                    {
                        BookId = 2,
                        ISBN = "ISBN002",
                        Title = "The Pragmatic Programmer",
                        Author = "Andrew Hunt",
                        CategoryId = 3,
                        PublisherName = "Addison Wesley",
                        PublishedOn = new DateTime(1999, 1, 1)
                    },
                    new Book
                    {
                        BookId = 3,
                        ISBN = "ISBN003",
                        Title = "Harry Potter",
                        Author = "J.K Rowling",
                        CategoryId = 1,
                        PublisherName = "Bloomsbury",
                        PublishedOn = new DateTime(1997, 1, 1)
                    },
                    new Book
                    {
                        BookId = 4,
                        ISBN = "ISBN004",
                        Title = "Brief History of Time",
                        Author = "Stephen Hawking",
                        CategoryId = 2,
                        PublisherName = "Bantam Books",
                        PublishedOn = new DateTime(1988, 1, 1)
                    },
                    new Book
                    {
                        BookId = 5,
                        ISBN = "ISBN005",
                        Title = "Atomic Habits",
                        Author = "James Clear",
                        CategoryId = 1,
                        PublisherName = "Penguin",
                        PublishedOn = new DateTime(2018, 1, 1)
                    }
                );
            }
            );
            modelBuilder.Entity<BookCategory>(bc =>
            {
                bc.HasKey(bc => bc.Id).HasName("BookCategory_pk");
                bc.Property(bc => bc.CategoryName).IsRequired();
                bc.Property(bc => bc.Description).IsRequired(false);bc.HasData(
                    new BookCategory
                    {
                        Id = 1,
                        CategoryName = "Fiction",
                        Description = "Fictional books"
                    },
                    new BookCategory
                    {
                        Id = 2,
                        CategoryName = "Science",
                        Description = "Science related books"
                    },
                    new BookCategory
                    {
                        Id = 3,
                        CategoryName = "Technology",
                        Description = "Programming and technology books"
                    }
                );

            });
            modelBuilder.Entity<BookCopy>(bc =>
            {
                bc.HasKey(bc => bc.Id).HasName("BookCopy_PK");
                bc.Property(bc => bc.CopyStatus).HasConversion<string>()
                    .IsRequired();

                bc.HasOne(bc => bc.Book)
                    .WithMany(b => b.BookCopies)
                    .HasForeignKey(bc => bc.BookId)
                    .HasConstraintName("BookCopy_BookId_FK")
                    .OnDelete(DeleteBehavior.Restrict);
                bc.HasData(
                    new BookCopy { Id = 1, BookId = 1, CopyStatus = BookCopyStatus.Available },
                    new BookCopy { Id = 2, BookId = 1, CopyStatus = BookCopyStatus.Available },

                    new BookCopy { Id = 3, BookId = 2, CopyStatus = BookCopyStatus.Available },
                    new BookCopy { Id = 4, BookId = 2, CopyStatus = BookCopyStatus.Available },

                    new BookCopy { Id = 5, BookId = 3, CopyStatus = BookCopyStatus.Available },
                    new BookCopy { Id = 6, BookId = 3, CopyStatus = BookCopyStatus.Available },

                    new BookCopy { Id = 7, BookId = 4, CopyStatus = BookCopyStatus.Available },
                    new BookCopy { Id = 8, BookId = 5, CopyStatus = BookCopyStatus.Available }
                );
            });
            modelBuilder.Entity<Member>(m =>
            {
                m.HasKey(x => x.Id);

                m.Property(x => x.Name)
                .IsRequired();

                m.Property(x => x.Email)
                .IsRequired();

                m.Property(x => x.PhoneNumber)
                .IsRequired();

                m.Property(x => x.MemberType)
                .HasConversion<string>()
                .IsRequired();

                m.Property(x => x.IsActive)
                .HasDefaultValue(true);

                m.Property(x => x.CreatedAt).HasColumnType("timestamp without time zone");

                m.HasIndex(x => x.Email)
                .IsUnique();

                m.HasIndex(x => x.PhoneNumber)
                .IsUnique();
                m.HasData(
                    new Member
                    {
                        Id = 1,
                        Name = "Bhargav",
                        Email = "bhargav@gmail.com",
                        PhoneNumber = "9999999991",
                        MemberType = MemberShipType.Premium,
                        IsActive = true,
                        CreatedAt = new DateTime(2026, 5, 15)
                    },
                    new Member
                    {
                        Id = 2,
                        Name = "Rahul",
                        Email = "rahul@gmail.com",
                        PhoneNumber = "9999999992",
                        MemberType = MemberShipType.Basic,
                        IsActive = true,
                        CreatedAt = new DateTime(2026, 5, 15)
                    }
                );

                m.HasMany(x => x.Borrowings)
                .WithOne(x => x.Member)
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Borrowing>(b =>
            {
                b.HasKey(x => x.Id).HasName("Borrowing_PK");
                b.Property(x => x.BorrowedOn).HasColumnType("timestamp without time zone")
                .IsRequired();
                b.Property(x => x.DueDate).HasColumnType("timestamp without time zone")
                .IsRequired();
                b.Property(x=>x.ReturnedOn).HasColumnType("timestamp without time zone");
                b.Property(x => x.FineSettled)
                .HasDefaultValue(true);
                b.Property(x => x.WasDamagedOnIssue)
                    .HasDefaultValue(false);

                b.HasOne(x => x.Member)
                .WithMany(x => x.Borrowings)
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.BookCopy)
                .WithMany(x => x.Borrowings)
                .HasForeignKey(x => x.BookCopyId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<FinePayment>(f =>
            {
                f.HasKey(x => x.Id).HasName("FinePayment_PK");

                f.Property(x => x.AmountPaid)
                .HasPrecision(10, 2)
                .IsRequired();

                f.Property(x => x.PaidOn)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

                f.HasOne(x => x.Borrowing)
                .WithOne(x => x.FinePayment)
                .HasForeignKey<FinePayment>(x => x.BorrowingId)
                .HasConstraintName("FinePayment_BorrowingId_FK")
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<UserAccount>(u =>
            {
                u.HasKey(x => x.Id).HasName("UserAccount_PK");

                u.Property(x => x.Username)
                .IsRequired();

                u.Property(x => x.PassWord)
                .IsRequired();

                u.Property(x => x.Role)
                .HasConversion<string>()
                .IsRequired();

                u.Property(x => x.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

                u.HasIndex(x => x.Username)
                .IsUnique();
                u.HasData(
                    new UserAccount
                    {
                        Id = 1,
                        Username = "admin",
                        PassWord = "admin123",
                        Role = UserRole.Admin,
                        MemberId = null,
                        CreatedOn = new DateTime(2026, 5, 15)
                    },
                    new UserAccount
                    {
                        Id = 2,
                        Username = "bhargav",
                        PassWord = "bhargav123",
                        Role = UserRole.Member,
                        MemberId = 1,
                        CreatedOn = new DateTime(2026, 5, 15)
                    },
                    new UserAccount
                    {
                        Id = 3,
                        Username = "rahul",
                        PassWord = "rahul123",
                        Role = UserRole.Member,
                        MemberId = 2,
                        CreatedOn = new DateTime(2026, 5, 15)
                    }
                );
                u.HasOne(x => x.Member)
                .WithOne()
                .HasForeignKey<UserAccount>(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}