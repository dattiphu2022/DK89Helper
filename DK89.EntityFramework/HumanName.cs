using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK89.EntityFramework
{
    public class HumanName
    {
        /// <summary>
        /// Họ (Family Name hoặc Last Name): Đây là phần tên cuối cùng trong tiếng Việt nhưng được coi là "họ" trong tiếng Anh. Ví dụ: Nguyễn, Trần, Lê, Phạm.
        /// </summary>
        public string? FamilyOrLastName { get; set; }



        /// <summary>
        /// Tên Đệm (Middle Name): Đây là phần tên nằm giữa họ và tên chính trong tiếng Việt. Ví dụ: Văn, Thị, Hoàng, Minh.
        /// </summary>
        public string? MiddleName { get; set; }



        /// <summary>
        /// Tên (Given Name hoặc First Name): Đây là tên chính, thường đứng cuối cùng trong tiếng Việt nhưng đứng đầu trong tiếng Anh. Ví dụ: Nam, Hoa, An, Linh.
        /// </summary>
        public string GivenOrFirstName { get; set; } = default!;

        /// <summary>
        /// Tách tên đầy đủ thành một đối tượng Tên người.
        /// Ví dụ: Nguyễn Văn A
        /// Tên: A
        /// Tên Đệm: Văn
        /// Họ: Nguyễn
        /// </summary>
        /// <param name="fullName">tên đầy đủ của một người. Ví dụ: Nguyễn Văn A</param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static HumanName SplitName(string fullName, char separator = ' ')
        {
            // Kiểm tra nếu chuỗi fullName là null hoặc chỉ chứa khoảng trắng
            if (string.IsNullOrWhiteSpace(fullName))
                return new HumanName
                {
                    GivenOrFirstName = string.Empty, // Tên riêng hoặc tên đầu tiên
                    MiddleName = string.Empty,      // Tên đệm
                    FamilyOrLastName = string.Empty // Họ hoặc tên cuối cùng
                };

            // Tách chuỗi fullName thành các phần dựa vào ký tự phân cách (separator)
            string[] parts = fullName.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
            {
                // Trường hợp chuỗi chỉ chứa 1 từ
                return new HumanName
                {
                    GivenOrFirstName = parts[0], // Từ duy nhất được coi là tên riêng
                    MiddleName = string.Empty,
                    FamilyOrLastName = string.Empty
                };
            }
            else if (parts.Length == 2)
            {
                // Trường hợp chuỗi chứa 2 từ
                return new HumanName
                {
                    FamilyOrLastName = parts[0],  // Từ đầu tiên được coi là họ
                    GivenOrFirstName = parts[1], // Từ thứ hai được coi là tên riêng
                    MiddleName = string.Empty    // Không có tên đệm
                };
            }
            else
            {
                // Trường hợp chuỗi chứa nhiều hơn 2 từ
                string lastName = parts[0];           // Từ đầu tiên là họ
                string firstName = parts[^1];        // Từ cuối cùng là tên riêng
                string middleName = string.Join(" ", parts[1..^1]); // Phần giữa là tên đệm

                return new HumanName
                {
                    GivenOrFirstName = firstName,    // Tên riêng
                    FamilyOrLastName = lastName,    // Họ
                    MiddleName = middleName         // Tên đệm
                };
            }
        }

    }

}
