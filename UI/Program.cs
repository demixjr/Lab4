using System;
using System.Collections.Generic;
using BLL;

namespace Lab_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceFacade facade = new ServiceFacade();
            ConsoleMenu consoleMenu = new ConsoleMenu(facade);
            consoleMenu.Menu();
        }
    }
}
