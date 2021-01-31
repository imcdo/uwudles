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
        
        public UwudleStats(float attack, float xp, UwudleType type){
            this.attack = attack;
            this.xp = xp;
            this.type = type;
        }

        public void addXp(float amount){
            xp += amount;
        }

        public void resetXp(){
            xp = 0;
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
