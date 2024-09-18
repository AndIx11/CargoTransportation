using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
