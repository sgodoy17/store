using System;

namespace StoreTest.Utils
{
    public class MessageUtil
    {
        public static void Message(string message)
        {
            Console.WriteLine($"\r\n{message}");
            Console.Write("\r\nPresione la tecla Enter para continuar");
            Console.ReadLine();
        }
    }
}
