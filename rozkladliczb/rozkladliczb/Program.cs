using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rozkladliczb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int input = Convert.ToInt16(Console.ReadLine());
            int a = input;
            while (a != 1)
            {   
                for (int i = 2; i <= a; i++)
                {
                    if (a % i == 0)
                    {
                        a = a / i;
                        Console.WriteLine(i);
                        break;
                    }
                }
            }
        }
    }
}
