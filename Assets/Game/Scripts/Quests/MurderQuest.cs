using Game.Scripts.Quests.Interfaces;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game.Scripts.Quests
{
    [Serializable]
    public class MurderQuest : IQuest
    {
        private string originConditionText = "Kill:";
        [SerializeField]
        private int progressOfKilled;

        public MurderQuest()
        {
            this.UpdateTextConditional();
        }

        public bool IsComplete() => 
            this.progressOfKilled == this.amountOfKill;

        public void UpdateProgress()
        {
            this.progressOfKilled++;
            this.UpdateTextConditional();
        }

        private void UpdateTextConditional()
        {
            this.conditionalText = $"{this.originConditionText}{(int) this.progressOfKilled}/{(int) this.amountOfKill}";
        }

        public TypeQuest typeQuest { get; set; }

        public string nameQuest { get; set; }

        public string descriptionText { get; set; }

        public string conditionalText { get; set; }

        public int amountOfKill { get; set; }

        public string typeEnemy { get; set; }
    }
}