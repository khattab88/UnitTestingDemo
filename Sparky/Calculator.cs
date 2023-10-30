namespace Sparky
{
    public class Calculator
    {
        public List<int> NumbersRange = new();

        public int Add(int a, int b)
        {
            return a + b;
        }

        public double AddDouble(double a, double b)
        {
            return a + b;
        }

        public bool IsOdd(int a)
        {
            return a % 2 != 0;
        }

        public List<int> GetOddNumbersRange(int min, int max)
        {
            this.NumbersRange.Clear();

            for (int i = min; i <= max; i++)
            {
                if(i % 2 != 0)
                {
                    this.NumbersRange.Add(i);
                }
            }

            return this.NumbersRange;
        }
    }
}