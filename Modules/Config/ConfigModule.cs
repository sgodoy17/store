using StoreTest.Modules.Client.Entities;
using StoreTest.Modules.Client.Services;
using StoreTest.Modules.Config.Entities;
using StoreTest.Modules.Product.Entities;
using StoreTest.Modules.Product.Services;
using StoreTest.Utils;
using System;

namespace StoreTest.Modules.Config
{
    public class ConfigModule
    {
        protected ClientService clientService;

        protected ProductService productService;

        protected ConfigEntity config;

        public ConfigModule(ClientService clientService, ProductService productService)
        {
            this.clientService = clientService;
            this.productService = productService;
            config = new ConfigEntity();
            config.Deploy = false;
        }

        public void Run()
        {
            if (! config.Deploy)
            {
                SetCompanyName();
                ListSeeders();
                config.Deploy = true;
            }
        }

        public string GetCompanyName()
        {
            return config.Company;
        }

        public void SetCompanyName()
        {
            Console.Clear();
            Console.Write("Ingrese el Nombre de la tienda: ");
            config.Company = Console.ReadLine();
        }

        public void ListSeeders()
        {
            Console.Clear();
            bool exit = false;

            while (! exit)
            {
                Console.Clear();
                Console.WriteLine("Desea ejecutar los seeders de las listas? (si/no)");

                switch (Console.ReadLine())
                {
                    case "si":
                        ClientSeeder();
                        ProductSeeder();
                        MessageUtil.Message("Seeders ejecutados satisfactoriamente");
                        exit = true;
                        break;
                    case "no":
                        exit = true;
                        break;
                    default:
                        exit = false;
                        break;
                }
            }
        }

        public void ClientSeeder()
        {
            clientService.Create(new ClientEntity("Jhon Doe", "19808512", "CR 34 D", "3015236332"));
            clientService.Create(new ClientEntity("Julita Venegas", "18512211", "CR 55 H", "3021525252"));
            clientService.Create(new ClientEntity("Jhonnar Rodrigez", "18512512", "CR 52 D", "2589656565"));
            clientService.Create(new ClientEntity("Julian Urrego", "14458565", "CR 71 C", "8965655252"));
            clientService.Create(new ClientEntity("Estephania Chavez", "15232525", "CR 80 M", "3015247854"));
            clientService.Create(new ClientEntity("Donal Trump", "85458545", "CR 41 A", "3052015212"));
            clientService.Create(new ClientEntity("Berny Alex", "45878454", "CR 12 V", "3002563256"));
            clientService.Create(new ClientEntity("Alexander Contreraz", "25632563", "CR 1 D", "3303255656"));
            clientService.Create(new ClientEntity("Saul Gomez", "41254521", "CR 21 T", "3015263212"));
            clientService.Create(new ClientEntity("Alexandra Ortega", "45878545", "CR 55 Q", "3085266325"));
            MessageUtil.Simple("ClientSeeder");
        }

        public void ProductSeeder()
        {
            productService.Create(new ProductEntity("00001", "Harina", 3000, 100));
            productService.Create(new ProductEntity("00002", "Azucar", 1000, 100));
            productService.Create(new ProductEntity("00003", "Mantequilla grande", 19000, 100));
            productService.Create(new ProductEntity("00004", "Leche", 9000, 100));
            productService.Create(new ProductEntity("00005", "Pan", 2000, 100));
            productService.Create(new ProductEntity("00006", "Atun", 4500, 100));
            productService.Create(new ProductEntity("00007", "Tody", 5000, 100));
            productService.Create(new ProductEntity("00008", "Sal", 1000, 100));
            productService.Create(new ProductEntity("00009", "Arroz", 6000, 100));
            productService.Create(new ProductEntity("00010", "Cereal", 11000, 100));
            MessageUtil.Simple("ProductSeeder");
        }
    }
}
