using System.Collections.Generic;

namespace Domain.Airtime.DataAggregate
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        /******************************************************************************
         * Biller
         ******************************************************************************/
        public virtual IList<Biller> Billers { get; set; } = new List<Biller>();
    }
}
