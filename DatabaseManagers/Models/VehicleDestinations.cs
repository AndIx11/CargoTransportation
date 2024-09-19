namespace DatabaseManagers
{
    public class VehicleDestinations : DataModel
    {
        public int VehicleID { get; set; }
        public int DestinationID { get; set; }

        /// <summary>
        /// соединение с VehicleModel (VehicleID с ID)
        /// соединение с DestinationModel (DestinationID c ID)
        /// </summary>
    }
}
