using StoreTest.Modules.Client;
using StoreTest.Modules.Client.Services;
using StoreTest.Utils;
using System;

namespace StoreTest.Modules.Home
{
    public class HomeModule
    {
        protected ClientModule clientModule;

        public HomeModule()
        {
            clientModule = new ClientModule(this, new ClientService());
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

        private bool Menu()
        {
            Console.Clear();

            Console.WriteLine("*******Nombre de la empresa******");
            Console.WriteLine("1) Modulo clientes");
            Console.WriteLine("2) Modulo productos");
            Console.WriteLine("3) Modulo ventas");
            Console.WriteLine("4) Modulo facturas");

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
                    return true;
                case "3":
                    return true;
                case "4":
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
