namespace Blog.Dal.Models.Common.Contracts
{
    public interface IBaseDropdownListModel<T>
    {
        T Id { get; }

        string Name { get; }
    }
}