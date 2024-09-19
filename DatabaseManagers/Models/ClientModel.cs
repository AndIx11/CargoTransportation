﻿namespace DatabaseManagers
{
    public class ClientModel : DataModel
    {
        public string Phone {  get; set; }
		public string Name { get; set; }
		/// <summary>
		/// соединение с EntityModel (ID с ClientID)
		/// соединение с IndividualModel (ID с ClientID)
		/// соединение с OrderModel (ID с SenderClientID)
		/// соединение с OrderModel (ID с ReceiverClientID)
		/// </summary>
	}
}
