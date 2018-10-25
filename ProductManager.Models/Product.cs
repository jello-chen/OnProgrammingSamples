namespace ProductManager.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductType ProductType { get; set; }
    }

    public enum ProductType
    {
        Book,
        Toy,
        Clothing
    }
}
