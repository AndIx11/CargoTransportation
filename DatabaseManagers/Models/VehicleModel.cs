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
        public string Name
        {
            get
            {
                if (Brand == null || CarModel == null || YearOfManufacture == null)
                {
                    return "Информация неполная!";
                }
                return $"{Brand.BrandName} {CarModel.ModelName} {YearOfManufacture}";
            }
        }


        //public string Name => $"{Brand.BrandName} {CarModel.ModelName} {YearOfManufacture}";

        /// <summary>
        /// соединение с TripModel (VehicleID с ID)
        /// соединение с VehicleDestination (ID c DestinationID)
        /// соединение с BrandModel (BrandID с ID)
        /// соединение с CarModelModel (ModelID с ID)
        /// </summary>
    }
}
