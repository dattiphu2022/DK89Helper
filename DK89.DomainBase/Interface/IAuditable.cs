using System;

namespace DK89.DomainBase.Interface
{
    public interface IAuditable
    {
        string? CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        string? EditedBy { get; set; }
        DateTime? EditedDate { get; set; }
        bool IsDeleted { get; set; }
    }

}
