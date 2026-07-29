namespace MarkusCrewUnitTests
{
    public class UnitTest1
    {
        List<int> numbers = Enumerable.Range(1, 40).ToList();



        [Fact]
        public void YieldingTest()
        {
            IEnumerable<int> shuffledList = numbers.Shuffle();
            int position = -1;

            for (int i = 0; i < 4; i++)
            {
                var randomNumbers = shuffledList.Take(new Range(i*10, i*10+10));

                if(randomNumbers.Contains(33))
                {
                    position = i;
                }
            }

            Assert.True(position >= 0);
        }
    }
}
