using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct DialogueContainer
    {
        public GameObject container;
        public List<GameObject> replicaObjects;
        public Color DefaultReplicaColor;
        public Color ChoiceReplicaColor;
        public List<Text> TextLink { readonly get; set; }
        public void InitTextLinks()
        {
            this.TextLink = new List<Text>();
            for (int i = 0; i < this.replicaObjects.Count; i++)
            {
                this.TextLink.Add(this.replicaObjects[i].GetComponentInChildren<Text>());
            }
        }

        public void BzeroText()
        {
            if (this.TextLink != null)
            {
                for (int i = 0; i < this.TextLink.Count; i++)
                {
                    this.TextLink[i].text = "";
                }
            }
        }
    }
}