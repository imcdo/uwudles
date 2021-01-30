using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace uwudles
{
    class Battle
    {
        public enum BattlePhase { Starting, Battling, Done }

        Uwudle _u1;
        Uwudle _u2;
        private int _delayTime;

        public BattlePhase Phase { get; private set; }

        public Battle(Uwudle u1, Uwudle u2, float turnTime = 1)
        {
            _u1 = u1;
            _u2 = u2;
            _delayTime = (int)(turnTime * 1000);
            Phase = BattlePhase.Starting;
        }

        public async void Start()
        {
            try
            {
                Uwudle winner = await BattleTask();
            } catch (Exception e) {
                Debug.LogException(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="target"></param>
        /// <returns>A winner if one exists</returns>
        private Uwudle AttackCycle(Uwudle attacker, Uwudle target)
        {
            // attacker.AttackAnimation(target.transform);
            // target.Health.Damage(attacker.Damage);
            if (target.Health.Hp == 0)
                return attacker;
            return null;
        }

        private async Task<Uwudle> BattleTask()
        {
            Uwudle winner = null;
            bool u1Attacker = true;

            while (!winner)
            {
                Uwudle attacker = u1Attacker ? _u1 : _u2;
                Uwudle target =  u1Attacker ? _u2 : _u1;
                winner = AttackCycle(attacker, target);

                u1Attacker = !u1Attacker;
                await Task.Delay(_delayTime);
            }
            return winner;
        }
    }
}
