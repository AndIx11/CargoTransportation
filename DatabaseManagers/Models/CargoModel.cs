namespace DatabaseManagers
{
    public class CargoModel : DataModel
    {
        public string CargoName { get; set; }

        /// <summary>
        /// соединение с CargoOrders (ID с OrderID)
        /// </summary>
    }
}
