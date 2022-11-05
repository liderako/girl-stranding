using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace RPGBatler.NPC
{
    public class RigIKTrigger : MonoBehaviour
    {
        private RigBuilder rigBuilder;

        public void ActivateRigIK(string nameLayer)
        {
            this.ChangeStateRigLayer(nameLayer, true);
        }

        private void Awake()
        {
            this.rigBuilder = base.GetComponent<RigBuilder>();
        }

        private void ChangeStateRigLayer(string nameLayer, bool state)
        {
            int count = this.rigBuilder.layers.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.rigBuilder.layers[i].name.Equals(nameLayer))
                {
                    this.rigBuilder.layers[i].active = state;
                }
            }
        }

        public void DeactivateRigIK(string nameLayer)
        {
            this.ChangeStateRigLayer(nameLayer, false);
        }
    }
}