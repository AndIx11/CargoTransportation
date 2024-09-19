namespace DatabaseManagers
{
    public class DriverCategories : DataModel
    {
        public int DriverID { get; set; }
        public int CategoryID { get; set; }

        public virtual CategoryModel Category { get; set; }
        public virtual DriverModel Driver { get; set; }

        /// <summary>
        /// соединение с CategoryModel (CategoryID с ID)
        /// соединение с DriverModel (DriverID с ID)
        /// </summary>
    }
}
