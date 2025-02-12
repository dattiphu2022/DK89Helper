using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK89.EntityFramework
{
    public abstract class ModelBase
        : ISoftDelete
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

    }
    public interface ISoftDelete
        : IGuidId
    {
        bool IsDeleted { get; }
    }

    public interface IGuidId
    {
        Guid Id { get; }
    }
}
