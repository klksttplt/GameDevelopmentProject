using Logic.Stats;

namespace Logic.Common
{
    public abstract class ContextComponent : ContextInterface
    {
        public StatValueProvider? GetStatValue(StatDef stat)
        {
            if (HasStats)
            {
                if (!Stats.StatsMap.ContainsKey(stat)) 
                    return null;
                
                StatValueProvider statValueProvider;
                Stats.StatsMap.TryGetValue(stat, out statValueProvider);
                return statValueProvider;
            }
            return null;
        }
    }
}