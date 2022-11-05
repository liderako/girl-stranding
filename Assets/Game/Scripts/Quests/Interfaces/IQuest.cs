namespace Game.Scripts.Quests.Interfaces
{
    using System;

    public interface IQuest
    {
        bool IsComplete();
        void UpdateProgress();

        TypeQuest typeQuest { get; set; }

        string descriptionText { get; set; }

        string conditionalText { get; set; }

        string nameQuest { get; set; }
    }
}