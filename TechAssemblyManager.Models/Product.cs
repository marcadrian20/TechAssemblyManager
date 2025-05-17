namespace TechAssemblyManager.Models
{
    public class Product
    {
        public string productId { get; set; }            // unique ID
        public string name { get; set; }                 // e.g., "Dell XPS 15"
        public string description { get; set; }          // detailed description
        public float price { get; set; }                // product price
        public string categoryId { get; set; }           // foreign key to ProductCategory
        public double rating { get; set; }               // criticScore, e.g., 4.5
        public bool isActive { get; set; }               // if shown in catalog or not
        public string imageURL { get; set; }
        public bool hasPromotion { get; set; }
        public string promotionId {  get; set; }

        public Product() { }
    }
}
