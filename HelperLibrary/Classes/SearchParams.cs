namespace HelperLibrary.Classes;
public class SearchParams : PaginationParams
{
    public string? OrderBy { get; set; }
    public bool Asc { get; set; } = true;
    public string? SearchTerm { get; set; }
    public string? ColumnFilters { get; set; }
}

