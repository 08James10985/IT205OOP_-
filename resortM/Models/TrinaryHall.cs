namespace ResortManagementSystem_2.Model
{
    public class TrinaryHall
    {
        public int Id { get; set; }
        public string HallName { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public decimal RentalPricePerHour { get; set; }
    }
}
