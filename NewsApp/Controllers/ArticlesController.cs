﻿using Microsoft.AspNetCore.Mvc;
using NewsApp.Models.Articles;
using NewsApp.Services.Articles;
using NewsApp.Services.Categories;
using NewsApp.Models.Categories;
using NewsApp.Data.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using NewsApp.Common;
using NewsApp.Filters;

namespace NewsApp.Controllers
{
    public class ArticlesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IArticlesService articlesService;

        public ArticlesController(ICategoriesService categoriesService, IArticlesService articlesService)
        {
            this.categoriesService = categoriesService;
            this.articlesService = articlesService;
        }
        [AllowAnonymous]
        public IActionResult All(int page = 1)
        {
            var totalArticlesCount = articlesService.GetArticlesCount();
            ViewData["ArticlesCount"] = totalArticlesCount;
            var articles = articlesService.GetPerPage<ArticlesPagingViewModel>(6, page);
            return View(articles);
        }
        [Authorize(Roles = $"{WebConstants.Role.AuthorRoleName},{WebConstants.Role.AdminRoleName}")]
        public IActionResult Add()
        {
            var articlesModel = new AddArticlesInputModel()
            {
                Categories = categoriesService.GetAll<CategoriesViewModel>()
            };
            return View(articlesModel);
        }
        [HttpPost]
        [Authorize(Roles = $"{WebConstants.Role.AuthorRoleName},{WebConstants.Role.AdminRoleName}")]
        public async Task<IActionResult> Add(AddArticlesInputModel article)
        {

            article.Categories = categoriesService.GetAll<CategoriesViewModel>();



            if (!ModelState.IsValid)
            {

                return View(article);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool addResult = await articlesService.AddAsync(article, ModelState, userId);
            if (!addResult)
            {

                return View(article);
            }
            TempData["AddedSuccessfully"] = "This article was created successfully!";
            return RedirectToAction(nameof(All), new { page = 1 });

        }
        public async Task<IActionResult> Delete(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool deleteResult = await articlesService.DeleteArticleByIdAsync(id, userId);
            if (!deleteResult)
            {
                return BadRequest();
            }
            TempData["DeletedSuccessfully"] = "This article was deleted successfully!";
            return RedirectToAction(nameof(All), new { page = 1 });


        }

        [AllowAnonymous]
        public IActionResult ByCategory(string name)
        {

            var articles = articlesService.GetArticlesByCategory<ListArticlesByCategoryViewModel>(name);
            ViewData["name"] = name;
            return View(articles);
        }
        public IActionResult Yours()
        {

            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            var article = await articlesService.GetByIdAsync<DisplayArticleViewModel>(id);
            if (article == null)
            {
                return NotFound();
            }

            ViewData["ArticleId"] = id;
            ViewData["UserId"] = User.Identity.IsAuthenticated ? User.FindFirstValue(ClaimTypes.NameIdentifier) : null;

            int views = await articlesService.IncrementViewsAsync(id);
            ViewData["Views"] = views;
            return View(article);
        }
        public async Task<IActionResult> Update(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var article = await articlesService.GetArticleIfYours<UpdateArticleInputModel>(id, userId);
            if (article == null)
            {
                return BadRequest();
            }
            article.Categories = categoriesService.GetAll<CategoriesViewModel>();
            return View(article);


        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateArticleInputModel article, string id)
        {
            article.Categories = categoriesService.GetAll<CategoriesViewModel>();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return View(article);
            }
            var doesArticleExists = await articlesService.ExistsAndIfYoursById(id, userId);

            if (!doesArticleExists)
            {
                return BadRequest();
            }
            await articlesService.UpdateAsync(article, id);
            TempData["UpdatedSuccessfully"] = "This article was updated successfully!";
            return RedirectToAction(nameof(Details), new { id = id });

        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
