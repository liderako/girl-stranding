namespace RPGBatler.NPC
{
    using RPGBatler.Player;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class BattleEnemyGroup : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> npc; //TODO need add enemy paty in incpector
        private BattleState currentState;
        private GameObject target;
        private enum BattleState
        {
            none,
            battle
        }

        private void ChangeBattleState(BattleState newState)
        {
            this.currentState = newState;
            if (this.currentState == BattleState.battle)
            {
                base.StartCoroutine(this.StartNPCInTurn());
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            //#TODO Activate Enemy group for battle with player
            // AController controller;
            // if ((this.currentState != BattleState.battle) && other.TryGetComponent<AController>(out controller))
            // {
            //     this.target = other.gameObject;
            //     this.ChangeBattleState(BattleState.battle);
            // }
        }
        
        private IEnumerator StartNPCInTurn()
        {
            for (int i = 0; i < npc.Count; i++)
            {
                // npc[i].InitBattleState(target); // #TODO activate enemy 
                yield return new WaitForSecondsRealtime(1);
            }
        }
    }
}
