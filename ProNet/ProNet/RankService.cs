using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class RankService : IRankService
    {
        private int _iteration;
        private readonly IProgrammer[] _programmers;
        private readonly double[] _ranks;

        public RankService(IProgrammerRepository programmerRepository)
        {
            _programmers = programmerRepository.GetAll().ToArray();
            _ranks = new double[_programmers.Length];
        }

        public double GetRank(string programmerId)
        {
            const int settleLimit = 20;
            const double dampingFactor = 0.85d;

            var indeces = new Dictionary<string, int>();
            for (var i = 0; i < _programmers.Length; i++)
                indeces.Add(_programmers[i].GetId(), i);

            while (++_iteration < settleLimit)
                for (var i = 0; i < _programmers.Length; i++)
                    _ranks[i] = 1 - dampingFactor + dampingFactor * _programmers[i].GetRecommenders(_programmers).Select(p => GetRank(p.GetId()) / RecommendationCount(p)).Sum();

            try
            {
                return _ranks[indeces[programmerId]];
            }
            catch (KeyNotFoundException e)
            {
                throw new ArgumentException($"Programmer {programmerId} was not found");
            }
        }

        private static int RecommendationCount(IRankable programmer)
        {
            return programmer.GetRecommendations().Count();
        }
    }
}