namespace Booster.Util
{
    public class Range
    {
        public float Min { get; set; }
        public float Max { get; set; }

        public Range(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public bool IsInRange(float number)
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