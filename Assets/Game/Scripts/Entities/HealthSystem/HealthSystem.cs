using System;
using UnityEngine;

namespace Game.Scripts.HealthSystem
{
    public class HealthSystem : MonoBehaviour, IHealthSystem
    {
        [Header("Health Settings"), SerializeField]
        protected int maxHp;
        [SerializeField]
        protected int currentHp;

        public virtual void AddHealth(int amount)
        {
            int num;
            this.currentHp = num = this.currentHp + amount;
            this.currentHp = Mathf.Clamp(num, 0, this.maxHp);
        }

        public void Init(int maxHp)
        {
            this.maxHp = maxHp;
            this.currentHp = this.maxHp;
        }

        public bool isDead() => 
            this.currentHp == 0;

        public virtual void SubstractHealth(int damage)
        {
            int num;
            this.currentHp = num = this.currentHp - damage;
            this.currentHp = Mathf.Clamp(num, 0, this.maxHp);
        }
    }
}