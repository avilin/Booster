namespace Booster
{
    public class Range
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public Range(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public bool IsInRange(int number)
        {
            if (number < Min)
            {
                return false;
            }
            if (number > Max)
            {
                return false;
            }
            return true;
        }
    }
}