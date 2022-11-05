using RPGBatler.NPC.States.Interfaces;
using RPGBatler.Player;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RPGBatler.NPC.States
{

    public class BattleState : IState
    {
        private const string BATTLE = "Battle";
        private UnityEngine.Animator animator;
        private const float MIN_DISTANCE = 1f;

        private void Attack()
        {
            this.owner.Punch();
        }

        private bool TryToAttack() => 
            Vector3.Distance(this.owner.transform.position, this.target.transform.position) <= 1f;

        public void UpdateState()
        {
            if (this.TryToAttack())
            {
                this.Attack();
            }
        }

        public GameObject target { get; set; }

        public UnityEngine.Animator Animator
        {
            get => 
                this.animator;
            set
            {
                this.animator = value;
                this.animator.SetBool("Battle", true);
            }
        }

        public ACombatAvatar owner { get; set; }
    }
}