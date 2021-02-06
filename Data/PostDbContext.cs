using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using PostsBoard.Models;

namespace PostsBoard.Data
{
    public class PostDbContext : DbContext
    {
        public IConfiguration Configuration { get; set; }
        public PostDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> Comments { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTagAssignment> PostTagAssignments { get; set; }
        public DbSet<PostTag> PostTags { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
           options.UseMySQL
           (
                 Configuration.GetConnectionString("MainDB")
           ).UseLazyLoadingProxies();
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*
            builder.Entity<Post>(e => {
                e.HasOne<PostCategory>()  //post 입장
                .WithMany()             // postCategory 입장
                .HasForeignKey(p => p.CategoryID);
            });

            */
            builder.Entity<PostTagAssignment>(e =>
            {
                e.HasKey(a => new { a.PostID, a.TagID });
                e.HasOne<Post>()
                .WithMany()
                .HasForeignKey(p => p.PostID);

                e.HasOne<PostTag>()
                .WithMany()
                .HasForeignKey(p => p.TagID);
            });

         
        }
        
    }
}
