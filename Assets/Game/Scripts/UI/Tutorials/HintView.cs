using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game.Scripts.UI.Tutorials
{

    public class HintView : MonoBehaviour
    {
        [SerializeField]
        private GameObject hint;
        [SerializeField]
        private KeyCode interactInput = KeyCode.E;

        public void ChangeStateHint(bool state)
        {
            base.StartCoroutine(this.WaitingEnable(state));
            this.hint.SetActive(state);
        }

        private void Update()
        {
            if (Input.GetKeyDown(this.interactInput))
            {
                this.ChangeStateHint(false);
            }
        }
        
        private IEnumerator WaitingEnable(bool state)
        {
            yield return new WaitForSecondsRealtime(1);
            hint.SetActive(state);
        }
        
    }
}
