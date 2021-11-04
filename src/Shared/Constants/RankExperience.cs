using System;
using Shared.Model;

namespace Shared.Constants
{
    public class RankExperience
    {
        private static int _baseExp = 547;
        private static float _factor = 2.325f;
        public static int GetExperienceFor(ushort level)
        {
            return (int) (_baseExp * Math.Pow(level, _factor));
        }
    }
}