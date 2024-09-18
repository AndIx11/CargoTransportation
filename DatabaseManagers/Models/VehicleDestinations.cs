using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagers
{
    public class VehicleDestinations : DataModel
    {
        public int VehicleID { get; set; }
        public int DestinationID { get; set; }

        /// <summary>
        /// соединение с VehicleModel (VehicleID с ID)
        /// соединение с DestinationModel (DestinationID c ID)
        /// </summary>
    }
}
