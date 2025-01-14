using DK89.DomainBase.Interface;
using System;
using System.ComponentModel.DataAnnotations;

namespace DK89.DomainBase.Base
{
    /// <summary>
    /// Default ID has GUID type.
    /// </summary>
    public abstract class AuditableBase : IAuditable
    {
        [Key]
        public virtual Guid Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
    public abstract class AuditableBase<TIdType> : IAuditable
    {
        [Key]
        public virtual TIdType Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}

