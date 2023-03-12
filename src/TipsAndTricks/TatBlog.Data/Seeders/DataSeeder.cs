using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;

namespace TatBlog.Data.Seeders
{
    public class DataSeeder : IDataSeeder
    {
        private readonly BlogDbContext _dbContext;

        public DataSeeder(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            _dbContext.Database.EnsureCreated();
            if (_dbContext.Posts.Any()) return;

            var authors = AddAuthors();
            var categories = AddCategories();
            var tags = AddTags();
            var posts = AddPosts(authors, categories, tags);
        }

        private IList<Author> AddAuthors()
        {
            var authors = new List<Author>()
            {
                new()
                {
                    FullName = "Jason Mouth",
                    UrlSlug = "jason-mouth",
                    Email = "json@gmail.com",
                    JoinedDate = new DateTime(2022, 10, 21)
                },
                new()
                {
                    FullName = "Jessica Wonder",
                    UrlSlug = "jessica-wonder",
                    Email = "jessica665@motip.com",
                    JoinedDate = new DateTime(2020, 4, 19)
                },
                new()
                {
                    FullName = "John Wasson",
                    UrlSlug = "john-wasson",
                    Email = "johnwas098@gmail.com",
                    JoinedDate = new DateTime(2021, 12, 24)
                },


            };
            _dbContext.Authors.AddRange(authors);
            _dbContext.SaveChanges();

            return authors;
        }

        private IList<Category> AddCategories()
        {
            var categories = new List<Category>()
            {
                new()
                {
                    Name = ".NET Core",
                    Description = ".NET Core",
                    UrlSlug = "net-core",
                    ShowOnMenu = true
                },
                new()
                {
                    Name = "Architecture",
                    Description = "Architecture",
                    UrlSlug = "architecture",
                    ShowOnMenu = true
                },

                new()
                {
                    Name = "Java",
                    Description = "Java",
                    UrlSlug = "java",
                    ShowOnMenu = true
                },

            };

            _dbContext.AddRange(categories);
            _dbContext.SaveChanges();

            return categories;
        }

        private IList<Tag> AddTags()
        {
            var tags = new List<Tag>()
            {
                new()
                {
                    Name = "Google",
                    Description = "Google applications",
                    UrlSlug = "google"
                },

                new()
                {
                    Name = "JavaScript",
                    Description = "JavaScript",
                    UrlSlug = "javascript"
                },

                new()
                {
                    Name = "Java",
                    Description = "Java",
                    UrlSlug = "java"
                },

            };

            _dbContext.AddRange(tags);
            _dbContext.SaveChanges();

            return tags;
        }

        private IList<Post> AddPosts(
            IList<Author> authors,
            IList<Category> categories,
            IList<Tag> tags)
        {
            var posts = new List<Post>()
            {
                new()
                {
                    Title = "ASP.NET Core Diagnostic Scenarios",
                    ShortDescription = "David and friends has a great repository filled",
                    Description = "Here's a few great DON'T and DO examples",
                    Meta = "David and friends has a great repository filled",
                    UrlSlug = "aspnet-core-diagnostic-scenarios",
                    Published = true,
                    PostedDate = new DateTime(2021,9,30,10,20,0),
                    ModifiedDate = null,
                    ViewCount = 10,
                    Author = authors[0],
                    Category = categories[0],
                    Tags = new List<Tag>()
                    {
                        tags[1]
                    }
                },

                new()
                {
                    Title = "Java Everywhere Again with DukeScript",
                    ShortDescription = "Java Everywhere Again with DukeScript",
                    Description = "Java Everywhere Again with DukeScript",
                    Meta = "Java Everywhere Again with DukeScript",
                    UrlSlug = "java-everywhere-again-with-dukescript",
                    Published = true,
                    PostedDate = new DateTime(2015,8,22,10,20,0),
                    ModifiedDate = null,
                    ViewCount = 72,
                    Author = authors[1],
                    Category = categories[7],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },


            };

            _dbContext.AddRange(posts);
            _dbContext.SaveChanges();

            return posts;
        }
    }
}