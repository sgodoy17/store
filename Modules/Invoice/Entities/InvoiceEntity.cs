using StoreTest.Modules.Product.Entities;
using System;
using System.Collections.Generic;

namespace StoreTest.Modules.Invoice.Entities
{
    public class InvoiceEntity
    {
        protected string code;

        protected string document;

        protected List<ProductEntity> products;

        protected double total;

        protected bool status;

        protected DateTime created;

        public InvoiceEntity() { }

        public InvoiceEntity(string code, string document, List<ProductEntity> products, double total, bool status, DateTime created)
        {
            this.code = code;
            this.document = document;
            this.products = products;
            this.total = total;
            this.status = status;
            this.created = created;
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Document
        {
            get { return document; }
            set { document = value; }
        }

        public List<ProductEntity> Products
        {
            get { return products; }
            set { products = value; }
        }

        public double Total
        {
            get { return total; }
            set { total = value; }
        }

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }

        public DateTime Created
        {
            get { return created; }
            set { created = value; }
        }

        public string ConverToStringHeader()
        {
            return $"Factura: {code},  Documento: {document}, Total: {total}, Fecha: {created} \n";
        }

        public string ConvertToString()
        {
            string title = "\n *******Nombre de la empresa****** \n";
            string header = $"Factura: {code}                   Documento: {document}            Fecha: {created} \n";
            string bodyTitle = "Detalle de la Compra: \n\n";
            string body = "";
            
            foreach (ProductEntity product in products)
            {
                body += $"{product.ConvertToString()}, Total: {product.Amount*product.Price}\n";
            }

            string footer = $"\n\n                                                              Total: {total}";

            return $"{title}{header}{bodyTitle}{body}{footer}";
        }
    }
}
