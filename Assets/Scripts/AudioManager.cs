using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace uwudles{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;
        public static AudioManager Instance => _instance ? _instance : _instance = FindObjectOfType<AudioManager>();
        [SerializeField] private List<Sound> sounds = new List<Sound>();
        private Dictionary<string, AudioSource> soundDict = new Dictionary<string, AudioSource>();
        private void Awake() {
            foreach(Sound s in sounds){
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.clip = s.clip;
                source.volume = s.volume;
                source.pitch = s.pitch;
                source.loop = s.loop;
                if(s.playOnAwake){
                    source.Play();
                }
            }
        }

        public void playSound(string soundName){
            AudioSource source = soundDict[soundName];
            source.Play();
        }

        public void playRandPitch(string soundName, float minPitch, float maxPitch){
            AudioSource source = soundDict[soundName];
            float oldPitch = source.pitch;
            source.pitch = Random.Range(Mathf.Clamp(minPitch, .1f, 3f), Mathf.Clamp(maxPitch, .1f, 3f));
            source.Play();
            source.pitch = oldPitch;
        }

    }
}
