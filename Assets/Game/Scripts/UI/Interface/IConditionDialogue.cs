using System;
using VIDE_Data;

namespace Game.Scripts.UI.Interface
{

    public interface IConditionDialogue
    {
        bool ContainsTriggerKey(VD.NodeData data);
        bool ImplementCondition(VD.NodeData data);
    }
}