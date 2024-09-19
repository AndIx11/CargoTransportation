namespace DatabaseManagers
{
    public class TripModel : DataModel
    {
        public int VehicleID { get; set; }
        public int CrewID { get; set; }
        public int OrderID { get; set; }
        public string Data { get; set; }

        public virtual VehicleModel Vehicle { get; set; }
        public virtual CrewModel Crew { get; set; }
        public virtual OrderModel Order { get; set; }
    }
}
