using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DK89.EntityFramework
{

    public class AppDbContext : DbContext
    {
        public DbSet<AuditHistory> AuditHistories { get; set; }

        // Define your entities

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dataFolderName = "DATA";
            var dataFileName = "AppDatabase.db";
            var connectionString = $"Data Source=./{dataFolderName}/{dataFileName};";

            if (dataFolderName is not null
                && Directory.Exists(dataFolderName) is not true)
            {
                Directory.CreateDirectory(dataFolderName);
            }

            optionsBuilder.UseSqlite(connectionString);
        }

        public override int SaveChanges()
        {
            var now = DateTime.UtcNow;
            var auditHistories = new List<AuditHistory>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Modified)
                {
                    string entityName = entry.Entity.GetType().Name;
                    Guid entityId = (Guid)entry.Property("Id").CurrentValue;

                    foreach (var property in entry.Properties)
                    {
                        if (property.IsModified)
                        {
                            auditHistories.Add(new AuditHistory
                            {
                                EntityName = entityName,
                                EntityId = entityId,
                                PropertyName = property.Metadata.Name,
                                OldValue = property.OriginalValue?.ToString(),
                                NewValue = property.CurrentValue?.ToString(),
                                ChangedAt = now,
                                ChangedBy = "System" // Replace with actual user if needed
                            });
                        }
                    }
                }
            }

            // Save audit histories
            AuditHistories.AddRange(auditHistories);

            return base.SaveChanges();
        }

        public static T ConvertToType<T>(string value)
        {
            if (value == null) return default;

            Type targetType = typeof(T);
            try
            {
                if (targetType.IsEnum)
                {
                    return (T)Enum.Parse(targetType, value);
                }
                return (T)Convert.ChangeType(value, targetType);
            }
            catch
            {
                throw new InvalidCastException($"Cannot convert '{value}' to type {targetType.Name}");
            }
        }

        public static void RestorePropertyFromHistory(object entity, AuditHistory history)
        {
            var property = entity.GetType().GetProperty(history.PropertyName);
            if (property == null) throw new Exception($"Property '{history.PropertyName}' not found on entity '{entity.GetType().Name}'.");

            // Lấy kiểu dữ liệu của thuộc tính
            var propertyType = property.PropertyType;

            // Chuyển đổi giá trị cũ từ chuỗi về kiểu dữ liệu thực tế
            var oldValue = string.IsNullOrEmpty(history.OldValue)
                ? null
                : Convert.ChangeType(history.OldValue, propertyType);

            property.SetValue(entity, oldValue);
        }

        public static void RestoreEntityFromHistory(object entity, List<AuditHistory> histories)
        {
            foreach (var history in histories)
            {
                RestorePropertyFromHistory(entity, history);
            }
        }
    }

}
