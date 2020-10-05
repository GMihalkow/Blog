namespace Blog.Web.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
            => string.IsNullOrWhiteSpace(str) || str.Length == 0;
    }
}