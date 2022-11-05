using System;
using UnityEngine;

namespace RPGBatler.NPC.Dialogue
{
    public class Memory : MonoBehaviour
    {
        private VIDE_Assign dialogSystem;

        private void Awake()
        {
            this.dialogSystem = base.GetComponent<VIDE_Assign>();
            this.dialogSystem.overrideStartNode = this.LoadMemory();
        }

        private int LoadMemory() => 
            0;

        public void SetNewStartNode(int newStartNode)
        {
            this.dialogSystem.overrideStartNode = newStartNode;
        }
    }
}