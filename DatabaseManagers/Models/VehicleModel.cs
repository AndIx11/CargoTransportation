using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagers
{
    public class VehicleModel : DataModel
    {
        public string LicencePlate { get; set; }
        public int BrandID { get; set; }
        public int ModelID { get; set; }
        public string PayloadCapacity { get; set; }
        public string YearOfManufacture { get; set; }
        public string YearOfOverhaul { get; set; }
        public int Mileage { get; set; }
        public byte[] Photo { get; set; }

        public virtual BrandModel Brand { get; set; }
        public virtual CarModelModel CarModel { get; set; }

        public string Name => $"{Brand.BrandName} {CarModel.ModelName} {YearOfManufacture}";

        /// <summary>
        /// соединение с TripModel (VehicleID с ID)
        /// соединение с VehicleDestination (ID c DestinationID)
        /// соединение с BrandModel (BrandID с ID)
        /// соединение с CarModelModel (ModelID с ID)
        /// </summary>
    }
}
