namespace SimpleCRUD_WinForms.Models;

public struct Product
{
    public int Id { get; init; }
    public string Code { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTime Date { get; init; }
}