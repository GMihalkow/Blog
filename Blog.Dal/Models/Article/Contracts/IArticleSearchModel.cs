using Blog.Dal.Models.Common.Contracts;

namespace Blog.Dal.Models.Article.Contracts
{
    public interface IArticleSearchModel : IBaseSearchModel
    {
        string CategoryId { get; set; }
    }
}