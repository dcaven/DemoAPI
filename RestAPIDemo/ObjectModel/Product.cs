namespace RestAPIDemo
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public double Price { get; set; } = 0.0;
        public bool Active{ get; set; } = true;
    }
}
