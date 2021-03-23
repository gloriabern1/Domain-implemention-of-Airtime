using PaymentSharedKernels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.Base
{
    public abstract class DbEntityBase : Entity<Guid>
    {
        //public object Id { get; set; }
    }
    public abstract class DbEntity<T> : DbEntityBase
    {
        //protected static T GetService<T>() => (T)DomainProvider.Provider.GetService(typeof(T));
#pragma warning disable CS0108 // 'DbEntity<T>.Id' hides inherited member 'Entity<Guid>.Id'. Use the new keyword if hiding was intended.
        public T Id { get; set; }
#pragma warning restore CS0108 // 'DbEntity<T>.Id' hides inherited member 'Entity<Guid>.Id'. Use the new keyword if hiding was intended.
        public bool Deleted { get; set; }
    }

    public class DbGuidEntity : DbEntity<Guid>
    {
        public DbGuidEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
