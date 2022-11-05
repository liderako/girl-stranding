using System;
using UnityEngine;

namespace RPGBatler.NPC.Dialogue
{
    public class NPCAnimator : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            this.animator = base.GetComponent<Animator>();
        }

        public void EndAnimation(string animationBoolName)
        {
            this.animator.SetBool(animationBoolName, false);
        }

        public void SetTriggerAnimation(string animationTriggerName)
        {
            this.animator.SetTrigger(animationTriggerName);
        }

        public void StartAnimation(string animationBoolName)
        {
            this.animator.SetBool(animationBoolName, true);
        }
    }
}