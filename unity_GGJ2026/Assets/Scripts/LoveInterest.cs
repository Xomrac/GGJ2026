using UnityEngine;

public class LoveInterest : MonoBehaviour
{
    public CharacterScriptableInterests characterInterests;
    public float Love = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AnswerManager.instance.currenteLover = this;
            other.GetComponent<FirstPersonController>().cameraCanMove = false;
            other.GetComponent<FirstPersonController>().lockCursor = false;
            AnswerManager.instance.spawnAnswer(AnswerManager.instance.answerQuantity, this);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AnswerManager.instance.currenteLover = null;
            other.GetComponent<FirstPersonController>().cameraCanMove = true;
            other.GetComponent<FirstPersonController>().lockCursor = true;
            AnswerManager.instance.stopMiniGame();

        }
    }
    
}
