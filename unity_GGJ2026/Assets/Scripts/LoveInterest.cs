using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LoveInterest : MonoBehaviour
{
    public CharacterScriptableInterests characterInterests;
    public float Love = 0f;
    public int LoveLevel = 0;
    public List<int> loveTreshold = new List<int>() { 10, 30, 60, 100 };
    public bool waitingObjective = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!waitingObjective)
            {


                AnswerManager.instance.currenteLover = this;
                other.GetComponent<FirstPersonController>().cameraCanMove = false;
                other.GetComponent<FirstPersonController>().lockCursor = false;
                AnswerManager.instance.spawnAnswer(AnswerManager.instance.answerQuantity, this);
            }
            else
            {

                //check object
                Debug.Log("Mi serve un oggetto");
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
                other.GetComponent<FirstPersonController>().cameraCanMove = true;
                other.GetComponent<FirstPersonController>().lockCursor = true;
                AnswerManager.instance.stopMiniGame();
            }
            else
            {
                //check object  
            }
        }

    }
}
