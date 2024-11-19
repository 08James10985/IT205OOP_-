namespace ResortManagementSystem_2.Model
{
    public class ConferenceRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public bool CateringServices { get; set; }
        public bool HasTechnicalSupport { get; set; }
    }
}
