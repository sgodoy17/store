using StoreTest.Modules.Client.Entities;
using StoreTest.Modules.Client.Services;
using StoreTest.Modules.Home;
using StoreTest.Modules.Invoice.Entities;
using StoreTest.Modules.Invoice.Services;
using StoreTest.Modules.Product.Entities;
using StoreTest.Modules.Product.Services;
using StoreTest.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreTest.Modules.Report
{
    public class ReportModule
    {
        protected HomeModule homeModule;

        protected ClientService clientService;

        protected ProductService productService;

        protected InvoiceService invoiceService;

        public ReportModule(HomeModule homeModule, ClientService clientService, ProductService productService, InvoiceService invoiceService)
        {
            this.homeModule = homeModule;
            this.clientService = clientService;
            this.productService = productService;
            this.invoiceService = invoiceService;
        }

        public void Run(bool showMenu = true)
        {
            while (showMenu)
            {
                showMenu = Menu();
            }

            // Set goodybye message if needed! MessageUtil.SimpleMessage("Adios") or something like that!
            Environment.Exit(0);
        }

        public bool Menu()
        {
            // Limpia la consola
            Console.Clear();

            // Imprime el primer bloque del menu
            Console.WriteLine("*******Modulo Reportes******");
            Console.WriteLine("1) Reporte Clientes");
            Console.WriteLine("2) Reporte Productos");
            Console.WriteLine("3) Reporte Facturas");

            // Salto de linea
            Console.WriteLine("\r\n");

            // Imprime las opciones de regresar al menu principal o salir del sistema
            Console.WriteLine("9) Menu principal");
            Console.WriteLine("0) Salir");

            // Salto de linea
            Console.WriteLine("\r\n");

            // Imprime Seleccionar opciones
            Console.Write("Seleccione una opción: ");

            /* 
             * Si la opcion no existe, va a mostrar un mensaje para intentar con una opcion diferente,             
             * recordar que todo esto esta dentro de un while, hasta que no se retorne a HomeModule.Run un false
             * el programa va a seguir pidiendo opciones! 
             */
            switch (Console.ReadLine())
            {
                case "1":
                    ListClients();
                    return true;
                case "2":
                    ListProducts();
                    return true;
                case "3":
                    ListInvoices();
                    return true;
                case "9":
                    // HomeModule por defecto recibe un true, esto es para volver al menu principal
                    homeModule.Run();
                    return true;
                case "0":
                    return false;
                default:
                    MessageUtil.Message("Opción incorrecta");
                    return true;
            }
        }

        public void ListClients()
        {
            // Limpia la consola
            Console.Clear();

            List<ClientEntity> clients = clientService.FindAll();

            if (clients.Count >= 1)
            {
                foreach (ClientEntity client in clients)
                {
                    MessageUtil.Simple(client.ConvertToString());
                }

                MessageUtil.Message("Final de la lista");
            }
            else
            {
                MessageUtil.Message("No hay registros en la base de datos");
            }
        }

        public void ListProducts()
        {
            // Limpia la consola
            Console.Clear();

            List<ProductEntity> products = productService.FindAll();

            if (products.Count >= 1)
            {
                foreach (ProductEntity product in products)
                {
                    MessageUtil.Simple(product.ConvertToString());
                }

                MessageUtil.Message("Final de la lista");
            }
            else
            {
                MessageUtil.Message("No hay registros en la base de datos");
            }
        }

        public void ListInvoices()
        {
            // Limpia la consola
            Console.Clear();

            List<InvoiceEntity> invoices = invoiceService.FindAll();

            if (invoices.Count >= 1)
            {
                foreach (InvoiceEntity invoice in invoices)
                {
                    MessageUtil.Simple(invoice.ConverToStringHeader());
                }

                MessageUtil.Message("Final de la lista");
            }
            else
            {
                MessageUtil.Message("No hay registros en la base de datos");
            }
        }
    }
}
