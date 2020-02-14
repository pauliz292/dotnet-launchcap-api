namespace Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToUpperFirstLetter(this string str)
        {
            if (str.Length == 1)
            {
                return str[0].ToString().ToUpper();
            }
            else
            {
                return str[0].ToString().ToUpper() + str.Substring(1);
            }
        }
    }
}