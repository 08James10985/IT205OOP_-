namespace Resort_Management.Model
{
    public class CottageReef
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public decimal EquipmentRentalPrice { get; set; }
        public string Schedule { get; set; }
    }
}
