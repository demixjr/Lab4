using System.Data.Entity;
namespace DAL
{
    public class BoardContext: DbContext
    {
        public BoardContext() : base("BoardDB") 
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<BoardContext>());
        }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Heading> Headings { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Username);

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasMaxLength(32)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasMaxLength(64)
                .IsRequired();



            modelBuilder.Entity<Announcement>()
                .HasKey(a => a.AnnouncementId);

            modelBuilder.Entity<Announcement>()
                .Property(a => a.Title)
                .HasMaxLength(32);

            modelBuilder.Entity<Announcement>()
                .Property(a => a.Description)
                .HasMaxLength(512)
                .IsRequired();

            modelBuilder.Entity<Announcement>()
                .HasRequired(a => a.User)
                .WithMany(u => u.Announcements)
                .HasForeignKey(a => a.Username)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Announcement>()
                .HasRequired(a => a.Category)
                .WithMany(c => c.Announcements)
                .HasForeignKey(a => a.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Announcement>()
                .HasRequired(a => a.Subcategory)
                .WithMany(s => s.Announcements)
                .HasForeignKey(a => a.SubcategoryId)
                .WillCascadeOnDelete(false);

         


            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryId);

            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .HasMaxLength(32)
                .IsRequired();

            modelBuilder.Entity<Category>()
                .HasRequired(c => c.Heading)
                .WithMany(h => h.Categories)
                .HasForeignKey(c => c.HeadingId)
                .WillCascadeOnDelete(false);



            modelBuilder.Entity<Subcategory>()
                .HasKey(s => s.SubcategoryId);

            modelBuilder.Entity<Subcategory>()
                .Property(s => s.Name)
                .HasMaxLength(32)
                .IsRequired();

            modelBuilder.Entity<Subcategory>()
                .HasRequired(s => s.Category)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(s => s.CategoryId)
                .WillCascadeOnDelete(false);

            


            modelBuilder.Entity<Heading>()
                .HasKey(h => h.HeadingId);

            modelBuilder.Entity<Heading>()
                .Property(h => h.Name)
                .HasMaxLength(32)
                .IsRequired();

            


            modelBuilder.Entity<Tag>()
                .HasKey(t => t.TagId);

            modelBuilder.Entity<Tag>()
                .Property(t => t.Name)
                .HasMaxLength(32)
                .IsRequired();

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.Announcements)
                .WithMany(a => a.Tags)
                .Map(at =>
                {
                    at.ToTable("AnnouncementTags");
                    at.MapLeftKey("TagId");
                    at.MapRightKey("AnnouncementId");
                });
        }

    }
}
