using StoreTest.Modules.Home;
using StoreTest.Modules.Invoice.Entities;
using StoreTest.Modules.Invoice.Services;
using StoreTest.Utils;
using System;
using System.Collections.Generic;

namespace StoreTest.Modules.Invoice
{
    public class InvoiceModule
    {
        protected HomeModule homeModule;

        protected InvoiceService service;

        public InvoiceModule(HomeModule homeModule, InvoiceService service)
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

            // Imprime el primer bloque del menu
            Console.WriteLine("*******Modulo Facturas******");
            Console.WriteLine("1) Buscar factura (codigo)");
            Console.WriteLine("2) Busccar factura (documento)");
            Console.WriteLine("3) Listar facturas");
            Console.WriteLine("4) Listar facturas (desactivadas)");
            Console.WriteLine("5) Desactivar factura (codigo)");

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
                    FindInvoiceByCode();
                    return true;
                case "2":
                    FindInvoiceByDocument();
                    return true;
                case "3":
                    ListInvoices();
                    return true;
                case "4":
                    ListDisabledInvoices();
                    return true;
                case "5":
                    DisableInvoices();
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

        public void FindInvoiceByCode()
        {
            // Limpia la consola
            Console.Clear();

            Console.Write("Ingrese el Codigo: ");
            InvoiceEntity invoice = service.Find(Console.ReadLine().ToString());

            if (invoice == null)
            {
                MessageUtil.Message("No se encontraron reguistros con ese codigo.");
            }
            else
            {
                MessageUtil.Message(invoice.ConvertToString());
            }
        }

        public void FindInvoiceByDocument()
        {
            // Limpia la consola
            Console.Clear();

            Console.Write("Ingrese el Documento: ");
            InvoiceEntity invoice = service.FindByDocument(Console.ReadLine().ToString());

            if (invoice == null)
            {
                MessageUtil.Message("No se encontraron reguistros con ese numero de documento.");
            }
            else
            {
                MessageUtil.Message(invoice.ConvertToString());
            }
        }

        public void ListInvoices()
        {
            // Limpia la consola
            Console.Clear();

            List<InvoiceEntity> invoices = service.FindAll();

            if (invoices.Count >= 1)
            {
                MessageUtil.Simple("\n**********************************************************************************************************");

                foreach (InvoiceEntity invoice in invoices)
                {
                    if (invoice.Status)
                    {
                        MessageUtil.Simple(invoice.ConvertToString());

                        MessageUtil.Simple("\n**********************************************************************************************************\n");
                    }
                }

                MessageUtil.Message("Final de la lista");
            }
            else
            {
                MessageUtil.Message("No hay registros en la base de datos");
            }
        }

        public void ListDisabledInvoices()
        {
            // Limpia la consola
            Console.Clear();

            List<InvoiceEntity> invoices = service.FindAll();

            if (invoices.Count >= 1)
            {
                foreach (InvoiceEntity invoice in invoices)
                {
                    if (! invoice.Status)
                    {
                        MessageUtil.Simple(invoice.ConvertToString());
                    }
                }

                MessageUtil.Message("Final de la lista");
            }
            else
            {
                MessageUtil.Message("No hay registros en la base de datos");
            }
        }

        public void DisableInvoices()
        {
            // Limpia la consola
            Console.Clear();

            Console.Write("Ingrese el Codigo: ");

            if (service.Disable(Console.ReadLine().ToString()))
            {
                MessageUtil.Message("Factura deshabilitada satisfactoriamente.");
            }
            else
            {
                MessageUtil.Message("No existe la factura.");
            }
        }
    }
}
