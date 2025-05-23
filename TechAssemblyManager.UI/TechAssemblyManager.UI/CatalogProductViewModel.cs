namespace TechAssemblyManager.UI
{
    public class CatalogProductViewModel
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        public bool CanAdd { get; set; }
    }
}