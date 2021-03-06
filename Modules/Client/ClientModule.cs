using StoreTest.Modules.Client.Entities;
using StoreTest.Modules.Client.Services;
using StoreTest.Modules.Home;
using StoreTest.Utils;
using System;

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
                    MessageUtil.Message(CreateClient());
                    return true;
                case "2":
                    return true;
                case "3":
                    MessageUtil.Message(FindClient());
                    return true;
                case "4":
                    return true;
                case "5":
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

        private string CreateClient()
        {
            // Limpia la consola
            Console.Clear();

            ClientEntity client = new ClientEntity();

            Console.Write("Ingrese el Nombre: ");
            client.Name = Console.ReadLine();

            Console.Write("Ingrese el Documento: ");
            client.Document = Console.ReadLine();

            Console.Write("Ingregse la Dirección: ");
            client.Address = Console.ReadLine();

            Console.Write("Ingrese el Telefono: ");
            client.Phone = Console.ReadLine();

            if (! service.Create(client))
            {
                return "El numero de documento ya se encuentra en nuestra base de datos.";
            }

            return "Cliente agregador satisfactoriamente";
        }

        private string FindClient()
        {
            // Limpia la consola
            Console.Clear();

            Console.Write("Ingrese el Documento: ");
            ClientEntity client = service.Find(Console.ReadLine().ToString());

            if (client == null)
            {
                return "No se encontraron reguistros con ese numero de documento.";
            }

            return client.ConvertToString();
        }
    }
}
