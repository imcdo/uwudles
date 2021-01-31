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
        public Material[] _uwudleSkins;
        private Random _rand = new Random();

        public Uwudle BuildRandom()
        {
            int baseId = _rand.Next(_baseUwudles.Length);
            int hatsId = _rand.Next(-1, _hats.Length);
            int skinId = _rand.Next(_uwudleSkins.Length);
            return Build(baseId, hatsId, skinId);
        }

        public Uwudle Build(int baseId, int hatId, int skinId)
        {
            Uwudle uwudle = Instantiate(_baseUwudles[baseId]);
            if (hatId != -1)
                Instantiate(_hats[hatId], uwudle.HatMountPoint);
            uwudle.SetSkin(_uwudleSkins[skinId]);

            return uwudle;
        }
    }
}
