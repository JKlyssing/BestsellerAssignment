namespace BestsellerNET.Models
{

    public class Rootobject
    {
        public Product[] Products { get; set; }
        public Category Categories { get; set; }
    }
    public class Product
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public float Price { get; set; }
        public object Stock { get; set; }
        public string Color { get; set; }
        public object[] Size { get; set; }
        public Name Name { get; set; }
        public string[] Images { get; set; }
        public string[] Categories { get; set; }
        public Variant[] Variant { get; set; }
    }
    public class Name
    {
        public string En { get; set; }
        public string Dk { get; set; }
    }
    public class Variant
    {
        public object Stock { get; set; }
        public string Color { get; set; }
        public string[] Size { get; set; }
        public string[] Images { get; set; }
    }

    public class Category
    {
        public string Id { get; set; }
        public string Parent_category_id { get; set; }
        public int Level { get; set; }
        public Name Name { get; set; }
        public Category[] Categories { get; set; }
    }


}
