﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
