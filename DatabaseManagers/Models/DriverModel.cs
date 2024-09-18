using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagers
{
    public class DriverModel : DataModel
    {
        public string BadgeNumber { get; set; }
        public int ClassID { get; set; }
        public string FullName { get; set; }
        public string BirthYear { get; set; }
        public int WorkExperience { get; set; }

        public virtual ClassificationModel Classification { get; set; }

        /// <summary>
        /// соединение с ClassificationsModel (ID с ID)
        /// соединение с DriverCategories (ID с DriverID)
        /// соединение с DriverCrews (ID с DriverID)
        /// </summary>
    }
}
