namespace NewsApp.Common
{
    public class DataConstants
    {
        public static class Article
        {
            public const int TitleMaxLength = 300;
            public const int TitleMinLength = 10;
            public const int ContentMinLength = 30;
        }
        public static class Category
        {
            public const int NameMaxLength = 50;
        }
        public static class Comment
        {
            public const int ContentMaxLength = 500;
            public const int ContentMinLength = 3;
        }

    }
}
