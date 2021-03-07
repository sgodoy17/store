using StoreTest.Modules.Product.Entities;
using System.Collections.Generic;

namespace StoreTest.Modules.Product.Services
{
    public class ProductService
    {
        /// <summary>
        /// Este objeto es donde vamos a almacenar temporalmente los productos
        /// </summary>
        protected List<ProductEntity> products = new List<ProductEntity>();

        /// <summary>
        /// Este metodo se utiliza para obtener la lista de productos almacenados
        /// </summary>
        /// <returns>Retorna la lista de los productos</returns>
        public List<ProductEntity> FindAll()
        {
            return products;
        }

        public ProductEntity Find(string code)
        {
            return products.Find(item => item.Code == code);
        }

        public bool Create(ProductEntity product)
        {
            if (FindIndex(product.Code) >= 0)
            {
                return false;
            }

            products.Add(product);

            return true;
        }

        public bool Edit(ProductEntity product)
        {
            int index = FindIndex(product.Code);

            if (index >= 0)
            {
                products[index] = product;

                return true;
            }

            return false;
        }

        public void UpdateAmount(ProductEntity product)
        {
            int index = FindIndex(product.Code);
            ProductEntity old = Find(product.Code);

            if (index >= 0)
            {
                int amount = old.Amount - product.Amount;
                products[index].Amount = amount;
            }
        }

        public bool Delete(string code)
        {
            int index = FindIndex(code);

            if (index >= 0)
            {
                products.RemoveAt(index);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Se utiliza para obtener el ultimo indice en la lista
        /// </summary>
        /// <param name="code">documento a buscar en la lista</param>
        /// <returns>retorna el indice o -1 si no se consigue nada</returns>
        private int FindIndex(string code)
        {
            return products.FindLastIndex(c => c.Code == code);
        }
    }
}
