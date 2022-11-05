using RPGBatler.NPC.States.Interfaces;
using RPGBatler.Player;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

namespace RPGBatler.NPC.States
{

    public class MoveState : IState
    {
        private const string SPEED_MOVEMENT = "SpeedMovement";
        private const float MIN_DISTANCE = 1f;
        private float speedMovement;
        private UnityEngine.Animator animator;

        private void CalcSpeedMove(bool move)
        {
            int num = move ? 1 : 0;
            this.speedMovement = Mathf.Lerp(this.speedMovement, (float) num, 0.1f);
        }

        private bool IsHaveToMove() => 
            Vector3.Distance(this.owner.transform.position, this.target.transform.position) >= 1f;

        private void Move(bool state)
        {
            if (!state)
            {
                this.Agent.isStopped = true;
            }
            else
            {
                this.Agent.isStopped = false;
                this.Agent.SetDestination(this.target.transform.position);
            }
            this.CalcSpeedMove(state);
            this.animator.SetFloat("SpeedMovement", this.speedMovement);
        }

        private void RotateAtTarget()
        {
            Quaternion rotation = this.owner.transform.rotation;
            this.owner.transform.LookAt(this.target.transform.position);
            Quaternion b = this.owner.transform.rotation;
            this.owner.transform.rotation = rotation;
            this.owner.transform.rotation = Quaternion.Lerp(this.owner.transform.rotation, b, 10f * Time.deltaTime);
        }

        public void UpdateState()
        {
            if (this.IsHaveToMove())
            {
                this.Move(true);
            }
            else
            {
                this.Move(false);
                this.RotateAtTarget();
            }
        }

        public NavMeshAgent Agent { get; set; }

        public ACombatAvatar owner { get; set; }

        public GameObject target { get; set; }

        public UnityEngine.Animator Animator
        {
            get => 
                this.animator;
            set => 
                this.animator = value;
        }
    }
}
