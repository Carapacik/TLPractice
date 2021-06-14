namespace CalculatorLib.Operations
{
    public class MultiplicationOperation : IOperation
    {
        public string OperationCode => "*";

        public int Apply(int operand1, int operand2)
        {
            return operand1 * operand2;
        }
    }
}