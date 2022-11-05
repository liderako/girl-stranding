using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGBatler.NPC
{
    public class RagdollsAvatar : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> bones;
        [SerializeField]
        private Collider mainCollider;
        [SerializeField]
        private Rigidbody mainRigidBody;

        public void ActivateRagDoll()
        {
            this.ChangeStateAnimator(false);
            this.ChangeStateMainCollider(false);
            this.ChangeStateMainRigidBody(false);
            for (int i = 0; i < this.bones.Count; i++)
            {
                this.bones[i].GetComponent<Rigidbody>().isKinematic = false;
                this.bones[i].GetComponent<Collider>().enabled = true;
            }
        }

        private void ChangeStateAnimator(bool state)
        {
            Animator animator;
            if (base.gameObject.TryGetComponent<Animator>(out animator))
            {
                animator.enabled = state;
            }
        }

        private void ChangeStateMainCollider(bool state)
        {
            if (this.mainCollider != null)
            {
                this.mainCollider.enabled = state;
            }
        }

        private void ChangeStateMainRigidBody(bool state)
        {
            if (this.mainRigidBody != null)
            {
                this.mainRigidBody.isKinematic = false;
            }
        }

        public void Freeze()
        {
            this.ChangeStateMainCollider(true);
            this.ChangeStateAnimator(true);
            this.ChangeStateMainRigidBody(true);
            for (int i = 0; i < this.bones.Count; i++)
            {
                this.bones[i].GetComponent<Rigidbody>().isKinematic = true;
                this.bones[i].GetComponent<Collider>().enabled = false;
            }
        }
    }
}