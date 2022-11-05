using Game.Scripts.Quests;
using Game.Scripts.Quests.Interfaces;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Journal
{
    public class MiniJournalView : MonoBehaviour
    {
        private IQuest currentLastQuest;
        [SerializeField]
        private Quests.Journal journal;
        [SerializeField]
        private GameObject container;
        [SerializeField]
        private Text description;
        [SerializeField]
        private Text condition;

        private void ChangeStateContainer(bool state)
        {
            this.container.SetActive(state);
        }

        private void HandleActivateQuest(IQuest quest)
        {
            this.currentLastQuest = quest;
            this.UpdateInfoQuest();
            this.ChangeStateContainer(true);
        }

        private void HandleCompleteQuest(IQuest quest)
        {
            this.currentLastQuest = null;
            this.currentLastQuest = this.journal.GetLastQuest();
            if (this.currentLastQuest == null)
            {
                this.ChangeStateContainer(false);
            }
            else
            {
                this.UpdateInfoQuest();
            }
        }

        private void HandleUpdateQuests(IQuest quest)
        {
            this.UpdateInfoQuest();
        }

        private void Start()
        {
            this.journal.OnActivateQuest += HandleActivateQuest;
            this.journal.OnCompleteQuest += HandleCompleteQuest;
            this.journal.OnUpdateQuests += HandleUpdateQuests;
        }

        private void UpdateInfoQuest()
        {
            this.description.text = this.currentLastQuest.descriptionText;
            this.condition.text = this.currentLastQuest.conditionalText;
        }
    }
}