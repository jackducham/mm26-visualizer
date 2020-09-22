using System.Text;

namespace MM26.Utilities
{
    public static class StringExtensions
    {
        /// <summary>
        /// Only keep chars who gives true in <c>char.IsSymbol</c>
        ///
        /// Use this to preprocess text from input fields
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string KeepVisibles(this string s)
        {
            var builder = new StringBuilder();

            foreach (var c in s)
            {
                if (char.IsSymbol(c)
                    || char.IsLetterOrDigit(c)
                    || char.IsPunctuation(c))
                {
                    builder.Append(c);
                }
            }

            return builder.ToString();
        }
    }
}
