using System;

namespace Domain.Airtime.DataAggregate
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public bool IsActive { get; set; }
        public bool IsAmountEditable { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Amount { get; set; }

        /******************************************************************************
         * Biller
         ******************************************************************************/
        public int BillerId { get; set; }
        public virtual Biller Biller { get; set; }

        
    }
}
