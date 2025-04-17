namespace MyApi.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string CodeFile { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
