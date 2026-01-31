using DefaultNamespace;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    public static AnswerManager instance;

    public GameObject rightAnswer;
    public GameObject canvas;
    public int consecutiveRightAnswers = 0;
    public GameObject answerPrefab;
    public LoveInterest currenteLover;
    public int answerQuantity = 4;
    public Coroutine answerCoroutine;
    public List<GameObject> answerList = new List<GameObject>();
    public CharacterScriptableInterests characterInterestsTest;
    public float delayBetweenAnswers = 0.5f;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        canvas = gameObject;

        //spawnAnswer(4,characterInterestsTest);
    }
    public IEnumerator ClearAnswers()
    {
        if (answerCoroutine != null)
        {
            StopCoroutine(answerCoroutine);
        }
        foreach (var answer in answerList)
        {
            Destroy(answer);
        }
        answerList.Clear();
        consecutiveRightAnswers++;
        yield return new WaitForSeconds(delayBetweenAnswers);
        spawnAnswer(answerQuantity,currenteLover);
    }
    public void stopMiniGame()
    {
        foreach (var answer in answerList)
        {
            Destroy(answer);
        }
        answerList.Clear();
        consecutiveRightAnswers = 0;
        if (answerCoroutine != null)
        {
            StopCoroutine(answerCoroutine);
        }
    }
    public void OnClickRightAnswer(LoveInterest lover)
    {
        lover.Love += 10;
        Debug.Log("Correct Answer Clicked!");
        answerCoroutine= StartCoroutine( ClearAnswers());
    }
    public void OnClickWarmAnswer()
    {
        Debug.Log("meh");
        answerCoroutine= StartCoroutine(ClearAnswers());

    }
    public void OnClickWrongAnswer(LoveInterest lover)
    {
        lover.Love -= 10;
        Debug.Log("Wrong Answer Clicked!");
        answerCoroutine= StartCoroutine(ClearAnswers());

    }
    public void spawnAnswer(int quantity,LoveInterest lover)
    {
       // var r=Random.Range(0, quantity);

        for (int i = 0; i < quantity; i++)
        {
            var r = Random.Range(0, lover.characterInterests.Interests.Count);
            CharacterInterest interest = lover.characterInterests.Interests[r];
            var answer= Instantiate(answerPrefab, new Vector3(Random.Range(-800,800), Random.Range(-300,304), 0), Quaternion.identity,gameObject.GetComponent<Transform>());
            var movement = answer.GetComponent < AnswerMovement>();
            movement.RandomValues(consecutiveRightAnswers);
            if (interest.Favorite)
            {
                answer.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>OnClickRightAnswer(lover));
            }
            else if (interest.Hate)
            {
                answer.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnClickWrongAnswer(lover));
            }
            else
            {
                answer.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClickWarmAnswer);
            }
            answer.GetComponentInChildren<TextMeshProUGUI>().text = interest.topic;
            answerList.Add(answer);
        }
    }
}
