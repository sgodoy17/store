namespace StoreTest.Modules.Product.Entities
{
    public class ProductEntity
    {
        protected string code;

        protected string name;

        protected double price;

        protected int amount;

        public ProductEntity() { }

        public ProductEntity(string code, string name, double price, int amount)
        {
            this.code = code;
            this.name = name;
            this.price = price;
            this.amount = amount;
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public string ConvertToString()
        {
            return $"Codigo: {code}, Nombre: {name}, Precio: {price}, Cantidad: {amount}";
        }
    }
}
