using System.Linq;
using NUnit.Framework;
using ProNet.Util;

namespace ProNet.Test.Util
{
    [TestFixture]
    public class CombinationServiceTests
    {
        [TestCase(1, 5)]
        [TestCase(2, 20)]
        [TestCase(3, 60)]
        [TestCase(4, 120)]
        [TestCase(5, 120)]
        public void Correct_number_of_results(int size, int expected)
        {
            var set = new[] { 1, 2, 3, 4, 5 };
            var combinations = new CombinationService().GetPermutations(set, size);
            Assert.That(combinations.Count(), Is.EqualTo(expected));
        }

        [Test]
        public void No_duplicates_in_any_combination()
        {
            var set = new[] { 1, 2, 3, 4, 5 };
            var combinations = new CombinationService().GetPermutations(set, 3);
            var duplicateCombinations = combinations.Where(combination => combination.Distinct().Count() != combination.Count());
            Assert.That(duplicateCombinations.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Different_orderings_of_each_permutation_are_returned()
        {
            var set = new[] { 1, 2, 3, 4, 5 };
            var combinations = new CombinationService().GetPermutations(set, 3);
            var combinationsWithOneTwoAndThree = combinations.Where(combination => combination.Contains(1) && combination.Contains(2) && combination.Contains(3));
            Assert.That(combinationsWithOneTwoAndThree.Count(), Is.EqualTo(6));
        }
    }
}