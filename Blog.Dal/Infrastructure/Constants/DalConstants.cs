namespace Blog.Dal.Infrastructure.Constants
{
    public static class DalConstants
    {
        public const int PageSize = 10;

        public const string SuccessMessage = "Successfully {0} {1}.";

        public static string[] AllowedHtmlTags = new string[]
        {
            "h1",
            "h2",
            "h3",
            "h4",
            "h5",
            "h6",
            "p",
            "b",
            "i",
            "strong",
            "img"
        };
    }
}