using Cinemachine;
using RPGBatler.NPC.Dialogue.Interfaces;
using System;
using UnityEngine;

namespace RPGBatler.NPC.Dialogue
{
    public class DialogueCamera : MonoBehaviour, IDialogueCamera
    {
        private CinemachineVirtualCamera dialogueCam;
        private CinemachineBrain mainCameraBrain;

        private void Awake()
        {
            this.dialogueCam = base.transform.GetComponentInChildren<CinemachineVirtualCamera>();
            this.mainCameraBrain = Camera.main.GetComponent<CinemachineBrain>();
        }

        public void ChangeStateDialogueCamera(bool state)
        {
            this.dialogueCam.enabled = state;
            this.mainCameraBrain.enabled = state;
        }
    }
}