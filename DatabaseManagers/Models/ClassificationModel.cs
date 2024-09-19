namespace DatabaseManagers
{
    public class ClassificationModel : DataModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// соединение с DriverModel (ID с ID)
        /// </summary>
    }
}
