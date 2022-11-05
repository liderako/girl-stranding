namespace Game.Scripts.Quests
{
    using Game.Scripts.Quests.Interfaces;
    using System.Collections.Generic;
    using UnityEngine;

    public class Journal : MonoBehaviour
    {
        private static List<IQuest> activeQuestsList = new List<IQuest>();
        private static List<IQuest> completedQuestsList = new List<IQuest>();

        public event QuestEvent OnActivateQuest;

        public event QuestEvent OnCompleteQuest;

        public event QuestEvent OnUpdateQuests;

        public void AddQuest(string idQuest)
        {
            IQuest item = this.loadQuest(idQuest);
            activeQuestsList.Add(item);
            if (this.OnActivateQuest != null)
            {
                this.OnActivateQuest(item);
            }
        }

        private void Awake()
        {
            activeQuestsList = new List<IQuest>();
            completedQuestsList = new List<IQuest>();
        }

        public void CompleteQuest(IQuest quest)
        {
            activeQuestsList.Remove(quest);
            completedQuestsList.Add(quest);
            if (this.OnCompleteQuest != null)
            {
                this.OnCompleteQuest(quest);
            }
        }

        public IQuest GetLastQuest() => 
            (activeQuestsList.Count <= 0) ? null : activeQuestsList[activeQuestsList.Count - 1];

        public static bool IsQuestComplete(string nameQuest)
        {
            for (int i = 0; i < completedQuestsList.Count; i++)
            {
                if (completedQuestsList[i].nameQuest == nameQuest)
                {
                    return true;
                }
            }
            return false;
        }

        public IQuest loadQuest(string idQuest)
        {
            MurderQuest quest1 = new MurderQuest()
            {
                nameQuest = "killBandits",
                descriptionText = "You need to kill 3 bandits",
                amountOfKill = 3,
                typeEnemy = "StreetBandit",
                typeQuest = TypeQuest.killEnemies
            };
            return quest1;
        }

        public void UpdateQuest(IQuest quest)
        {
            int index = activeQuestsList.IndexOf(quest);
            activeQuestsList[index].UpdateProgress();
            if (activeQuestsList[index].IsComplete())
            {
                this.CompleteQuest(quest);
            }
            else if (this.OnUpdateQuests != null)
            {
                this.OnUpdateQuests(quest);
            }
        }

        public delegate void QuestEvent(IQuest quest);
    }
}
