using Shared.Model;

namespace Shared.Constants
{
    public class RankExperience
    {
        private static int _baseExp = 456;
        public static int GetExperienceFor(ushort level)
        {
            return _baseExp * 2 ^ level;
        }
    }
}