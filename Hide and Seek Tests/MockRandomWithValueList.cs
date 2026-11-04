namespace Hide_and_Seek_Tests
{
    /// <summary>
    /// Mock Random for testing that uses a list to return values
    /// </summary>
    public class MockRandomWithValueList : System.Random
    {
        private Queue<int> valuesToReturn;
        public MockRandomWithValueList(IEnumerable<int> values) =>
            valuesToReturn = new Queue<int>(values);

        /// <summary>
        /// get the next value in the queue and also move it to the back of the queue
        /// </summary>
        /// <returns></returns>
        public int NextValue()
        {
            var nextValue = valuesToReturn.Dequeue();
            valuesToReturn.Enqueue(nextValue);
            return nextValue;
        }

        public override int Next() => NextValue();
        public override int Next(int maxValue) => Next(0, maxValue);
        public override int Next(int minValue, int maxValue)
        {
            var next = NextValue();
            return next > minValue && next < maxValue ? next : minValue;
        }
    }
}
