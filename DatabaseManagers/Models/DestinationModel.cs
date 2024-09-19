namespace DatabaseManagers
{
    public class DestinationModel : DataModel
    {
        public string CargoTypeName { get; set; }

        /// <summary>
        /// соединение с VehicleDestinations (ID c DestinationID)
        /// </summary>
    }
}
