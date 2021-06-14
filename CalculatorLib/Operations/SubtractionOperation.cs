namespace CalculatorLib.Operations
{
    public class SubtractionOperation : IOperation
    {
        public string OperationCode => "-";

        public int Apply(int operand1, int operand2)
        {
            return operand1 - operand2;
        }
    }
}