using System.Linq;

namespace Blog.Web.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string TrimText(this string str, int length)
        {
            var result = string.Join(string.Empty, str.Take(length));

            return str.Length > length ? result + "..." : result;
        }
    }
}