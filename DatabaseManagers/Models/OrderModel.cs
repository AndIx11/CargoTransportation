using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagers
{
    public class OrderModel : DataModel
    {
        public string OrderDate { get; set; }

        public int SenderClientID { get; set; }
        public int ReceiverClientID { get; set; }
        public string LoadingAddress { get; set; }
        public string UnloadingAddress { get; set; }
        public double RouteLength { get; set; }
        public decimal OrderCost { get; set; }

        public virtual ClientModel SenderClient { get; set; }
        public virtual ClientModel ReceiverClient { get; set; }

		/// <summary>
		/// соединение с TripModel (ID с OrderID)
		/// соединение с CargoOrders (ID с OrderID)
		/// соединение с ClientModel (SenderClientID с ID)
		/// соединение с ClientModel (ReceiverClientID с ID)
		/// </summary>
	}
}
