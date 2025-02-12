namespace DK89.Extensions
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public static class StringHelper
    {
        /// <summary>
        /// Chuẩn hóa chuỗi thành Camel Case.
        /// <br/>
        /// <br/>
        /// Đầu vào: hElLo wOrLd! 123 this IS a Test.
        /// <br/>
        /// Đầu ra: helloWorld123ThisIsATest
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <returns>Chuỗi đã được chuẩn hóa thành Camel Case.</returns>
        public static string ToCamelCase(this string input, bool removeNoneDigitOrLeterCharacters = true)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            if (removeNoneDigitOrLeterCharacters is true)
            {
                // Loại bỏ các ký tự không hợp lệ (nếu có)
                input = Regex.Replace(input, @"[^a-zA-Z0-9\s]", string.Empty);
            }

            // Tách chuỗi thành các từ
            string[] words = input.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
                return string.Empty;

            // Viết hoa chữ cái đầu tiên của mỗi từ, trừ từ đầu tiên (Camel Case)
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = i == 0
                    ? words[i].ToLower(CultureInfo.InvariantCulture)
                    : CultureInfo.InvariantCulture.TextInfo.ToTitleCase(words[i].ToLower(CultureInfo.InvariantCulture));
            }

            // Kết hợp lại thành một chuỗi
            return string.Concat(words);
        }



        /// <summary>
        /// Chuẩn hóa chuỗi thành Pascal Case, giữ nguyên một khoảng trắng giữa các từ.
        /// <br/>
        /// <br/>
        /// Đầu vào: "nguyễn văn aN!@#123 bÙi thị       "
        /// <br/>
        /// Đầu ra: Nguyễn Văn An123 Bùi Thị
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <returns>Chuỗi đã được chuẩn hóa thành Pascal Case.</returns>
        public static string ToHumanNamingCase(this string input, bool removeNoneDigitOrLeterCharacters = true)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;
            if (removeNoneDigitOrLeterCharacters is true)
            {
                // Loại bỏ các ký tự không hợp lệ (giữ lại chữ, số và khoảng trắng)
                input = Regex.Replace(input, @"[^a-zA-Z0-9\s]", string.Empty);
            }

            // Chuyển chuỗi về dạng chữ thường
            input = input.ToLower(CultureInfo.InvariantCulture);

            // Tách chuỗi thành các từ
            string[] words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Viết hoa chữ cái đầu của mỗi từ
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(words[i]);
            }

            // Kết hợp lại các từ với một khoảng trắng
            return string.Join(" ", words);
        }
    }

}
