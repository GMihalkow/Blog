namespace Blog.Dal.Models.Common.Contracts
{
    public abstract class BaseDropdownListModel<T>
    {
        public T Id { get; set; }

        public string Name { get; set; }
    }
}