using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;
using Yarn.Unity;

public class LoveInterest : MonoBehaviour
{
    public CharacterScriptableInterests characterInterests;
    public event Action<CrushRequestData> CrushMadeRequest;
    public event Action CrushRequestCompleted; 
    public float Love = 0f;
    public int LoveLevel = 0;
    public List<int> loveTreshold = new() { 10, 30, 60 };
    public bool waitingObjective = true;
    public List<DialogueReference> _testDialogue;
    public List<DialogueReference> _dialogueLoop;
    public DialogueReference wrongDialogue;
    public DialogueReference goodDialogue;
    [SerializeField,ReadOnly]private DeliverableData _currentRequestedItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
       var player= other.GetComponent<Player>();
        if (player!=null)
        {
            other.GetComponent<FirstPersonController>().cameraCanMove = false;
            other.GetComponent<FirstPersonController>().lockCursor = false;
            if (!waitingObjective)
            {
                AnswerManager.instance.currenteLover = this;
                AnswerManager.instance.spawnAnswer(AnswerManager.instance.answerQuantity, this);
            }
            else
            {
               if(player.ObjectManager.heldObject!=null)
                {
                    if (player.ObjectManager.heldObject.Data == _currentRequestedItem)
                    {
                        waitingObjective = false;
                        player.ObjectManager.ForceRemoval();
                        FindAnyObjectByType<DialogueRunner>().StartDialogue(goodDialogue);
                        CrushRequestCompleted?.Invoke();
                        _currentRequestedItem = null;
                    }
                    else
                    {
                        FindAnyObjectByType<DialogueRunner>().StartDialogue(wrongDialogue);
                        player.ObjectManager.ForceRemoval();

                    }
                }
                else
                {
                    FindAnyObjectByType<DialogueRunner>().StartDialogue(_dialogueLoop[LoveLevel]);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!waitingObjective)
            {
                AnswerManager.instance.currenteLover = null;
                AnswerManager.instance.stopMiniGame();
            }
            else
            {
                FindAnyObjectByType<DialogueRunner>().Stop();
            }
            other.GetComponent<FirstPersonController>().cameraCanMove = true;
            other.GetComponent<FirstPersonController>().lockCursor = true;
        }

    }

    public void ForceRequest()
    {
        waitingObjective = true;
        var newRequest = new CrushRequestData();
        newRequest.requestedItem = characterInterests.levelUpObjects.Keys.ToList()[LoveLevel];
        newRequest.timeToDeliver = characterInterests.levelUpObjects[newRequest.requestedItem];
        _currentRequestedItem = newRequest.requestedItem;
        newRequest.requester = this;
        CrushMadeRequest?.Invoke(newRequest);
        DeliveriesManager.instance.SetupCrushDelivery(newRequest.requestedItem);
    }

    public void LevelUp()
    {   
        LoveLevel++;    
        ForceRequest();
    }

    public void LevelDown()
    {
        LoveLevel--;
    }
}
