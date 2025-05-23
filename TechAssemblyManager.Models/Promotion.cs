namespace TechAssemblyManager.Models
{
    public class Promotion
    {
        public string promotionId { get; set; }
        public string createdBy { get; set; }
        public string description { get; set; }
        public float discountPercentage { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public bool isActive { get; set; }
        public string name { get; set; }
        public Dictionary<string, bool> includedProductIds { get; set; } = new Dictionary<string, bool>();
    }
}
