using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace uwudles
{
    [CreateAssetMenu(menuName="uwudle/UwudleBuilder")]
    public class UwudleBuilder : ScriptableObject
    {
        public Uwudle Twodle;
        public Uwudle Fourble;
        public Uwudle Sixple;

        public GameObject[] _hats;
        public Material[] PlantTwodleSkins;
        public Material[] WetTwodleSkins;
        public Material[] HotTwodleSkins;

        public Material[] PlantFourbleSkins;
        public Material[] WetFourbleSkins;
        public Material[] HotFourbleSkins;
        
        public Material[] PlantSixpleSkins;
        public Material[] WetSixpleSkins;
        public Material[] HotSixpleSkins;


        public AudioClip[] attackSounds;
        private Random _rand = new Random();

        public Uwudle BuildRandom()
        {
            int baseId = _rand.Next(3);
            int hatsId = _rand.Next(-1, _hats.Length);
            int elementId = _rand.Next(3);
            return Build(baseId, hatsId, elementId);
        }

        public Uwudle Build(int baseId, int hatId, int elementId)
        {
            Uwudle uwudle;
            switch (baseId)
            {
                case 0:
                    uwudle = Instantiate(Twodle);
                    break;
                case 1:
                    uwudle = Instantiate(Fourble);
                    break;
                case 2:
                    uwudle = Instantiate(Sixple);
                    break;
                default:
                    return null;
            }

            if (hatId != -1)
            {
                var hat = Instantiate(_hats[hatId], uwudle.HatMountPoint);
                hat.transform.localScale = new Vector3(
                        hat.transform.localScale.x / uwudle.HatMountPoint.transform.lossyScale.x,
                        hat.transform.localScale.y / uwudle.HatMountPoint.transform.lossyScale.y,
                        hat.transform.localScale.z / uwudle.HatMountPoint.transform.lossyScale.z
                    );
                hat.transform.localPosition = Vector3.zero;
                hat.transform.localRotation = Quaternion.Euler(0, 90, 0);
            }

            Material[][] ma = new Material[][] { PlantTwodleSkins, WetTwodleSkins, HotTwodleSkins,
                                                 PlantFourbleSkins, WetFourbleSkins, HotFourbleSkins,
                                                 PlantSixpleSkins, WetSixpleSkins, HotSixpleSkins };

            // TODO: Set element to 
            uwudle.Element = (Element)elementId;
            Material[] mat = ma[3 * baseId + elementId];
            uwudle.SetSkin(mat[_rand.Next(mat.Length)]);
            AudioSource source = uwudle.gameObject.AddComponent<AudioSource>();
            source.clip = attackSounds[UnityEngine.Random.Range(0, attackSounds.Length)];
            source.spatialBlend = 1f;
            source.volume = 1f;
            source.pitch = 1f;
            return uwudle;
        }
    }
}
