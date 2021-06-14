namespace CalculatorLib.Operations
{
    public interface IOperation
    {
        string OperationCode { get; }
        int Apply(int operand1, int operand2);
    }
}