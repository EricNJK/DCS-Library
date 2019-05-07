namespace DCSLibrary
{
    public class Product
    {
        private int id = 0;     //Default 'empty' id
        private string name;
        private string description;
        private ProductType type;
        private string imagePath;
        private float price;    //Marked price (Less discount)
        private float markedPrice;
        private int minimumAge;

        public Product(int id, string name, string description, ProductType type, string imagePath, float price, float markedPrice, int minimumAge)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.type = type;
            this.imagePath = imagePath;
            this.price = price;
            this.markedPrice = markedPrice;
            this.minimumAge = minimumAge;
        }

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }

        public ProductType Type
        {
            get { return type; }
        }

        public string ImagePath
        {
            get { return imagePath; }
        }

        public float Price
        {
            get { return price; }
        }

        public float MarkedPrice
        {
            get { return markedPrice; }
        }

        public int MinimumAge
        {
            get { return minimumAge; }
        }

        public int NumInStock { get; set; } = 1;    //Default 1

        public int NumInOrder { get; set; } = 1;

        public override bool Equals(object obj)
        {
            Product prod = (Product)obj;
            return this.id.Equals(prod.id);
        }

        public override int GetHashCode()
        {
            return id.GetHashCode() + type.GetHashCode() * ((int)price).GetHashCode();
        }
    }
}
