### Example of Usage for `AuditableBase` and `AuditableBase<TIdType>`

The following examples demonstrate how to use the `AuditableBase` and `AuditableBase<TIdType>` classes in your project.

#### `AuditableBase` Example

```csharp
using DK89.DomainBase;
using System;

namespace MyProject.Domain
{
    /// <summary>
    /// Example entity inheriting from AuditableBase with a default GUID as ID.
    /// </summary>
    public class Product : AuditableBase
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
    }
}

// Usage
var product = new Product
{
    Id = Guid.NewGuid(),
    Name = "Example Product",
    Price = 19.99M,
    CreatedBy = "Admin",
    CreatedDate = DateTime.UtcNow
};

Console.WriteLine($"Product: {product.Name}, Created By: {product.CreatedBy}, Created At: {product.CreatedDate}");
```

#### `AuditableBase<TIdType>` Example

```csharp
using DK89.DomainBase;
using System;

namespace MyProject.Domain
{
    /// <summary>
    /// Example entity inheriting from AuditableBase with a custom ID type.
    /// </summary>
    public class Order : AuditableBase<int>
    {
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; } = default!;
    }
}

// Usage
var order = new Order
{
    Id = 1,
    OrderDate = DateTime.UtcNow,
    CustomerName = "John Doe",
    CreatedBy = "System",
    CreatedDate = DateTime.UtcNow
};

Console.WriteLine($"Order ID: {order.Id}, Customer: {order.CustomerName}, Order Date: {order.OrderDate}");
```

### Key Features of `AuditableBase` and `AuditableBase<TIdType>`
- **Auditable Fields**:
  - `CreatedBy`: Indicates the user who created the entity.
  - `CreatedDate`: Timestamp of when the entity was created.
  - `EditedBy`: Indicates the user who last edited the entity.
  - `EditedDate`: Timestamp of the last edit.
  - `IsDeleted`: Boolean flag to indicate soft deletion.
  
- **Customizable ID Type**:
  - The default `AuditableBase` class uses a GUID for the `Id`.
  - Use `AuditableBase<TIdType>` to define an ID with a custom type, such as `int`, `string`, or another type.

### When to Use These Classes
- **Entities in a Domain Model**: Use these classes as base types for entities that require auditing fields and flexible ID management.
- **Soft Deletions**: Handle soft deletions gracefully using the `IsDeleted` property.
  
### Additional Notes
- The properties in the base classes can be overridden in derived classes as needed.
- Ensure you initialize the `Id` property in your derived classes, particularly when using custom ID types.

======================================================================

# AuditHistory Class

## Overview

The `AuditHistory` class is a sealed class that inherits from `AuditableBase`. It provides a detailed representation of changes made to entities within an application. This design ensures that audit entries include not only the change-specific details but also any common audit fields provided by the `AuditableBase` class.

## Features

### Properties from `AuditHistory`

- **EntityName**: The type of entity being modified (e.g., "Product", "Order").
- **EntityId**: A unique identifier for the specific entity instance.
- **PropertyName**: The name of the property that was changed.
- **OldValue**: The property's value before the change.
- **NewValue**: The property's value after the change.

### Inherited from `AuditableBase`

Ensure that your `AuditableBase` class contains fields such as:
- **CreatedAt**: The timestamp when the audit record was created.
- **CreatedBy**: The user or system that created the record.
- **ModifiedAt**: The timestamp of the most recent modification (if applicable).
- **ModifiedBy**: The user or system that last modified the record.

## Benefits

- **Extensibility**: By inheriting from `AuditableBase`, common audit fields can be reused across other classes.
- **Strong Type Safety**: Sealed class ensures that `AuditHistory` is not further extended, preserving its integrity.
- **Simplified Initialization**: Default values (`default!`) ensure that properties are properly initialized, reducing runtime errors.

## Example Usage

### Initialization Example

```csharp
var auditEntry = new AuditHistory
{
    EntityName = "Product",
    EntityId = Guid.Parse("123e4567-e89b-12d3-a456-426614174000"),
    PropertyName = "Price",
    OldValue = "100",
    NewValue = "120",
    CreatedAt = DateTime.UtcNow,
    CreatedBy = "AdminUser"
};

// Example: Save `auditEntry` to a database or log for tracking
Console.WriteLine($"Audit Entry for {auditEntry.EntityName} ({auditEntry.EntityId})");
Console.WriteLine($"{auditEntry.PropertyName} changed from {auditEntry.OldValue} to {auditEntry.NewValue}");
```

### Notes

- Ensure that your `AuditableBase` implementation aligns with the audit requirements of your project.
- Extend logging mechanisms or database mappings as needed for seamless integration.

## Future Enhancements

- **Improved Property Tracking**: Include metadata for composite properties or collection changes.
- **Integration with ORM**: Support for automatic tracking with Entity Framework or similar tools.
- **Custom Serialization**: Add custom JSON converters for auditing frameworks.

## Contribution

Contributions to improve this class and its documentation are welcome! Feel free to submit a pull request.
```