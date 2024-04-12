namespace ApplicationAdmin.Contracts.Abstractions;
public record ResponseData<T> where T: class
{
    public List<T> Data { get; set; } = new();
    public int TotalCount { get; set; } = 0;
    public int CurrentPage { get; set; } = 0;
}
