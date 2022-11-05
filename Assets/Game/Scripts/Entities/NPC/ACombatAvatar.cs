using Game.Scripts.Audio;
using Game.Scripts.HealthSystem;
using RPGBatler.Player.Interface;
using UnityEngine;
    
namespace RPGBatler.NPC
{

    [RequireComponent(typeof(Sounder))]
    public abstract class ACombatAvatar : MonoBehaviour, IPunchable, IDamagable
    {
        public virtual string typeUnit { get; set; }

        protected HealthSystem HealthSystem { get; set; }

        public delegate void CombatEvent(string typeEnemy);
        public Sounder sounder;
        [SerializeField]
        protected Weapon weapon;

        public event CombatEvent KillEvent;

        public abstract void Death();
        public bool IsDead() => 
            this.HealthSystem.isDead();

        public void KillEnemy(string typeEnemy)
        {
            if (this.KillEvent != null)
            {
                this.KillEvent(typeEnemy);
            }
        }

        public virtual void Punch()
        {
            ResultAttack result = this.weapon.Attack();
            AudioClip audioClip = this.weapon.GetAudioClip(result);
            if (result.isEnemyDead)
            {
                this.KillEnemy(result.typeEnemy);
            }
            this.sounder.PlayAudioPhrase(audioClip);
        }

        public virtual void ReceiveHit(int damage)
        {
            this.HealthSystem.SubstractHealth(damage);
            if (this.HealthSystem.isDead())
            {
                this.Death();
            }
        }
    }
}