namespace BaseLib
{
    public class CalculationBlocking : Calculation
    {
        public int Calculate(int value)
        {
            return value++;
        }
    }
}
