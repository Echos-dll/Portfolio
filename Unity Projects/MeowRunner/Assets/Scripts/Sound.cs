using UnityEngine;

namespace Utility
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip AudioClip;
        [Range(0, 1f)] public float volume = 1;
        [Range(0f, 3f)] public float pitch = 1;

        public bool loop;

        [HideInInspector] public AudioSource source;
    }
}