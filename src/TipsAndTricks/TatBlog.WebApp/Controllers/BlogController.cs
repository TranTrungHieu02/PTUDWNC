﻿using Microsoft.AspNetCore.Mvc;
using TatBlog.Core;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IAuthorRepository _authorRepository;
        public BlogController(IBlogRepository blogRepository, IAuthorRepository authorRepository)
        {
            _blogRepository = blogRepository;
            _authorRepository = authorRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(
            [FromQuery(Name = "k")] string keyword = null,
            [FromQuery(Name ="p")] int pageNumber = 1,
            [FromQuery(Name ="ps")] int pageSize =10)
        {
            
            var postQuery = new PostQuery()
            {
                
                PublishedOnly = true,
                KeyWord = keyword,
            };
            IPagingParams pagingParams = CreatePagingParamsForPost(pageNumber, pageSize);
            //Truy vấn các bài viết theo điều kiện đã tạo
            var postsList = await _blogRepository.GetPagesPostQueryAsync(postQuery, pagingParams);
            //Lưu lại điều kiện truy vấn để hiển thị trong View
            ViewBag.PostQuery = postQuery;
            return View(postsList);
        }
        public async Task<IActionResult> Category(
            string slug,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 2)
        {
            var category = await _blogRepository.GetCategoryBySlugAsync(slug);
            var postQuery = new PostQuery()
            {
                PublishedOnly = true,
                CategorySlug = slug,
                CategoryName = category.Name,
            };
            IPagingParams pagingParams = CreatePagingParamsForPost(pageNumber, pageSize);
            var postsList = await _blogRepository.GetPagesPostQueryAsync(postQuery, pagingParams);
            ViewBag.PostQuery = postQuery;
            ViewBag.Title = $"Các bài viết có chủ đề '{postQuery.CategoryName}'";
            return View("Index", postsList);
        }
        public async Task<IActionResult> Author(
            string slug,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 5)
        {
            var author = await _authorRepository.GetAuthorBySlugAsync(slug);
            var postQuery = new PostQuery()
            {
                PublishedOnly = true,
                AuthorSlug = slug,
                AuthorName = author?.FullName,
            };
            IPagingParams pagingParams = CreatePagingParamsForPost(pageNumber, pageSize);
            var postsList = await _blogRepository.GetPagesPostQueryAsync(postQuery, pagingParams);
            ViewBag.PostQuery = postQuery;
            ViewBag.Title = $"Các bài viết có tác giả '{postQuery.AuthorName}'";
            return View("Index", postsList);
        }
        public async Task<IActionResult> Tag(
            string slug,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 5)
        {
            var tag = await _blogRepository.GetTagBySlugAsync(slug);
            var postQuery = new PostQuery()
            {
                PublishedOnly = true,
                TagSlug = slug,
                TagName = tag.Name,
            };
            IPagingParams pagingParams = CreatePagingParamsForPost(pageNumber, pageSize);
            var postsList = await _blogRepository.GetPagesPostQueryAsync(postQuery, pagingParams);
            ViewBag.PostQuery = postQuery;
            ViewBag.Title = $"Các bài viết có tag '{postQuery.TagName}'";
            return View("Index", postsList);
        }
        public IActionResult About() => View();
        public IActionResult Contact() => View();
        public IActionResult Rss() => Content("Nội dung sẽ được cập nhật");

        private IPagingParams CreatePagingParamsForPost(
            int pageNumber = 1,
            int pageSize = 5,
            string sortColumn = "PostedDate",
            string sortOrder = "DESC")
        {
            return new PagingParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortOrder = sortOrder,
            };
        }
    }
}
