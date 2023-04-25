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
                    FullName = "Kamisato Ayaka",
                    UrlSlug = "ksmi-yaya",
                    Email = "Ayaya@gmail.com",
                    JoinedDate = new DateTime(2021, 4, 12)
                },
                  new()
                {
                    FullName = "Raiden Shogun",
                    UrlSlug = "raiden-shogun",
                    Email = "baal@gmail.com",
                    JoinedDate = new DateTime(2019, 1, 25)
                },
                   new()
                {
                    FullName = "Xiao Changmeng",
                    UrlSlug = "xiao-changmeng",
                    Email = "xiaono1@gmail.com",
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
                new()
                {
                    Name = "Lol",
                    Description ="Lien minh",
                    UrlSlug = "lol",
                    ShowOnMenu = true
                },
                new()
                {
                    Name = "GI",
                    Description = "Genshin",
                    UrlSlug = "GI",
                    ShowOnMenu = true
                },
                new()
                {
                    Name = "HonKa",
                    Description = "Honkai Impact",
                    UrlSlug = "HonKa",
                    ShowOnMenu = true
                },
                new()
                {
                    Name = "StarRail",
                    Description = "Honkai StarRail",
                    UrlSlug = "Star",
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
                    Name = "Data Science",
                    Description = "Data Science",
                    UrlSlug = "data-science"
                },
                new()
                {
                    Name = "Docker",
                    Description = "Docker",
                    UrlSlug = "docker"
                },
                new()
                {
                    Name = "Kubernetes",
                    Description = "Kubernetes",
                    UrlSlug = "kubernetes"
                },
                new()
                {
                    Name = "Machine Learning",
                    Description = "Machine Learning",
                    UrlSlug = "machine-learning"
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
                        tags[3]
                    }
                },

                new()
                {
                    Title = "Genshin Impact",
                    ShortDescription = "Genshin Impact is an open-world, action role-playing game that allows the player to control one of four interchangeable characters in a party",
                    Description = "What is Genshin ",
                    Meta = "Switching between characters can be done quickly during combat, allowing the player to use several different combinations of skills and attacks",
                    UrlSlug = "GenshinIm",
                    Published = true,
                    PostedDate = new DateTime(2019,7,12,10,20,0),
                    ModifiedDate = null,
                    ViewCount = 41,
                    Author = authors[3],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[0]
                    }
                },
                new()
                {
                    Title = "Games and Rules",
                    ShortDescription = "Why do we play games and why do we play them on computers? The contributors of Games and Rules take a closer look at the core of each game and the motivational system that is the game mechanics.",
                    Description = "Game Mechanics for the Magic Circle",
                    Meta = "Why do we play games and why do we play them on computers? The contributors of Games and Rules take a closer look at the core of each game and the motivational system that is the game mechanics.",
                    UrlSlug = "games-and-rules",
                    Published = true,
                    PostedDate = new DateTime(2019,1,2,10,20,0),
                    ModifiedDate = null,
                    ViewCount = 42,
                    Author = authors[4],
                    Category = categories[10],
                    Tags = new List<Tag>()
                    {
                        tags[7]
                    }
                },
                new()
                {
                    Title = "Learning React Native",
                    ShortDescription = "React Native is an open-source mobile application framework created by Facebook.",
                    Description = "React Native",
                    Meta = "React Native is an open-source mobile application framework created by Facebook.",
                    UrlSlug = "learning-react-native",
                    Published = true,
                    PostedDate = new DateTime(2019,9,22,10,20,0),
                    ModifiedDate = null,
                    ViewCount = 31,
                    Author = authors[3],
                    Category = categories[8],
                    Tags = new List<Tag>()
                    {
                        tags[7]
                    }
                },
                new()
                {
                    Title = "C# Notes for Professionals",
                    ShortDescription = "The C# Notes for Professionals book is compiled from Stack Overflow Documentation",
                    Description = "C#",
                    Meta = "The C# Notes for Professionals book is compiled from Stack Overflow Documentation",
                    UrlSlug = "csharp-notes-for-professionals",
                    Published = true,
                    PostedDate = new DateTime(2019,6,11,10,20,0),
                    ModifiedDate = null,
                    ViewCount = 12,
                    Author = authors[6],
                    Category = categories[9],
                    Tags = new List<Tag>()
                    {
                        tags[5]
                    }
                },
                new()
                {
                    Title = "Gameplay, Emotions and Narrative",
                    ShortDescription = "This book is devoted to emotional and narrative immersion in the experience of gameplay.",
                    Description = "Independent Games Experienced",
                    Meta = "This book is devoted to emotional and narrative immersion in the experience of gameplay.",
                    UrlSlug = "gameplay-emotions-and-narrative",
                    Published = true,
                    PostedDate = new DateTime(2019,5,27,10,20,0),
                    ModifiedDate = null,
                    ViewCount = 15,
                    Author = authors[0],
                    Category = categories[6],
                    Tags = new List<Tag>()
                    {
                        tags[6]
                    }
                },
                new()
                {
                    Title = "Introduction to Data Science",
                    ShortDescription = "The demand for skilled data science practitioners in industry, academia, and government is rapidly growing.",
                    Description = "Data Analysis and Prediction Algorithms with R",
                    Meta = "The demand for skilled data science practitioners in industry, academia, and government is rapidly growing.",
                    UrlSlug = "introduction-to-data-science",
                    Published = true,
                    PostedDate = new DateTime(2019,9,30,10,20,0),
                    ModifiedDate = null,
                    ViewCount = 13,
                    Author = authors[1],
                    Category = categories[7],
                    Tags = new List<Tag>()
                    {
                        tags[8]
                    }
                },
                new()
                {
                    Title = "HTML5 Canvas Notes for Professionals",
                    ShortDescription = "The HTML5 Canvas Notes for Professionals book",
                    Description = "HTML5 Canvas",
                    Meta = "The HTML5 Canvas Notes for Professionals book",
                    UrlSlug = "html5-canvas-notes-for-professionals",
                    Published = true,
                    PostedDate = new DateTime(2019,3,20,10,20,0),
                    ModifiedDate = null,
                    ViewCount = 11,
                    Author = authors[1],
                    Category = categories[5],
                    Tags = new List<Tag>()
                    {
                        tags[9]
                    }
                },
                new()
                {
                    Title = "Code Club Book of Scratch",
                    ShortDescription = "The first ever Code Club book is here! With it, you'll learn how to code using Scratch, the block-based programming language",
                    Description = "Simple coding for total beginners",
                    Meta = "The first ever Code Club book is here! With it, you'll learn how to code using Scratch, the block-based programming language",
                    UrlSlug = "code-clug-book-of-scratch",
                    Published = true,
                    PostedDate = new DateTime(2018,9,11,10,20,0),
                    ModifiedDate = null,
                    ViewCount = 20,
                    Author = authors[0],
                    Category = categories[8],
                    Tags = new List<Tag>()
                    {
                        tags[3]
                    }
                },
                new()
                {
                    Title = "Coding with Minecraft",
                    ShortDescription = "In Coding with Minecraft, you'll create a virtual robot army with Lua, a programming language used by professional game developers.",
                    Description = "Build Taller, Farm Faster, Mine Deeper, and Automate the Boring Stuff",
                    Meta = "In Coding with Minecraft, you'll create a virtual robot army with Lua, a programming language used by professional game developers.",
                    UrlSlug = "coding-with-minecraft",
                    Published = true,
                    PostedDate = new DateTime(2018,9,12,10,20,0),
                    ModifiedDate = null,
                    ViewCount = 4,
                    Author = authors[4],
                    Category = categories[2],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
                new()
                {
                    Title = "PHP Notes for Professionals",
                    ShortDescription = "The PHP Notes for Professionals book ",
                    Description = "PHP Notes",
                    Meta = "The PHP Notes for Professionals book ",
                    UrlSlug = "php-notes-for-professionals",
                    Published = true,
                    PostedDate = new DateTime(2018,12,20,10,20,0),
                    ModifiedDate = null,
                    ViewCount = 10,
                    Author = authors[0],
                    Category = categories[8],
                    Tags = new List<Tag>()
                    {
                        tags[4]
                    }
                },
            };

            _dbContext.AddRange(posts);
            _dbContext.SaveChanges();

            return posts;
        }
    }
}
