namespace LibraryManagementModelLibrary.Models;

public class BookCategory
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public ICollection<Book>? Books { get; set; }

    public BookCategory()
    {


    }
    public BookCategory(string categoryName, string? description)
    {
        CategoryName = categoryName;
        Description = description;
    }
    public override string ToString()
    {
        return $"Id : {Id}\n" +
            $"Category Name : {CategoryName}\n" +
            $"Description : {Description}";
    }
}
