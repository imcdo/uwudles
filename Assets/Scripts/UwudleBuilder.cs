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

        public Uwudle BuildRandom()
        {
            Random r = new Random();
            int baseId = r.Next(_baseUwudles.Length);
            int hatsId = r.Next(-1, _hats.Length);
            int skinId = r.Next(_uwudleSkins.Length);
            return Build(baseId, hatsId, skinId);
        }

        public Uwudle Build(int baseId, int hatId, int skinId)
        {
            Uwudle uwudle = Instantiate(_baseUwudles[baseId]);
            if (hatId != -1)
                Instantiate(_hats[hatId], uwudle.HatMountPoint);
            uwudle.SetSkin(_uwudleSkins[skinId]);

            return _baseUwudles[0];
        }
    }
}
