namespace Game.Scripts.UI.Dialog.Сonditions
{
    using Game.Scripts.Quests;
    using Game.Scripts.UI.Interface;
    using System;
    using UnityEngine;
    using VIDE_Data;

    public class QuestCondition : IConditionDialogue
    {
        private const string YES = "yes";
        private const string NO = "no";
        private const string TriggerKey = "questKey";
        private string[] subKey;

        public QuestCondition()
        {
            string[] textArray1 = new string[] { "yes", "no" };
            this.subKey = textArray1;
        }

        public bool ContainsTriggerKey(VD.NodeData data) => 
            data.extraVars.ContainsKey("questKey") && this.ValidationDialogue(data);

        public bool ImplementCondition(VD.NodeData data)
        {
            if (Journal.IsQuestComplete((string) ((string) data.extraVars["questKey"])))
            {
                VD.SetNode((int) ((int) data.extraVars["yes"]));
                return true;
            }
            VD.SetNode((int) ((int) data.extraVars["no"]));
            return true;
        }

        private bool ValidationDialogue(VD.NodeData data)
        {
            int num = 0;
            for (int i = 0; i < this.subKey.Length; i++)
            {
                if (data.extraVars.ContainsKey(this.subKey[i]))
                {
                    num++;
                }
            }
            if (num == this.subKey.Length)
            {
                return true;
            }
            Debug.Log($"Dialogue node ${(int) data.nodeID} is broken");
            return false;
        }
    }
}