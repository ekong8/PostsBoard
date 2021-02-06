using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using PostsBoard.Models;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace PostsBoard.Data
{
    public class PostDbContext : DbContext
    {
        public IConfiguration Configuration { get; set; }
        public IWebHostEnvironment HostEnvironment { get; set; }

        public PostDbContext(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            HostEnvironment = environment;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> Comments { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTagAssignment> PostTagAssignments { get; set; }
        public DbSet<PostTag> PostTags { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string connectionString;
            if (HostEnvironment.IsDevelopment())
            {
                connectionString = Configuration.GetConnectionString("MainDB");
            }
            else
            {
                connectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING");
            }

           options.UseMySQL(connectionString)
                    .UseLazyLoadingProxies();
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
