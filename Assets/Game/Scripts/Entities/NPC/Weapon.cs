using System.Collections.Generic;
using UnityEngine;

namespace RPGBatler.NPC
{
    public struct ResultAttack
    {
        public bool isHit;
        public string typeEnemy;
        public bool isEnemyDead;
    }
    
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField]
        protected int Damage;
        [SerializeField]
        protected GameObject weaponGameObject;
        [SerializeField]
        protected List<AudioClip> HitsAudioClips;
        [SerializeField]
        protected List<AudioClip> MissAudioClips;

        public abstract ResultAttack Attack();
        public abstract AudioClip GetAudioClip(ResultAttack result);
    }
}