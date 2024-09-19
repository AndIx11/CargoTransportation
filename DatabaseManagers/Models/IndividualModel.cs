namespace DatabaseManagers
{
    public class IndividualModel : DataModel
    {
        public string FullName { get; set; }
        public string PassportNumber { get; set; }
        public string IssueDate { get; set; }
        public string IssuedBy { get; set; }
        public int ClientID { get; set; }

        public virtual ClientModel Client { get; set; }

        /// <summary>
        /// соединение с ClientModel (ClientID c ID)
        /// </summary>
    }
}
