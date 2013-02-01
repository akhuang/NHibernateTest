using System.Text.RegularExpressions;

namespace NHibernateTest.AutoMapping.Conventions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string to database naming style.
        /// E.g. DatabaseName => DATABASE_NAME
        /// </summary>
        internal static string ToDatabaseName(this string s)
        {
            return Regex.Replace(s, @"\B[A-Z]", match => "_" + match.ToString()).ToUpper();
        }
    }
}