using Game.Scripts.UI.Interface;
using RPGBatler.NPC.Dialogue.Interfaces;
using System.Collections;
using Game.Scripts.UI.Dialog.Сonditions;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;
using Debug = UnityEngine.Debug;

namespace Game.Scripts.UI.Dialog
{
    public class DialogView : ADialogView
    {
        [SerializeField]
        private DialogueContainer dialogueContainer;
        private bool isAnimatingText;
        private bool isDialoguePaused;
        private IEnumerator npcTextAnimator;
        private int countCurrentPhrase;
        private IDialogueCamera dialogueCamera;
        private IConditionDialogue[] Conditions = new IConditionDialogue[] { new QuestCondition() };
        public bool doNotNext;

        private void ActivateCameraDialogue(VIDE_Assign assign)
        {
            IDialogueCamera camera;
            if (assign.gameObject.TryGetComponent<IDialogueCamera>(out camera))
            {
                this.dialogueCamera = camera;
                this.dialogueCamera.ChangeStateDialogueCamera(true);
            }
        }

        private void BeginDialog(VIDE_Assign dialogue)
        {
            this.dialogueContainer.InitTextLinks();
            this.dialogueContainer.BzeroText();
            this.SignEvents();
            VD.BeginDialogue(dialogue);
            this.ChangeStateDialogueObject(true);
            this.ActivateCameraDialogue(dialogue);
        }

        private void CallNextNodeInTheDialogue()
        {
            if (this.isAnimatingText)
            {
                this.PrematureFinishTextAnimation();
            }
            else if (!this.isDialoguePaused)
            {
                VD.Next();
            }
        }

        private void ChangeStateDialogueObject(bool state)
        {
            this.dialogueContainer.BzeroText();
            this.dialogueContainer.container.SetActive(state);
        }

        private void Disable()
        {
            if (this.dialogueCamera != null)
            {
                this.dialogueCamera.ChangeStateDialogueCamera(false);
                this.dialogueCamera = null;
            }
            this.UnsignEvents();
            this.ChangeStateDialogueObject(false);
            VD.EndDialogue();
        }

        private void DisplayReplicasOptions(string[] choices)
        {
            if (choices.Length > this.dialogueContainer.replicaObjects.Count)
            {
                Debug.LogError("Too big replicas in this dialogue");
            }
            for (int i = 0; i < choices.Length; i++)
            {
                this.dialogueContainer.replicaObjects[i].SetActive(true);
                this.dialogueContainer.replicaObjects[i].GetComponentInChildren<Text>().text = choices[i];
            }
            this.countCurrentPhrase = choices.Length;
        }

        IEnumerator DrawText(string text, float time)
        {
            isAnimatingText = true;

            string[] words = text.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                if (i != words.Length - 1) word += " ";

                string previousText = dialogueContainer.TextLink[0].text;

                float lastHeight = dialogueContainer.TextLink[0].preferredHeight;
                dialogueContainer.TextLink[0].text += word;
                if (dialogueContainer.TextLink[0].preferredHeight > lastHeight)
                {
                    previousText += System.Environment.NewLine;
                }

                for (int j = 0; j < word.Length; j++)
                {
                    dialogueContainer.TextLink[0].text = previousText + word.Substring(0, j + 1);
                    yield return new WaitForSeconds(time);
                }
            }
            dialogueContainer.TextLink[0].text = text;
            isAnimatingText = false;
        }

        private void EndDialogue(VD.NodeData data)
        {
            this.Disable();
        }

        public override void Interact(VIDE_Assign assign)
        {
            this.doNotNext = this.PreConditions(assign);
            if (!VD.isActive)
            {
                if (this.doNotNext)
                {
                    Debug.Log((int) VD.nodeData.nodeID);
                }
                this.BeginDialog(assign);
            }
            else if (this.doNotNext)
            {
                this.UpdateUI(VD.nodeData);
            }
            else
            {
                if (this.doNotNext)
                {
                    Debug.Log((int) VD.nodeData.nodeID);
                }
                this.CallNextNodeInTheDialogue();
            }
        }

        private void OnDisable()
        {
            this.Disable();
        }

        private bool PreConditions(VIDE_Assign dialogue)
        {
            VD.NodeData nodeData = VD.nodeData;
            if (VD.isActive)
            {
                for (int i = 0; i < this.Conditions.Length; i++)
                {
                    if (this.Conditions[i].ContainsTriggerKey(nodeData))
                    {
                        return this.Conditions[i].ImplementCondition(nodeData);
                    }
                }
            }
            return false;
        }

        private void PrematureFinishTextAnimation()
        {
            base.StopCoroutine(this.npcTextAnimator);
            this.dialogueContainer.TextLink[0].text = VD.nodeData.comments[VD.nodeData.commentIndex];
            this.isAnimatingText = false;
        }

        private void SignEvents()
        {
            VD.OnNodeChange += new VD.NodeChange(this.UpdateUI);
            VD.OnEnd += new VD.NodeChange(this.EndDialogue);
        }

        private void UnsignEvents()
        {
            VD.OnNodeChange -= new VD.NodeChange(this.UpdateUI);
            VD.OnEnd -= new VD.NodeChange(this.EndDialogue);
        }

        private void Update()
        {
            if (VD.isActive)
            {
                VD.NodeData nodeData = VD.nodeData;
                if (!nodeData.pausedAction && nodeData.isPlayer)
                {
                    if (Input.GetKeyDown(KeyCode.S) && (nodeData.commentIndex < (this.countCurrentPhrase - 1)))
                    {
                        nodeData.commentIndex++;
                    }
                    if (Input.GetKeyDown(KeyCode.W) && (nodeData.commentIndex > 0))
                    {
                        nodeData.commentIndex--;
                    }
                    for (int i = 0; i < this.countCurrentPhrase; i++)
                    {
                        this.dialogueContainer.TextLink[i].color = this.dialogueContainer.DefaultReplicaColor;
                        if (i == nodeData.commentIndex)
                        {
                            this.dialogueContainer.TextLink[i].color = this.dialogueContainer.ChoiceReplicaColor;
                        }
                    }
                }
            }
        }

        private void UpdateNPCDialogueData(VD.NodeData data)
        {
            this.dialogueContainer.replicaObjects[0].SetActive(true);
            this.npcTextAnimator = this.DrawText(data.comments[data.commentIndex], 0.02f);
            base.StartCoroutine(this.npcTextAnimator);
        }

        private void UpdatePlayerDialogueData(VD.NodeData data)
        {
            this.DisplayReplicasOptions(data.comments);
        }

        private void UpdateUI(VD.NodeData data)
        {
            for (int i = 0; i < this.dialogueContainer.replicaObjects.Count; i++)
            {
                this.dialogueContainer.replicaObjects[i].SetActive(false);
                this.dialogueContainer.TextLink[i].color = this.dialogueContainer.DefaultReplicaColor;
            }
            this.ChangeStateDialogueObject(true);
            if (data.isPlayer)
            {
                this.UpdatePlayerDialogueData(data);
            }
            else
            {
                this.UpdateNPCDialogueData(data);
            }
            base.sounder.PlayAudioPhrase(data.audios[0]);
        }
    }
}
