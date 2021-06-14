using System.Collections.Generic;
using CalculatorLib.Operations;
using Xunit;

namespace CalculatorLib.Tests
{
    public class SimpleCalculatorTest
    {
        [Fact]
        public void SimpleCalculator_Calculate_AdditionOperation_Calculated()
        {
            //Arrange
            var operations = new List<IOperation>
            {
                new AdditionOperation()
            };

            var simpleCalculator = new SimpleCalculator(operations);

            //Act
            var result = simpleCalculator.Calculate("2+2");

            //Assert
            Assert.Equal(4, result);
        }

        [Fact]
        public void SimpleCalculator_Calculate_DivisionOperation_Calculated()
        {
            //Arrange
            var operations = new List<IOperation>
            {
                new DivisionOperation()
            };

            var simpleCalculator = new SimpleCalculator(operations);

            //Act
            var result = simpleCalculator.Calculate("4/2");

            //Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void SimpleCalculator_Calculate_DivisionWithRemainderOperation_Calculated()
        {
            //Arrange
            var operations = new List<IOperation>
            {
                new DivisionWithRemainderOperation()
            };

            var simpleCalculator = new SimpleCalculator(operations);

            //Act
            var result = simpleCalculator.Calculate("4%3");

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void SimpleCalculator_Calculate_MultiplicationOperation_Calculated()
        {
            //Arrange
            var operations = new List<IOperation>
            {
                new MultiplicationOperation()
            };

            var simpleCalculator = new SimpleCalculator(operations);

            //Act
            var result = simpleCalculator.Calculate("3*2");

            //Assert
            Assert.Equal(6, result);
        }

        [Fact]
        public void SimpleCalculator_Calculate_SubtractionOperation_Calculated()
        {
            //Arrange
            var operations = new List<IOperation>
            {
                new SubtractionOperation()
            };

            var simpleCalculator = new SimpleCalculator(operations);

            //Act
            var result = simpleCalculator.Calculate("1-2");

            //Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void SimpleCalculator_Calculate_ExponentiationOperation_Calculated()
        {
            //Arrange
            var operations = new List<IOperation>
            {
                new ExponentiationOperation()
            };

            var simpleCalculator = new SimpleCalculator(operations);

            //Act
            var result = simpleCalculator.Calculate("3^4");

            //Assert
            Assert.Equal(81, result);
        }
    }
}