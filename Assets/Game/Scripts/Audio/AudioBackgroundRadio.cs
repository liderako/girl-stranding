using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Audio
{
    public class AudioBackgroundRadio : MonoBehaviour
    {
        [SerializeField]
        private List<AudioClip> lifeBackgrounds;
        [SerializeField]
        private List<AudioClip> battleBackgrounds;
        [SerializeField]
        private AudioSource radio;
        private StateRadio currentState;
        private IEnumerator nextSongCoroutine;
        
        public enum StateRadio
        {
            life,
            battle
        }

        private void Awake()
        {
            this.PlayRadio(StateRadio.life);
        }
        
        private IEnumerator NextSong(float len)
        {
            yield return new WaitForSecondsRealtime(len);
            PlayRadio(StateRadio.life);
        }

        public void PlayRadio(StateRadio state)
        {
            if (this.nextSongCoroutine != null)
            {
                base.StopCoroutine(this.nextSongCoroutine);
            }
            this.radio.Stop();
            if (state == StateRadio.life)
            {
                this.radio.clip = this.lifeBackgrounds[Random.Range(0, this.lifeBackgrounds.Count)];
            }
            else if (state == StateRadio.battle)
            {
                this.radio.clip = this.battleBackgrounds[Random.Range(0, this.battleBackgrounds.Count)];
            }
            this.nextSongCoroutine = this.NextSong(this.radio.clip.length);
            base.StartCoroutine(this.nextSongCoroutine);
            this.radio.Play();
            this.currentState = state;
        }
    }
}
