using Game.Scripts.Audio;
using Game.Scripts.UI.Interface;
using System;
using UnityEngine;

namespace Game.Scripts.UI
{
    [RequireComponent(typeof(Sounder))]
    public abstract class ADialogView : MonoBehaviour, IInteractableDialogView
    {
        [HideInInspector]
        public Sounder sounder;

        protected ADialogView()
        {
        }

        protected virtual void Awake()
        {
            this.sounder = base.GetComponent<Sounder>();
        }

        public abstract void Interact(VIDE_Assign assign);
    }
}