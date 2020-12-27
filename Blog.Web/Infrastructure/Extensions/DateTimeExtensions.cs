using Blog.Web.Infrastructure.Constants;
using System;

namespace Blog.Web.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToWebDateFormat(this DateTime dateTime) => dateTime.ToString(WebConstants.WebDateFormat);
    }
}