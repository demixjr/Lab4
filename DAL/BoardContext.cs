using System.Data.Entity;
namespace DAL
{
    public class BoardContext: DbContext
    {
        public BoardContext() : base("BoardDB") 
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<BoardContext>());
        }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Heading> Headings { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
       
    }
}
