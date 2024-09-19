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
