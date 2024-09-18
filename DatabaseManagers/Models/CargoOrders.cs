using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagers
{
    public class CargoOrders : DataModel
    {
        public int CargoID { get; set; }
        public int OrderID { get; set; }
        public int UnitID { get; set; }
        public int Quantity { get; set; }
        public double TotalWeight { get; set; }
        public double InsuranceValue { get; set; }

        public virtual CargoModel Cargo { get; set; }
        public virtual UnitModel Unit { get; set; } 
        public virtual OrderModel Order { get; set; }

    }
}
