﻿namespace Blog.Dal.Infrastructure.Constants
{
    public static class DalConstants
    {
        public const int PageSize = 10;

        public const string SuccessMessage = "Successfully {0} {1}.";

        public const string NotFoundMessage = "{0} not found.";

        public const string ImageUrlRegex = @"^(https?:\/\/.*\.(?:png|jpg|jpeg|webp))$";

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