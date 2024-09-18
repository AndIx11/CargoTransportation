using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagers
{
    public class UnitModel : DataModel
    {
        public string UnitName { get; set; }

        /// <summary>
        /// соединение с CargoOrders (ID с UnitID)
        /// </summary>
    }
}
