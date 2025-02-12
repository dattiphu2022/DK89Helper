using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK89.EntityFramework
{
    public class AuditHistory
    {
        public Guid Id { get; set; }
        public string EntityName { get; set; } // Tên của thực thể, ví dụ: "Product", "Order"
        public Guid EntityId { get; set; } // ID của thực thể bị thay đổi
        public string PropertyName { get; set; } // Tên thuộc tính thay đổi
        public string OldValue { get; set; } // Giá trị cũ
        public string NewValue { get; set; } // Giá trị mới
        public DateTime ChangedAt { get; set; } // Thời điểm thay đổi
        public string ChangedBy { get; set; } // Ai thực hiện thay đổi (nếu cần)
    }

}
