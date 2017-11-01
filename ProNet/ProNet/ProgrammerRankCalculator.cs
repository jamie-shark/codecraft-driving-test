using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class ProgrammerRankCalculator : IRankCalculator
    {
        private readonly IProgrammerRepository _programmerRepository;

        private int _iteration;
        private readonly double[] _ranks;

        public ProgrammerRankCalculator(IProgrammerRepository programmerRepository)
        {
            _programmerRepository = programmerRepository;
            _ranks = new double[_programmerRepository.GetAll().Count()];
        }

        public double GetRank(string programmerId)
        {
            const int settleLimit = 20;
            const double dampingFactor = 0.85d;

            var programmers = _programmerRepository.GetAll().ToArray();
            var indeces = new Dictionary<string, int>();
            for (var i = 0; i < programmers.Length; i++)
                indeces.Add(programmers[i].GetId(), i);

            while (++_iteration < settleLimit)
                for (var i = 0; i < programmers.Length; i++)
                    _ranks[i] = 1 - dampingFactor + dampingFactor * programmers[i].GetRecommenders(_programmerRepository.GetAll()).Select(p => GetRank(p.GetId()) / RecommendationCount(p)).Sum();

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