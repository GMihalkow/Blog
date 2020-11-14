using Blog.Dal.Models.Common.Contracts;

namespace Blog.Dal.Models.Category
{
    public class CategoryDropdownListModel : IBaseDropdownListModel<string>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}