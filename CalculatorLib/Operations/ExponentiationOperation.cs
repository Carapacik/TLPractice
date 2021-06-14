using System;

namespace CalculatorLib.Operations
{
    public class ExponentiationOperation : IOperation
    {
        public string OperationCode => "^";

        public int Apply(int operand1, int operand2)
        {
            return (int) Math.Pow(operand1, operand2);
        }
    }
}