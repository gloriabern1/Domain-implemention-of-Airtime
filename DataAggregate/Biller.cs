using System;
using System.Collections.Generic;

namespace Domain.Airtime.DataAggregate
{
    public class Biller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string IdentifierName { get; set; }
        public bool RequiredValidation { get; set; }
        public bool IsData { get; set; }
        public bool IsAquired { get; set; }

        /******************************************************************************
         * Category
         ******************************************************************************/
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        /******************************************************************************
         * Package
         ******************************************************************************/
        public virtual IList<Package> Packages { get; set; } = new List<Package>();
        public virtual IList<DataPackages> DataPackages { get; set; } = new List<DataPackages>();

    }
}
