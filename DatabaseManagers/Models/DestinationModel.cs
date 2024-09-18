using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagers
{
    public class DestinationModel : DataModel
    {
        public string CargoTypeName { get; set; }

        /// <summary>
        /// соединение с VehicleDestinations (ID c DestinationID)
        /// </summary>
    }
}
