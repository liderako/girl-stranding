namespace Game.Scripts.Audio
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class EffectListener : SoundListener
    {
        public PoolAudioEffects PoolAudio;

        public void PlayAudioSource(AudioClip clip)
        {
            AudioSource src = this.PoolAudio.Pool.Get();
            src.clip = clip;
            src.Play();
            base.StartCoroutine(this.ReturnedToPool(src));
        }
        
        private IEnumerator ReturnedToPool(AudioSource src)
        {
            yield return new WaitForSecondsRealtime(src.clip.length);
            PoolAudio.Pool.Release(src);
        }
    }
}
