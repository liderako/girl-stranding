using Game.Scripts.Quests.Interfaces;
using System.Collections.Generic;
using RPGBatler.NPC;
using UnityEngine;

namespace Game.Scripts.Quests
{
    public class MurderQuestListener : MonoBehaviour
    {
        private List<MurderQuest> listenersQuests = new List<MurderQuest>();
        [SerializeField]
        private Journal journal;
        [SerializeField]
        private ACombatAvatar ownerAvatar;

        public void CheckQuest(string type)
        {
            for (int i = 0; i < this.listenersQuests.Count; i++)
            {
                if (this.listenersQuests[i].typeEnemy == type)
                {
                    this.journal.UpdateQuest(this.listenersQuests[i]);
                    return;
                }
            }
        }

        private void HandleDeleteQuest(IQuest quest)
        {
            if (quest.typeQuest == TypeQuest.killEnemies)
            {
                MurderQuest item = quest as MurderQuest;
                this.listenersQuests.Remove(item);
            }
        }

        private void HandleOnActivateQuest(IQuest quest)
        {
            if (quest.typeQuest == TypeQuest.killEnemies)
            {
                this.listenersQuests.Add(quest as MurderQuest);
            }
        }

        private void Start()
        {
            this.journal.OnActivateQuest += new Journal.QuestEvent(this.HandleOnActivateQuest);
            this.journal.OnCompleteQuest += new Journal.QuestEvent(this.HandleDeleteQuest);
            // this.ownerAvatar.KillEvent += new ACombatAvatar.CombatEvent(this.CheckQuest); #TODO need to sign on kill event
        }
    }
}