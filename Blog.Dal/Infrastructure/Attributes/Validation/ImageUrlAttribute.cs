using Blog.Dal.Infrastructure.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Blog.Dal.Infrastructure.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ImageUrlAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var regex = new Regex(DalConstants.ImageUrlRegex, RegexOptions.IgnoreCase);

            return regex.IsMatch(value.ToString());
        }
    }
}