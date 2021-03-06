using StoreTest.Modules.Client.Entities;
using System.Collections.Generic;

namespace StoreTest.Modules.Client.Services
{
    public class ClientService
    {
        /// <summary>
        /// Este objeto es donde vamos a almacenar temporalmente los clientes
        /// </summary>
        readonly List<ClientEntity> clients = new List<ClientEntity>();

        /// <summary>
        /// Este metodo se utiliza para obtener la lista de clientes almacenados
        /// </summary>
        /// <returns>Retorna la lista de los clientes</returns>
        public List<ClientEntity> FindAll()
        {
            return clients;
        }

        public ClientEntity Find(string document)
        {
            var client = clients.Find(item => item.Document == document);

            if (client != null)
            {
                return client;
            }

            return null;
        }

        public bool Create(ClientEntity client)
        {
            if (FindIndex(client.Document) >= 0)
            {
                return false;
            }

            clients.Add(client);

            return true;
        }

        /// <summary>
        /// Se utiliza para obtener el ultimo indice en la lista
        /// </summary>
        /// <param name="document">documento a buscar en la lista</param>
        /// <returns>retorna el indice o -1 si no se consigue nada</returns>
        private int FindIndex(string document)
        {
            return clients.FindLastIndex(c => c.Document == document);
        }
    }
}
