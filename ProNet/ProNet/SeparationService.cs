﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class SeparationService
    {
        private readonly List<IProgrammer> _programmers;

        public SeparationService(IGetProgrammers getProgrammers)
        {
            _programmers = getProgrammers.GetAll().ToList();
        }

        public int GetDegreesBetween(string programmerAId, string programmerBId)
        {
            if (programmerAId == programmerBId)
                return -1;

            var programmerA = GetProgrammer(programmerAId);
            var programmerB = GetProgrammer(programmerBId);

            if (programmerA.GetRecommendations().Contains(programmerBId) || programmerB.GetRecommendations().Contains(programmerAId))
                return 0;

            if (programmerA.GetRecommendations().Intersect(programmerB.GetRecommendations()).Any())
                return 1;

            if (programmerA.GetRecommenders(_programmers).Intersect(programmerB.GetRecommenders(_programmers)).Any())
                return 1;

            var aSecondDegree =
                programmerA
                    .GetRecommendations()
                    .Select(id => _programmers.Single(p => p.GetId() == id))
                    .SelectMany(recommendation => recommendation.GetRecommendations())
                    .Distinct();

            var bSecondDegree =
                programmerB
                    .GetRecommendations()
                    .Select(id => _programmers.Single(p => p.GetId() == id))
                    .SelectMany(recommendation => recommendation.GetRecommendations())
                    .Distinct();

            if (aSecondDegree.Contains(programmerBId) || bSecondDegree.Contains(programmerAId))
                return 1;

            return -1;
        }

        private IProgrammer GetProgrammer(string programmerId)
        {
            var programmer = _programmers.SingleOrDefault(p => p.GetId() == programmerId);
            if (programmer == null)
                throw new ArgumentException($"Programmer {programmerId} was not found");
            return programmer;
        }
    }
}