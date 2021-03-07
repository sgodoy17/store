using StoreTest.Modules.Client;
using StoreTest.Modules.Client.Services;
using StoreTest.Modules.Config;
using StoreTest.Modules.Invoice.Services;
using StoreTest.Modules.Product;
using StoreTest.Modules.Product.Services;
using StoreTest.Modules.Report;
using StoreTest.Modules.Sale;
using StoreTest.Utils;
using System;

namespace StoreTest.Modules.Home
{
    public class HomeModule
    {
        protected ConfigModule configModule;

        protected ClientModule clientModule;

        protected ProductModule productModule;

        protected SaleModule saleModule;

        protected ReportModule reportModule;

        public HomeModule()
        {
            ClientService clientService = new ClientService();
            ProductService productService = new ProductService();
            InvoiceService invoiceService = new InvoiceService();

            configModule = new ConfigModule(clientService, productService);
            clientModule = new ClientModule(this, clientService);
            productModule = new ProductModule(this, productService);
            saleModule = new SaleModule(this, clientService, productService, invoiceService);
            reportModule = new ReportModule(this, clientService, productService, invoiceService);
        }

        public void Run(bool showMenu = true)
        {
            configModule.Run();

            while (showMenu)
            {
                showMenu = Menu();
            }

            // Set goodybye message if needed! MessageUtil.SimpleMessage("Adios") or something like that!
            Environment.Exit(0);
        }

        private bool Menu()
        {
            Console.Clear();

            Console.WriteLine($"*******{configModule.GetCompanyName()}******");
            Console.WriteLine("1) Modulo clientes");
            Console.WriteLine("2) Modulo productos");
            Console.WriteLine("3) Modulo ventas");
            Console.WriteLine("4) Modulo facturas");
            Console.WriteLine("5) Modulo reportes");

            Console.WriteLine("\r\n");

            Console.WriteLine("0) Salir");

            Console.WriteLine("\r\n");

            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1":
                    clientModule.Run();
                    return true;
                case "2":
                    productModule.Run();
                    return true;
                case "3":
                    saleModule.Run();
                    return true;
                case "4":
                    return true;
                case "5":
                    reportModule.Run();
                    return true;
                case "0":
                    return false;
                default:
                    MessageUtil.Message("Opción incorrecta");
                    return true;
            }
        }
    }
}
