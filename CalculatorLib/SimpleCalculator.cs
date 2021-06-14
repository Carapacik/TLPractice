using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CalculatorLib.Operations;

namespace CalculatorLib
{
    public class SimpleCalculator : ICalculator
    {
        private readonly List<IOperation> _supportedOperations;

        public SimpleCalculator(List<IOperation> operations)
        {
            _supportedOperations = operations;
        }

        public int Calculate(string example)
        {
            var numberAndOperations = Regex
                .Replace(example, @"([\d]+)", "'$1'")
                .Split(new[] {'\''}, StringSplitOptions.RemoveEmptyEntries);

            var result = int.Parse(numberAndOperations[0]);

            for (var i = 1; i < numberAndOperations.Length; i += 2)
            {
                var operationCode = numberAndOperations[i].Trim();
                var operation = _supportedOperations.Find(x => x.OperationCode == operationCode);
                var number = int.Parse(numberAndOperations[i + 1]);
                result = operation.Apply(result, number);
            }

            return result;
        }
    }
}