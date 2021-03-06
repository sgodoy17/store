using StoreTest.Modules.Client.Entities;
using StoreTest.Modules.Client.Services;
using StoreTest.Modules.Home;
using StoreTest.Utils;
using System;
using System.Collections.Generic;

namespace StoreTest.Modules.Client
{
    public class ClientModule
    {
        protected HomeModule homeModule;

        /// <summary>
        /// Este es el servicio que se encarga de almacenar la información del cliente
        /// </summary>
        protected ClientService service;

        /// <summary>
        /// Utilizamos este para generar una nueva instancia del servicio de cliente
        /// que le estamos pasando desde el main
        /// </summary>
        /// <param name="homeModule">instancia de modulo home para tener persistencia de datos</param>
        /// <param name="service">instancia del servicio para mentener persistencia de datos</param>
        public ClientModule(HomeModule homeModule, ClientService service)
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

        /// <summary>
        /// Este es el modulo de clientes, se encarga de instanciar todo este metodo,
        /// es invocado por el modulo home, este se encarga de pintar el menu,
        /// Toda la logica de negocio esta en este metodo, digamos que es una especie de
        /// controlador.
        /// </summary>
        /// <returns>retorna un verdadero/falso</returns>
        public bool Menu()
        {
            // Limpia la consola
            Console.Clear();

            // Imprime el primer bloque del menu (crear, listar, buscar, editar, eliminar)
            Console.WriteLine("*******Modulo Clientes******");
            Console.WriteLine("1) Crear cliente");
            Console.WriteLine("2) Listar clientes");
            Console.WriteLine("3) Buscar cliente (documento)");
            Console.WriteLine("4) Editar cliente (documento)");
            Console.WriteLine("5) Eliminar cliente (document)");

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
                    CreateClient();
                    return true;
                case "2":
                    ListClients();
                    return true;
                case "3":
                    FindClient();
                    return true;
                case "4":
                    EditClient();
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
                    return true;
            }
        }

        private void ListClients()
        {
            // Limpia la consola
            Console.Clear();

            List<ClientEntity> clients = service.FindAll();

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

        private void CreateClient()
        {
            // Limpia la consola
            Console.Clear();

            ClientEntity client = new ClientEntity();

            Console.Write("Ingrese el Documento: ");
            client.Document = Console.ReadLine();

            Console.Write("Ingrese el Nombre: ");
            client.Name = Console.ReadLine();

            Console.Write("Ingregse la Dirección: ");
            client.Address = Console.ReadLine();

            Console.Write("Ingrese el Telefono: ");
            client.Phone = Console.ReadLine();

            if (! service.Create(client))
            {
                MessageUtil.Message("El numero de documento ya se encuentra en nuestra base de datos.");
            }
            else
            {
                MessageUtil.Message("Cliente creado satisfactoriamente");
            }
        }

        private void FindClient()
        {
            // Limpia la consola
            Console.Clear();

            Console.Write("Ingrese el Documento: ");
            ClientEntity client = service.Find(Console.ReadLine().ToString());

            if (client == null)
            {
                MessageUtil.Message("No se encontraron reguistros con ese numero de documento.");
            }
            else
            {
                MessageUtil.Message(client.ConvertToString());
            }
        }

        private void EditClient()
        {
            // Limpia la consola
            Console.Clear();

            Console.Write("Ingrese el Documento: ");
            ClientEntity client = service.Find(Console.ReadLine().ToString());

            if (client == null)
            {
                MessageUtil.Message("No se encontraron reguistros con ese numero de documento.");
            }
            else
            {
                bool exit = false;
                string message = "";

                while (! exit)
                {
                    // Limpia la consola
                    Console.Clear();

                    Console.WriteLine($"Cliente: {client.ConvertToString()}");

                    // Salto de linea
                    Console.WriteLine("\r\n");

                    Console.WriteLine("Que parametro desea cambiar?");
                    Console.WriteLine("1) Documento");
                    Console.WriteLine("2) Nombre");
                    Console.WriteLine("3) Dirección");
                    Console.WriteLine("4) Telefono");

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
                            Console.Write("Ingrese el Documento: ");
                            client.Document = Console.ReadLine();
                            exit = false;
                            break;
                        case "2":
                            // Limpia la consola
                            Console.Clear();

                            // Solicitar nombre
                            Console.Write("Ingrese el Nombre: ");
                            client.Name = Console.ReadLine();
                            exit = false;
                            break;
                        case "3":
                            // Limpia la consola
                            Console.Clear();

                            // Solicitar direccion
                            Console.Write("Ingregse la Dirección: ");
                            client.Address = Console.ReadLine();
                            exit = false;
                            break;
                        case "4":
                            // Limpia la consola
                            Console.Clear();

                            // Solicitar telefono
                            Console.Write("Ingrese el Telefono: ");
                            client.Phone = Console.ReadLine();
                            exit = false;
                            break;
                        case "9":
                            // Limpia la consola
                            Console.Clear();

                            // Llama al servicio, envia el cliente y procede a editarlo
                            service.Edit(client);
                            exit = true;
                            message = "Cliente editado satisfactoriamente";
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

            Console.Write("Ingrese el Documento: ");

            if (! service.Delete(Console.ReadLine().ToString()))
            {
                MessageUtil.Message("No se encontraron reguistros con ese numero de documento.");
            }
            else
            {
                MessageUtil.Message("Cliente eliminado satisfactoriamente");
            }
        }
    }
}
