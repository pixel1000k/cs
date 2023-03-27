using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace liczbypierwsze
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int input = Convert.ToInt16(Console.ReadLine());
            bool LiczbaPierwsza = true;
            for (int i = 2; i*i < input; i++)
            {
                if (input % i == 0)
                {
                    LiczbaPierwsza = false;
                }
            }
            Console.WriteLine(LiczbaPierwsza);
        }
    }
}
