using System.Numerics;
using Xunit;
using Xunit.Abstractions;
using M = System.Math;

namespace UnitTests.Math
{
    /// <summary>
    /// This class contains unit tests, which will always pass and write log messages to test runner.
    /// Purpose of these tests is to perform estimations, based on probability theory, if it makes sense to apply LRU cache in your particular case
    /// </summary>
    public class Probabilities
    {
        private readonly ITestOutputHelper _output;
        public Probabilities(ITestOutputHelper output)
        {
            _output = output;
        }

        /// <summary>
        /// Assume we have 'totalItems' items total. Among these items we have a group, which takes 'oftenRequestedGroupSizePercent' percent of 'totalItems', which we call "Often requested items group". 
        /// The probability that we will receive item from "Often requested items" group is 'oftenRequestedGroupProbability'.
        /// Find probability of getting single item from "Often requested items" group.
        /// </summary>
        /// <remarks>In real world 'totalItems' is the count of items in your data set. F.e. you have 5_000 products in db in total</remarks>
        [Theory]
        /* 
         * Assume we have 5_000 items total. Among these items we have a group, which takes 50 percent of 5_000, which we call "Often requested items group". 
         * The probability that we will receive item from "Often requested items" group is 0.5 (50 percent).
         * Find probability of getting single item from "Often requested items" group.
         */
        [InlineData(5_000, 50, 0.5)] // 0.0002 - all items have equal probability (uniform distribution)
        [InlineData(5_000, 20, 0.8)] // 0.0008 - 20 percent of items have 80 percent probability (Pareto distribution)
        public void CalculateItemAppearanceProbability(int totalItems, int oftenRequestedGroupSizePercent, double oftenRequestedGroupProbability)
        {
            // 1 = oftenRequestedGroupProbability + rareRequestedGroupProbability
            // oftenRequestedGroupProbability = oftenRequestedItemsCount*oftenRequestedITemProbability
            // oftenRequestedItemsCount = totalItems *  oftenRequestedGroupSizePercent / 100
            // oftenRequestedITemProbability = oftenRequestedGroupProbability/oftenRequestedItemsCount

            var oftenRequestedItemsCount = totalItems * oftenRequestedGroupSizePercent / 100;
            var oftenRequestedItemProbability = oftenRequestedGroupProbability / oftenRequestedItemsCount;

            Log($"{oftenRequestedItemProbability:0.#######}");
        }

        /// <summary>
        /// https://www.matburo.ru/tvart_sub.php?p=calc_bern_naiv#calc
        /// </summary>
        /// <remarks>
        /// In real world scenario, this test will help ypu to determine the size of LRU cache you need, in order to allow items to be fetched from cache.
        /// If value you will receive will be greater or equal to items count in your dataset - appluing LRU does not make sense for you.
        /// If value is less then you total data set - you can try. Use tests from 'UnitTests/MemoryConsumptionTests' to determine if you have enough free memory to use LRU of requested size.
        /// F.e. your total data set items count is 5000. Probability of items appearance you need is '0.0008', and you would like that at least one time this item will be fetched from cache to justify its presense.
        /// Then you will need cache size from 1250 to 3749 items, which is less then 5000, so it makes sense.
        /// </remarks>
        [Theory]
        [InlineData(2, 0.0002)] // 5001-14999 - greater then amount of items in set, does not make sense to apply LRU cache (uniform distribution)
        [InlineData(2, 0.0008)] // 1250-3749 - less then amount of items in set, makes sense to apply LRU cache (Pareto distribution)
        [InlineData(3, 0.0002)] // 10001-19999 - greater then amount of items in set, does not make sense to apply LRU cache (uniform distribution)
        [InlineData(3, 0.0008)] // 2500-4999 - less then amount of items in set, makes sense to apply LRU cache (Pareto distribution)
        public void CalculateNumberOfTakesForMatchCount(int desiredNumberOfMatches, double successProbability)
        {
            // np - (1 - p) < T
            // mp + (1 - p) > T

            // n ?

            // n < (T + (1-p))/p
            // n > (T - (1-p))/p

            var min = (desiredNumberOfMatches - (1 - successProbability)) / successProbability;
            var max = (desiredNumberOfMatches + (1 - successProbability)) / successProbability;

            Log($"To archieve desired matches count, number of takes should lie between: {M.Floor(min)} - {M.Floor(max)}");
            // .... but not larger then total items set
        }

        /// <summary>
        /// Assume we have 'totalItems' total takes for some event. Event probability is 'successProbability'. What is the most probable amount of matches ?
        /// https://www.matburo.ru/tvart_sub.php?p=calc_bern_naiv#calc
        /// </summary>
        /// <remarks>
        /// This test could be useful in real world if you already know you memory limit and you have calculated what capasity cache you can create in this limit (probably you used tests from 'UnitTests/MemoryConsumptionTests'). 
        /// With this test you can check this LRU cache capasity to see if it will work.
        /// </remarks>
        [Theory]
        /*
         * Assume we have 5_000 total takes for some event. Event probability is 0.0002. What is the most probable amount of matches ?
         */
        [InlineData(5_000, 0.0002)] // 0 - 2 (uniform distribution)
        [InlineData(5_000, 0.0008)] // 3 - 5 (Pareto distribution)
        public void CalculateMostProbableMatchCount(int totalItems, double successProbability)
        {
            // np - (1 - p) < T
            // mp + (1 - p) > T

            // n ?

            // n < T + (1-p)/p
            // n > T - (1-p)/p

            var min = totalItems * successProbability - (1 - successProbability);
            var max = totalItems * successProbability + (1 + successProbability);

            Log($"Most probable amount of matches for the event lies between: {M.Floor(min)} - {M.Floor(max)}");
        }

        /// <summary>
        /// Assume we have 'numberOfEvents' total events. Assume we expect event to be happen exactly 'expectedOccurencies' times. Event probability is 'probabilityOfItem'.
        /// What is the probability of event to be happen exact 'expectedOccurencies' times in 'numberOfEvents' total events.
        /// https://www.matburo.ru/tvbook_sub.php?p=par17
        /// </summary>
        /// <remarks>This does not seems to be very usefull in the topic of the article, but why not ? :)</remarks>
        [Theory]
        [InlineData(5_000, 2, 0.0002)] // ~ 0.1839
        public void CacheItemTouchedExactTimesProbability(int numberOfEvents, int expectedOccurencies, double probabilityOfItem)
        {
            // 1. Cnk
            // 2. pK
            // 3. qN-K

            var pPowK = M.Pow(probabilityOfItem, expectedOccurencies);
            var qPowNmK = M.Pow(1 - probabilityOfItem, numberOfEvents - expectedOccurencies);
            var CnK = (double)Combination(n: numberOfEvents, k: expectedOccurencies);

            var probability = CnK * (pPowK * qPowNmK);

            Log($"Probability of {expectedOccurencies} times item occurence is: {probability:0.#######}");
        }

        private static BigInteger Combination(int n, int k)
        {
            var n1 = Factorial(n);
            var d1 = Factorial(n - k);
            var d2 = Factorial(k);
            var dt = d1 * d2;
            var result = n1 / dt;
            return result;
        }

        private static BigInteger Factorial(long num)
        {
            BigInteger n = num;
            for (long i = num - 1; i > 0; i--)
            {
                n *= i;
            }
            return n;
        }

        private void Log(string message)
        {
            _output.WriteLine(message);
        }
    }
}
