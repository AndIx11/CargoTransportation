using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagers
{
    public class CrewModel : DataModel
    {
        private string GetCrewName()
        {
            var driversList = DatabaseManager.GetInstance().DriversCrewList(this.Id);
            var driversNames = driversList.Select(d => d.FullName);

            string crewName = string.Join(", ", driversNames);
            return crewName;
        }

        public string Name { get => GetCrewName(); }
        
        /// <summary>
        /// соединение с DriverCrews (ID c CrewID)
        /// соединение с TripModel (ID c CrewID)
        /// </summary>
    }
}
