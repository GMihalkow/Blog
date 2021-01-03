using Blog.Dal.Models.Article;
using Blog.Dal.Services.Articles.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Blog.Web.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ViewsFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is NotFoundResult) return;

            var articleService = (IArticleService)context.HttpContext.RequestServices.GetService(typeof(IArticleService));
            var model = ((ArticleViewModel)(context.Result as ViewResult).Model);

            articleService.IncrementViews(model.Id);
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}