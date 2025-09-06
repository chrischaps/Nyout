using System.Linq;

namespace DefaultNamespace
{
    public static class DiceUtils
    {
        public static bool IsAllTails(this int[] rolls)
        {
            return rolls.All(x => x == 0);
        }
        
        public static Horse[] Occupants(this Node node)
        {
            return node.GetComponentsInChildren<Horse>();
        }
    }
}