using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles{
    /// <summary>
    /// Holds the stats for a Uwudle. (Attack, xp, and type)
    /// </summary>
    public class UwudleStats{
        public float attack { get; set; }
        public float xp { get; set; }
        public UwudleType type { get; set; }
        public int level { get; set; }
        public Damagable Health;

        public UwudleStats(float attack, float xp, UwudleType type, Damagable Health){
            this.attack = attack;
            this.xp = xp;
            this.type = type;
            level = 0;
        }

        public UwudleStats(float attack, float xp, UwudleType type, Damagable Health, int level){
            this.attack = attack;
            this.xp = xp;
            this.type = type;
            this.level = level;
        }

        public void addXp(float amount){
            xp += amount;
        }

        public void resetXp(){
            xp = 0;
        }

        /// <summary>
        /// Levels Uwudle up, increases all stats, regen all health, and resets
        /// xp
        /// </summary>
        public void levelUp(){
            level = (level < 3) ? level + 1 : 3;
            Health.UpdateMaxHp((int)(Health.MaxHp * 1.5));
            Health.Hp = Health.MaxHp;
            xp = 0;
            attack *= 1.5f;
        }
    }

    /// <summary>
    /// Holds the types for an uwudle
    /// (WET, HOT, and PLANT)
    /// </summary>
    public enum UwudleType{
        WET,
        HOT,
        PLANT
    }
}
