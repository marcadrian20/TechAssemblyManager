namespace TechAssemblyManager.Models
{
    public class ProductCategory
    {
        public string categoryId { get; set; }           // unique ID, can be used as key in Firebase
        public string name { get; set; }                 // e.g., "Laptops"
        public string type { get; set; }                 // e.g., "system", "component", etc.
        public string description { get; set; }          // optional category description

        public ProductCategory() { }

    }
}
