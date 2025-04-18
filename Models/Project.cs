namespace MyApi.Models
{
    public class Projects
    {
        public List<DBProject> dbProject { get; set; }
        public List<DSProject> dsProject { get; set; }
    }
    
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
