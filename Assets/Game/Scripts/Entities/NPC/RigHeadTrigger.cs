using RPGBatler.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace RPGBatler.NPC
{

    [RequireComponent(typeof(RigBuilder))]
    public class RigHeadTrigger : MonoBehaviour
    {
        private RigBuilder rigBuilder;
        private const string headRigName = "RigHeadSetup";
        private const string HeadTag = "Head";
        [SerializeField]
        private List<MultiAimConstraint> aimObjects;

        private void AssignTarget(Transform transformTarget)
        {
            int count = this.aimObjects.Count;
            for (int i = 0; i < count; i++)
            {
                WeightedTransformArray sourceObjects = this.aimObjects[i].data.sourceObjects;
                sourceObjects.SetTransform(0, transformTarget);
                this.aimObjects[i].data.sourceObjects = sourceObjects;
            }
            this.rigBuilder.Build();
        }

        private void Awake()
        {
            this.rigBuilder = base.GetComponent<RigBuilder>();
        }

        private void OnTriggerEnter(Collider other)
        {
            // AController controller;
            // if (other.TryGetComponent<AController>(out controller))
            // {
            //     this.AssignTarget(this.RecursiveFindChild(controller.transform));
            // }
        }

        private void OnTriggerExit(Collider other)
        {
            // AController controller;
            // if (other.TryGetComponent<AController>(out controller))
            // {
            //     this.UnAssignTarget();
            // }
        }

        public Transform RecursiveFindChild(Transform parent)
        {
            Transform child = null;
            int index = 0;
            while (true)
            {
                if (index < parent.childCount)
                {
                    child = parent.GetChild(index);
                    if (child.tag != "Head")
                    {
                        child = this.RecursiveFindChild(child);
                        if (child == null)
                        {
                            index++;
                            continue;
                        }
                    }
                }
                return child;
            }
        }

        private void UnAssignTarget()
        {
            int count = this.aimObjects.Count;
            for (int i = 0; i < count; i++)
            {
                this.aimObjects[i].data.sourceObjects.RemoveAt(0);
            }
            this.rigBuilder.Build();
        }
    }
}
