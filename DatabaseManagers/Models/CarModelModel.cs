using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagers
{
    public class CarModelModel : DataModel
    {
        public string ModelName { get; set; }

        /// <summary>
        /// соединение с VehicleModel (ID с ModelID)
        /// </summary>
        /// 
        public override string ToString() => ModelName;
    }
}
