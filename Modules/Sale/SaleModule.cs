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

namespace StoreTest.Modules.Sale
{
    public class SaleModule
    {
        protected HomeModule homeModule;

        protected ClientService clientService;

        protected ProductService productService;

        protected InvoiceService invoiceService;

        public SaleModule(HomeModule homeModule, ClientService clientService, ProductService productService, InvoiceService invoiceService)
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

            // Imprime el primer bloque del menu (venta)
            Console.WriteLine("*******Modulo Ventas******");
            Console.WriteLine("1) Iniciar venta");

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
                    CreateSale();
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

        public void CreateSale()
        {
            // Limpia la consola
            Console.Clear();

            Console.Write("Ingrese el Documento: ");
            ClientEntity client = clientService.Find(Console.ReadLine().ToString());

            if (client == null)
            {
                MessageUtil.Message("No se encontraron reguistros con ese numero de documento.");
            }
            else
            {
                bool exit = false;
                string message = "";
                List<ProductEntity> products = new List<ProductEntity>();
                double total = 0;

                while (!exit)
                {
                    // Limpia la consola
                    Console.Clear();

                    Console.WriteLine($"Cliente: {client.ConvertToString()}");
                    
                    if (products.Count > 0)
                    {
                        foreach (ProductEntity product in products)
                        {
                            MessageUtil.Simple(product.ConvertToString());
                        }
                    }

                    // Salto de linea
                    Console.WriteLine("\r\n");

                    Console.WriteLine("Agregue productos a la venta");

                    // Salto de linea
                    Console.WriteLine("\r\n");


                    Console.WriteLine("1) Buscar producto (codigo)");

                    // Salto de linea
                    Console.WriteLine("\r\n");
                    Console.WriteLine("9) Guardar cambios");
                    Console.WriteLine("0) Cancelar");

                    // Salto de linea
                    Console.WriteLine("\r\n");

                    // Imprime Seleccionar opciones
                    Console.Write("Seleccione una opción: ");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            // Limpia la consola
                            Console.Clear();

                            Console.Write("Ingrese el Codigo: ");
                            ProductEntity product = productService.Find(Console.ReadLine().ToString());

                            if (product == null)
                            {
                                MessageUtil.Message("No se encontraron reguistros con ese codigo.");
                            }
                            else
                            {
                                Console.Write($"Ingrese la cantidad (existencia de {product.Amount}): ");
                                int amount = int.Parse(Console.ReadLine());
                                if (amount > product.Amount)
                                {
                                    MessageUtil.Message("El numero de productos supera el inventario.");
                                }
                                else
                                {
                                    total += product.Price * amount;
                                    products.Add(new ProductEntity(product.Code, product.Name, product.Price, amount));
                                }
                            }
                            exit = false;
                            break;
                        case "9":
                            // Limpia la consola
                            Console.Clear();
                            message = GenerateInvoice(client, products, total);
                            exit = true;
                            break;
                        case "0":
                            // Limpia la consola
                            Console.Clear();
                            exit = true;
                            message = "Operación cancelada";
                            break;
                        default:
                            exit = false;
                            break;
                    }
                }

                MessageUtil.Message(message);
            }
        }

        public void UpdateProducts(List<ProductEntity> products)
        {
            foreach (ProductEntity product in products)
            {
                productService.UpdateAmount(product);
            }
        }

        public string GenerateInvoice(ClientEntity client, List<ProductEntity> products, double total)
        {
            InvoiceEntity invoice = new InvoiceEntity
            {
                Code = invoiceService.GenerateCode(),
                Document = client.Document,
                Products = products,
                Total = total,
                Status = true,
                Created = DateTime.Now
            };

            if (invoiceService.Create(invoice))
            {
                UpdateProducts(invoice.Products);
                return invoice.ConverToString();
            }
            else
            {
                return "Ocurrio un error";
            }
        }
    }
}
