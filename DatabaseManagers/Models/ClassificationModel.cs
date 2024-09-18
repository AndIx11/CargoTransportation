using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagers
{
    public class ClassificationModel : DataModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// соединение с DriverModel (ID с ID)
        /// </summary>
    }
}
