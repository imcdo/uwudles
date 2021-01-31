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
        [SerializeField] private float maxTimeBWRandSounds;
        [SerializeField] private float minTimeBWRandSounds;
        [SerializeField] private bool keepPlayingRandom = true;
        private float timeBetweenSounds;
        private Dictionary<string, AudioSource> soundDict = new Dictionary<string, AudioSource>();
        private List<AudioSource> randomPlaySounds = new List<AudioSource>();
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
                if(s.playRandomly){
                    randomPlaySounds.Add(source);
                }
            }
            StartCoroutine(playRandom());
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

        public void stopPlaying(string soundName){
            AudioSource source = soundDict[soundName];
            source.Stop();
        }

        IEnumerator playRandom(){
            while(keepPlayingRandom){
                float randomTime = Random.Range(minTimeBWRandSounds, maxTimeBWRandSounds);
                int randomIndex = Random.Range(0, randomPlaySounds.Count);
                randomPlaySounds[randomIndex].Play();
                yield return new WaitForSeconds(randomTime);
            }
            yield return null;
        }

    }
}
