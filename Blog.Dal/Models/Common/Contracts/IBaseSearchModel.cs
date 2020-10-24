namespace Blog.Dal.Models.Common.Contracts
{
    public interface IBaseSearchModel
    {
        string Keywords { get; }

        int? Size { get; }

        int? Page { get; }
    }
}