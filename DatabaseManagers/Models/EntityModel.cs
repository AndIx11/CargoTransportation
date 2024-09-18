using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagers
{
    public class EntityClientModel : DataModel
    {
        public string Name { get; set; }
        public string CEOName { get; set; }
        public string INN { get; set; }
        public string LegalAddress { get; set; }
        public int BankID { get; set; }
        public int ClientID { get; set; }

		public virtual ClientModel Client { get; set; }
        public virtual BankModel Bank { get; set; }

		/// <summary>
		/// соединение с BanksModel (ID c ID)
		/// соединение с ClientModel (ClientID c ID)
		/// </summary>
	}
}
