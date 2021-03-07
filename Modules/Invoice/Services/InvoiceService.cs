using StoreTest.Modules.Invoice.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreTest.Modules.Invoice.Services
{
    public class InvoiceService
    {
        protected List<InvoiceEntity> invoices = new List<InvoiceEntity>();

        public List<InvoiceEntity> FindAll()
        {
            return invoices;
        }

        public InvoiceEntity Find(string code)
        {
            return invoices.Find(item => item.Code == code);
        }

        public InvoiceEntity FindByDocument(string document)
        {
            return invoices.Find(item => item.Document == document);
        }

        public bool Create(InvoiceEntity invoice)
        {
            if (FindIndex(invoice.Code) >= 0)
            {
                return false;
            }

            invoices.Add(invoice);

            return true;
        }

        public bool Disable(string code)
        {
            InvoiceEntity invoice = Find(code);

            if (! invoice.Status)
            {
                return false;
            }

            int index = FindIndex(code);

            if (index >= 0)
            {
                invoices[index].Status = false;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Este bloque es copiado de internet, es para generar un caracter aleatorio
        /// </summary>
        /// <returns></returns>
        public string GenerateCode()
        {
            StringBuilder strbuilder = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < 8; i++)
            {
                double myFloat = random.NextDouble();
                var myChar = Convert.ToChar(Convert.ToInt32(Math.Floor(25 * myFloat) + 65));

                strbuilder.Append(myChar);
            }

            return strbuilder.ToString();
        }

        /// <summary>
        /// Se utiliza para obtener el ultimo indice en la lista
        /// </summary>
        /// <param name="code">codigo a buscar en la lista</param>
        /// <returns>retorna el indice o -1 si no se consigue nada</returns>
        private int FindIndex(string code)
        {
            return invoices.FindLastIndex(c => c.Code == code);
        }
    }
}
