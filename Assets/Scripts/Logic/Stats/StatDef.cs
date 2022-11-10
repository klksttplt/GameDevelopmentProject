using UnityEngine;

namespace Logic.Stats
{
    [CreateAssetMenu(fileName = "StatDef.asset", menuName = "Stats/StatDef")]
    public class StatDef : ScriptableObject
    {
        // Fields: Editor
        
        [SerializeField]
        private StatValueType valueType;

        [SerializeField]
        private string statName;
        
        [SerializeField]
        private Sprite spriteIcon;
        
        // Public API

        public virtual int ID => name.GetHashCode();

        public string Name => statName;
        
        public Sprite SpriteIcon => spriteIcon;
        
        public StatValueType ValueType => valueType;
        
        public bool Equals(StatDef otherStatDef)
        {
            if (ReferenceEquals(null, otherStatDef)) return false;
            return ID == otherStatDef.ID;
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            return other is StatDef otherStatDef && Equals(otherStatDef);
        }
        
        public static bool operator ==(StatDef left, StatDef right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(StatDef left, StatDef right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}