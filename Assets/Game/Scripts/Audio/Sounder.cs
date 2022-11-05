using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace Game.Scripts.Audio
{
    [Serializable]
    public class Sounder : MonoBehaviour
    {
        public event AudioEvent AudioPlay;

        public void PlayAudioPhrase(AudioClip audioClips)
        {
            if (this.AudioPlay != null)
            {
                this.AudioPlay(audioClips);
            }
        }

        public delegate void AudioEvent(AudioClip clip);
    }
}