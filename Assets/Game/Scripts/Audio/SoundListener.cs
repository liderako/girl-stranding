using System;
using UnityEngine;

namespace Game.Scripts.Audio
{

    [RequireComponent(typeof(AudioSource))]
    public class SoundListener : MonoBehaviour
    {
        protected AudioSource source;
        [SerializeField]
        protected Sounder sounder;

        private void OnDisable()
        {
            this.sounder.AudioPlay -= new Sounder.AudioEvent(this.PlayAudioSource);
        }

        public virtual void PlayAudioSource(AudioClip clip)
        {
            this.source.Stop();
            this.source.clip = clip;
            this.source.Play();
        }

        private void Start()
        {
            this.source = base.GetComponent<AudioSource>();
            this.sounder.AudioPlay += new Sounder.AudioEvent(this.PlayAudioSource);
        }
    }
}