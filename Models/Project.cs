namespace MyApi.Models
{
    public class DSProject
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? CodeFile { get; set; } = null!;
        public string? Image { get; set; } = null!;
    }
    public class DBProject
    {
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? CodeFile { get; set; } = null!;
    public string? Image { get; set; }
}
}
