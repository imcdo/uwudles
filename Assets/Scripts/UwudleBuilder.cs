using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace uwudles
{
    [CreateAssetMenu(menuName="uwudle/UwudleBuilder")]
    public class UwudleBuilder : ScriptableObject
    {
        public Uwudle[] _baseUwudles;
        public GameObject[] _hats;
        public Texture[] _uwudleSkins;  

        public Uwudle BuildRandom()
        {
            Random r = new Random();
            int baseId = r.Next(_baseUwudles.Length);
            int hatsId = r.Next(_hats.Length);
            int skinId = r.Next(_uwudleSkins.Length);
            return Build(baseId, hatsId, skinId);
        }

        public Uwudle Build(int baseId, int hatId, int skinId)
        {
            return _baseUwudles[0];
        }
    }
}
