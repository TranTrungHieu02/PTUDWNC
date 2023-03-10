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
                new()
                {
                    FullName = "Alice Jessi",
                    UrlSlug = "alice-jessi",
                    Email = "jessalice22@gmail.com",
                    JoinedDate = new DateTime(2019, 4, 4)
                },
                new()
                {
                    FullName = "Mira John",
                    UrlSlug = "mira-john",
                    Email = "miraaaa2848@gmail.com",
                    JoinedDate = new DateTime(2023, 1, 8)
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
                    Name = "Messaging",
                    Description = "Messaging",
                    UrlSlug = "messaging",
                    ShowOnMenu = true
                },
                new()
                {
                    Name = "OOP",
                    Description = "Object-Oriented Programming",
                    UrlSlug = "oop",
                    ShowOnMenu = true
                },
                new()
                {
                    Name = "Design Patterns",
                    Description = "Design Patterns",
                    UrlSlug = "design-patterns",
                    ShowOnMenu = true
                },
                new()
                {
                    Name = "Front-End",
                    Description = "Front-End",
                    UrlSlug = "front-end",
                    ShowOnMenu = true
                },
                new()
                {
                    Name = "Python",
                    Description = "Python",
                    UrlSlug = "python",
                    ShowOnMenu = true
                },
                new()
                {
                    Name = "Java",
                    Description = "Java",
                    UrlSlug = "java",
                    ShowOnMenu = true
                },
                new()
                {
                    Name = "Backend",
                    Description = "Backend",
                    UrlSlug = "backend",
                    ShowOnMenu = true
                },
                new()
                {
                    Name = "Git/Github",
                    Description = "Git/Github",
                    UrlSlug = "git-github",
                    ShowOnMenu = true
                },
                new()
                {
                    Name = "Graphic Design",
                    Description = "Graphic Design",
                    UrlSlug = "graphic-design",
                    ShowOnMenu = true
                },
                new()
                {
                    Name = "C/C++",
                    Description = "C/C++",
                    UrlSlug = "c-cplusplus",
                    ShowOnMenu = true
                },
                new()
                {
                    Name = "Games",
                    Description = "Games",
                    UrlSlug = "games",
                    ShowOnMenu = true
                },
                new()
                {
                    Name = "Data Science",
                    Description = "Data Science",
                    UrlSlug = "data-science",
                    ShowOnMenu = true
                },
                new()
                {
                    Name = "Full-Stack",
                    Description = "Full-Stack",
                    UrlSlug = "full-stack",
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
                    Name = "ASP.NET MVC",
                    Description = "ASP.NET MVC",
                    UrlSlug = "aspnet-mvc"
                },
                new()
                {
                    Name = "Razor Page",
                    Description = "Razor Page",
                    UrlSlug = "razor-page"
                },
               
                new()
                {
                    Name = "JavaScript",
                    Description = "JavaScript",
                    UrlSlug = "javascript"
                },
                new()
                {
                    Name = "React",
                    Description = "React",
                    UrlSlug = "react"
                },
                new()
                {
                    Name = "Python",
                    Description = "Python",
                    UrlSlug = "python"
                },
                new()
                {
                    Name = "Java",
                    Description = "Java",
                    UrlSlug = "java"
                },
                new()
                {
                    Name = "PHP",
                    Description = "PHP",
                    UrlSlug = "php"
                },
                new()
                {
                    Name = "NodeJs",
                    Description = "NodeJs",
                    UrlSlug = "nodejs"
                },
                new()
                {
                    Name = "Ruby",
                    Description = "Ruby",
                    UrlSlug = "ruby"
                },
                new()
                {
                    Name = "Git",
                    Description = "Git",
                    UrlSlug = "git"
                },
                new()
                {
                    Name = "Photoshop",
                    Description = "Photoshop",
                    UrlSlug = "photoshop"
                },
                
                new()
                {
                    Name = "C#",
                    Description = "C#",
                    UrlSlug = "c-sharp"
                },
                new()
                {
                    Name = "HTML",
                    Description = "HTML",
                    UrlSlug = "html"
                },
                new()
                {
                    Name = "CSS",
                    Description = "CSS",
                    UrlSlug = "css"
                },
                new()
               
                {
                    Name = "Windows",
                    Description = "Windows",
                    UrlSlug = "windows"
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
                    Title = "Optimizing Software In C++",
                    ShortDescription = "This is an optimization manual for advanced C++ programmers",
                    Description = "This book are not for beginners",
                    Meta = "This is an optimization manual for advanced C++ programmers",
                    UrlSlug = "optimizing-software-in-cplusplus",
                    Published = true,
                    PostedDate = new DateTime(2020,8,21,10,20,0),
                    ModifiedDate = null,
                    ViewCount = 17,
                    Author = authors[3],
                    Category = categories[11],
                    Tags = new List<Tag>()
                    {
                        tags[16]
                    }
                },
                
                new()
                {
                    Title = "Learning Java",
                    ShortDescription = "Java is a class-based, object-oriented programming language that is designed to have as few implementation dependencies as possible",
                    Description = "Java Learning",
                    Meta = "Java is a class-based, object-oriented programming language that is designed to have as few implementation dependencies as possible",
                    UrlSlug = "learning-java",
                    Published = true,
                    PostedDate = new DateTime(2019,7,12,10,20,0),
                    ModifiedDate = null,
                    ViewCount = 41,
                    Author = authors[3],
                    Category = categories[7],
                    Tags = new List<Tag>()
                    {
                        tags[9]
                    }
                },
                
                new()
                {
                    Title = "Python Notes for Professionals",
                    ShortDescription = "The Python Notes for Professionals book",
                    Description = "Python Notes",
                    Meta = "The Python Notes for Professionals book",
                    UrlSlug = "python-notes-for-professionals",
                    Published = true,
                    PostedDate = new DateTime(2018,10,1,10,20,0),
                    ModifiedDate = null,
                    ViewCount = 10,
                    Author = authors[4],
                    Category = categories[6],
                    Tags = new List<Tag>()
                    {
                        tags[8]
                    }
                },
            };

            _dbContext.AddRange(posts);
            _dbContext.SaveChanges();

            return posts;
        }
    }
}
