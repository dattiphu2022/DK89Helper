using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK89.EntityFramework
{
    public abstract class AuditDbContext : DbContext
    {
        public AuditDbContext(DbContextOptions options) : base(options) { }

        // DbSet cho AuditHistory, bắt buộc phải triển khai trong lớp con
        public abstract DbSet<AuditHistory> AuditHistories { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(cancellationToken);
            await OnAfterSaveChangesAsync(auditEntries);
            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditableClassBase auditableEntity)
                {
                    var now = DateTime.UtcNow;
                    var userName = GetCurrentUser(); // Lấy tên người dùng từ context, nếu có

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditableEntity.CreatedAt = now;
                            auditableEntity.CreatedBy = userName;
                            break;

                        case EntityState.Modified:
                            auditableEntity.UpdatedAt = now;
                            auditableEntity.UpdatedBy = userName;
                            break;

                        case EntityState.Deleted:
                            auditableEntity.IsDeleted = true;
                            entry.State = EntityState.Modified;
                            break;
                    }

                    // Tạo danh sách các thay đổi thuộc tính
                    foreach (var property in entry.Properties)
                    {
                        if (property.IsModified)
                        {
                            auditEntries.Add(new AuditEntry
                            {
                                EntityName = entry.Entity.GetType().Name,
                                EntityId = entry.Property("Id")?.CurrentValue?.ToString(),
                                PropertyName = property.Metadata.Name,
                                OldValue = property.OriginalValue?.ToString(),
                                NewValue = property.CurrentValue?.ToString(),
                                ChangedAt = now,
                                ChangedBy = userName
                            });
                        }
                    }
                }
            }

            return auditEntries;
        }

        private async Task OnAfterSaveChangesAsync(List<AuditEntry> auditEntries)
        {
            if (auditEntries.Any())
            {
                foreach (var auditEntry in auditEntries)
                {
                    var auditHistory = new AuditHistory
                    {
                        EntityName = auditEntry.EntityName,
                        EntityId = auditEntry.EntityId,
                        PropertyName = auditEntry.PropertyName,
                        OldValue = auditEntry.OldValue,
                        NewValue = auditEntry.NewValue,
                        ChangedAt = auditEntry.ChangedAt,
                        ChangedBy = auditEntry.ChangedBy
                    };

                    await Set<AuditHistory>().AddAsync(auditHistory);
                }

                await base.SaveChangesAsync();
            }
        }

        // Phương thức để lấy thông tin người dùng, có thể ghi đè khi kế thừa
        protected virtual string GetCurrentUser()
        {
            return "System"; // Mặc định trả về "System", có thể thay đổi khi sử dụng
        }
    }

    public class AuditEntry
    {
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ChangedAt { get; set; }
        public string ChangedBy { get; set; }
    }

}
