namespace DatabaseManagers
{
    public class UnitModel : DataModel
    {
        public string UnitName { get; set; }

        /// <summary>
        /// соединение с CargoOrders (ID с UnitID)
        /// </summary>
    }
}
