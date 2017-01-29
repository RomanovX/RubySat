using System;
using System.IO;
using System.Collections.Generic;

namespace SatSolver
{
    public class Program
    {
        public static void Main()
        {
            while (true)
            {
                try
                {
                    String invoer = Console.ReadLine();
                    IFormule formule = Parser.ParseFormule(invoer);
                    Valuatie valuatie = Solver.Vervulbaar(formule);

                    if (valuatie == null)
                        Console.WriteLine("UNSAT");
                    else Console.WriteLine("SAT\n" + valuatie);
                }
                catch (Exception exc)
                {
                    Console.WriteLine("FOUT: " + exc.Message);
                }
                break;
            }
        }
    }
}
