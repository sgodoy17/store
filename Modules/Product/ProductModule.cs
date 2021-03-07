using StoreTest.Modules.Home;
using StoreTest.Modules.Product.Entities;
using StoreTest.Modules.Product.Services;
using StoreTest.Utils;
using System;
using System.Collections.Generic;

namespace StoreTest.Modules.Product
{
    public class ProductModule
    {
        protected HomeModule homeModule;

        protected ProductService service;

        public ProductModule(HomeModule homeModule, ProductService service)
        {
            this.homeModule = homeModule;
            this.service = service;
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

            // Imprime el primer bloque del menu (crear, listar, buscar, editar, eliminar)
            Console.WriteLine("*******Modulo Productos******");
            Console.WriteLine("1) Crear producto");
            Console.WriteLine("2) Listar productos");
            Console.WriteLine("3) Buscar producto (codigo)");
            Console.WriteLine("4) Editar producto (codigo)");
            Console.WriteLine("5) Eliminar producto (codigo)");

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
                    CreateProduct();
                    return true;
                case "2":
                    ListProducts();
                    return true;
                case "3":
                    FindProduct();
                    return true;
                case "4":
                    EditProduct();
                    return true;
                case "5":
                    DeleteClient();
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

        private void ListProducts()
        {
            // Limpia la consola
            Console.Clear();

            List<ProductEntity> products = service.FindAll();

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

        private void CreateProduct()
        {
            // Limpia la consola
            Console.Clear();

            ProductEntity product = new ProductEntity();

            Console.Write("Ingrese el Codigo: ");
            product.Code = Console.ReadLine();

            Console.Write("Ingrese el Nombre: ");
            product.Name = Console.ReadLine();

            Console.Write("Ingregse el Precio: ");
            product.Price = double.Parse(Console.ReadLine());

            Console.Write("Ingrese la Cantidad: ");
            product.Amount = int.Parse(Console.ReadLine());

            if (!service.Create(product))
            {
                MessageUtil.Message("El codigo ya se encuentra en nuestra base de datos.");
            }
            else
            {
                MessageUtil.Message("Producto creado satisfactoriamente");
            }
        }

        private void FindProduct()
        {
            // Limpia la consola
            Console.Clear();

            Console.Write("Ingrese el Codigo: ");
            ProductEntity product = service.Find(Console.ReadLine().ToString());

            if (product == null)
            {
                MessageUtil.Message("No se encontraron reguistros con ese codigo.");
            }
            else
            {
                MessageUtil.Message(product.ConvertToString());
            }
        }

        private void EditProduct()
        {
            // Limpia la consola
            Console.Clear();

            Console.Write("Ingrese el Codigo: ");
            ProductEntity product = service.Find(Console.ReadLine().ToString());

            if (product == null)
            {
                MessageUtil.Message("No se encontraron reguistros con ese codigo.");
            }
            else
            {
                bool exit = false;
                string message = "";

                while (!exit)
                {
                    // Limpia la consola
                    Console.Clear();

                    Console.WriteLine($"Producto: {product.ConvertToString()}");

                    // Salto de linea
                    Console.WriteLine("\r\n");

                    Console.WriteLine("Que parametro desea cambiar?");
                    Console.WriteLine("1) Codigo");
                    Console.WriteLine("2) Nombre");
                    Console.WriteLine("3) Precio");
                    Console.WriteLine("4) Cantidad");

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

                            // Solicitar documento
                            Console.Write("Ingrese el Codigo: ");
                            product.Code = Console.ReadLine();
                            exit = false;
                            break;
                        case "2":
                            // Limpia la consola
                            Console.Clear();

                            // Solicitar nombre
                            Console.Write("Ingrese el Nombre: ");
                            product.Name = Console.ReadLine();
                            exit = false;
                            break;
                        case "3":
                            // Limpia la consola
                            Console.Clear();

                            // Solicitar direccion
                            Console.Write("Ingregse el Precio: ");
                            product.Price = double.Parse(Console.ReadLine());
                            exit = false;
                            break;
                        case "4":
                            // Limpia la consola
                            Console.Clear();

                            // Solicitar telefono
                            Console.Write("Ingrese la Cantidad: ");
                            product.Amount = int.Parse(Console.ReadLine());
                            exit = false;
                            break;
                        case "9":
                            // Limpia la consola
                            Console.Clear();

                            // Llama al servicio, envia el cliente y procede a editarlo
                            service.Edit(product);
                            exit = true;
                            message = "Producto editado satisfactoriamente";
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

        private void DeleteClient()
        {
            // Limpia la consola
            Console.Clear();

            Console.Write("Ingrese el Codigo: ");

            if (! service.Delete(Console.ReadLine().ToString()))
            {
                MessageUtil.Message("No se encontraron reguistros con ese codigo.");
            }
            else
            {
                MessageUtil.Message("Producto eliminado satisfactoriamente");
            }
        }
    }
}
