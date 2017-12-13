using System.Linq;
using NUnit.Framework;
using ProNet.Util;

namespace ProNet.Test.Util
{
    [TestFixture]
    public class PermutationServiceTests
    {
        [TestCase(0, 1)]
        [TestCase(1, 5)]
        [TestCase(2, 10)]
        [TestCase(3, 10)]
        [TestCase(4, 5)]
        [TestCase(5, 1)]
        public void Correct_number_of_results(int size, int expected)
        {
            var set = new[] { 1, 2, 3, 4, 5 };
            var permutations = new PermutationService().GetPermutations(set, size);
            Assert.That(permutations.Count(), Is.EqualTo(expected));
        }

        [Test]
        public void No_duplicates_in_any_permutation()
        {
            var set = new[] { 1, 2, 3, 4, 5 };
            var permutations = new PermutationService().GetPermutations(set, 3);
            var duplicatePermutations = permutations.Where(permutation => permutation.Distinct().Count() != permutation.Count());
            Assert.That(duplicatePermutations.Count(), Is.EqualTo(0));
        }

        [Test]
        public void No_permutations_have_the_same_exact_items()
        {
            var set = new[] { 1, 2, 3, 4, 5 };
            var permutations = new PermutationService().GetPermutations(set, 3);
            var permutationsWithSameItems = permutations
                .Select(permutation => permutation.OrderBy(x => x))
                .Distinct();
            Assert.That(permutations.Count(), Is.EqualTo(permutationsWithSameItems.Count()));
        }
    }
}
