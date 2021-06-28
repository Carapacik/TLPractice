using System;
using System.Collections.Generic;
using CalculatorLib;
using CalculatorLib.Operations;

namespace InterfaceLab
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 1) throw new ArgumentException("Please specify calculation string");

            var operations = new List<IOperation>
            {
                new AdditionOperation(),
                new SubtractionOperation()
            };
            var calculator = new SimpleCalculator(operations);
            var result = calculator.Calculate(args[0]);
            Console.WriteLine($"Result: {result}");
        }
    }
}