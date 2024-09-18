using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagers
{
    public class BrandModel : DataModel
    {
        public string BrandName { get; set; }

        /// <summary>
        /// соединение с VehicleModel (ID с BrandID)
        /// </summary>

        public override string ToString() => BrandName;
    }
}
