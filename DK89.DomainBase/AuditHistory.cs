using System;

namespace DK89.DomainBase
{
    /// <summary>
    /// Represents a record of changes made to an entity for audit purposes.
    /// Inherits from <see cref="AuditableBase"/> to include common audit properties.
    /// </summary>
    public sealed class AuditHistory
        : AuditableBase
    {
        /// <summary>
        /// Name of the entity that was changed (e.g., "Product", "Order").
        /// </summary>
        public string EntityName { get; set; } = default!;

        /// <summary>
        /// Unique identifier of the entity that was changed.
        /// </summary>
        public Guid EntityId { get; set; } = default!;

        /// <summary>
        /// Name of the property that was changed.
        /// </summary>
        public string PropertyName { get; set; } = default!;

        /// <summary>
        /// The value of the property before the change.
        /// </summary>
        public string OldValue { get; set; } = default!;

        /// <summary>
        /// The value of the property after the change.
        /// </summary>
        public string NewValue { get; set; } = default!;
    }

}
