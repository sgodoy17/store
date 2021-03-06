namespace StoreTest.Modules.Client.Entities
{
    public class ClientEntity
    {
        protected string name;

        protected string document;

        protected string address;

        protected string phone;

        public ClientEntity() { }

        public ClientEntity(string name, string document)
        {
            this.name = name;
            this.document = document;
        }

        public ClientEntity(string name, string document, string address, string phone)
        {
            this.name = name;
            this.document = document;
            this.address = address;
            this.phone = phone;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Document
        {
            get { return document; }
            set { document = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string ConvertToString()
        {
            return $"Nombre: {name}, Documento: {document}, Dirección: {address}, Telefono: {phone}";
        }
    }
}
