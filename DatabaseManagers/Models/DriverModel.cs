using System.Collections.Generic;
using System.Linq;

namespace DatabaseManagers
{
    public class DriverModel : DataModel
    {
        public string BadgeNumber { get; set; }
        public int ClassID { get; set; }
        public string FullName { get; set; }
        public string BirthYear { get; set; }
        public int WorkExperience { get; set; }

        public virtual ClassificationModel Classification { get; set; }

		public string GetCategoriesNames()
		{
			var categoriesList = DatabaseManager.GetInstance().DriverCategoriesList();
			var driversCategories = categoriesList.Where(c => c.DriverID == Id).ToList();

			var categoriesNames = driversCategories.Select(dc => dc.Category.Name);

			string crewName = string.Join(", ", categoriesNames);
			return crewName;
		}

		public string CategoriesNames => GetCategoriesNames();

	}
}
