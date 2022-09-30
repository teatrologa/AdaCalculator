using AdaCalculator;
using Moq;
using Shouldly;

namespace AdaCalculatorTest
{
    public class CalculatorTests
    {
        private readonly CalculatorMachine _calculatorMachine;
        private readonly CalculatorMachine _calculatorMachineMock;
        private readonly Mock<ICalculator> _calcMock;

        public CalculatorTests()
        {
            _calculatorMachine = new CalculatorMachine();
            _calcMock = new Mock<ICalculator>();
            _calculatorMachineMock = new CalculatorMachine(_calcMock.Object);
        }


        #region Testes Positivos (Sem Mock)

        [Theory]
        [InlineData("sum", 21, 34, 55)]
        [InlineData("sum", -56, -42, -98)]
        [InlineData("sum", -48, 97, 49)]
        [InlineData("sum", -9, 0, -9)]
        [InlineData("sum", 9, -9, 0)]
        public void CalculatorMachine_Soma_Valido(string operation, double n1, double n2, double result)
        {
            var calc = _calculatorMachine.Calculate(operation, n1, n2);
            
            calc.ShouldBe((operation, result));
        }

        [Theory]
        [InlineData("subtract", 21, 37, -16)]
        [InlineData("subtract", -56, -42, -14)]
        [InlineData("subtract", 97, 9, 88)]
        [InlineData("subtract", -9, 10, -19)]
        [InlineData("subtract", 9, -6, 15)]
        [InlineData("subtract", 0, -6, 6)]
        public void CalculatorMachine_Subtracao_Valido(string operation, double n1, double n2, double result)
        {
            var calc = _calculatorMachine.Calculate(operation, n1, n2);

            calc.ShouldBe((operation, result));
        }

        [Theory]
        [InlineData("divide", 21, 3, 7)]
        [InlineData("divide", 21, 9, 2.33)]
        [InlineData("divide", 21, 0, double.PositiveInfinity)]
        public void CalculatorMachine_Divisao_Valido(string operation, double n1, double n2, double result)
        {
            var calc = _calculatorMachine.Calculate(operation, n1, n2);

            calc.ShouldBe((operation, result));
        }

        #endregion

        #region Testes Positivos (Com Mock)

        [Theory]
        [InlineData("sum", 1, 1, 2)]
        [InlineData("sum", 21, 34, 55)]
        [InlineData("sum", -56, -42, -98)]
        [InlineData("sum", -48, 97, 49)]
        [InlineData("sum", -9, 0, -9)]
        [InlineData("sum", 9, -9, 0)]
        public void Calculator_SomaMock_Valido(string operation, double n1, double n2, double result)
        {
            _calcMock.Setup(calc => calc.Calculate(operation, n1, n2)).Returns((operation, result));
            var metodoUsado = _calculatorMachineMock.Calculate(operation, n1, n2);
            _calcMock.Verify(c => c.Calculate(operation, n1, n2), Times.Once);
        }


        #endregion



        #region Testes Negativos

        [Theory]
        [InlineData("sum", 2, 3, 4)]
        [InlineData("sum", -2, 3, 5)]
        public void CalculatorMachine_Soma_Invalido(string operation, double n1, double n2, double result)
        {
            var calc = _calculatorMachine.Calculate(operation, n1, n2);
           
            calc.ShouldNotBe((operation, result));
        }

        //Divisão por 0 deve dar erro

        #endregion
    }
}