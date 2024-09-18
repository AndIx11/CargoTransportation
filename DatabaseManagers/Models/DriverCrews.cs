using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagers
{
    public class DriverCrews : DataModel
    {
        public int DriverID { get; set; }
        public int CrewID { get; set; }

        public virtual DriverModel Driver { get; set; }
        public virtual CrewModel Crew { get; set; } 

        /// <summary>
        /// соединение с CrewModel (CrewID с ID)
        /// соединение с DriverModel (DriverID с ID)
        /// </summary>
    }
}
