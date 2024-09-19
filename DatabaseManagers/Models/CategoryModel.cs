namespace DatabaseManagers
{
    public class CategoryModel : DataModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// соединение с DriverCategories (ID с CategoryID)
        /// </summary>
    }
}
