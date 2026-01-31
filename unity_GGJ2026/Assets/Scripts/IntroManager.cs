using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class IntroManager : MonoBehaviour
{
	[SerializeField] private FirstPersonController _fpsChar;
	public LoveInterest firstLover;

	[SerializeField] private DeliveriesManager _delvieriesManager;
	[SerializeField] private DialogueReference _introDialogue;
	[SerializeField] private DialogueReference _firstEncounter;

	private void Awake()
	{
		_fpsChar.enabled = false;
	}
	private void Start()
	{
		var runner = FindAnyObjectByType<DialogueRunner>();
		runner.StartDialogue(_introDialogue);
		runner.onDialogueComplete.AddListener(OnDialogueEnded);
		_delvieriesManager.FirstDelivery += StartFirstEncounter;
		
	}

	public void StartFirstEncounter()
	{
        var runner = FindAnyObjectByType<DialogueRunner>();
        runner.onDialogueComplete.RemoveListener(OnDialogueEnded);
        runner.StartDialogue(_firstEncounter);
        _delvieriesManager.stopAll();
        _fpsChar.enabled = false;
        _fpsChar.GetComponent<Transform>().LookAt(firstLover.GetComponent<Transform>());
        firstLover.CrushRequestCompleted += FirstLover_CrushRequestCompleted;
        runner.onDialogueComplete.AddListener(OnFirstEncounterEnded);

    }

    private void FirstLover_CrushRequestCompleted()
    {
        _delvieriesManager.StartDeliveries();
        firstLover.CrushRequestCompleted -= FirstLover_CrushRequestCompleted;
    }


    private void OnDialogueEnded()
	{
		_fpsChar.enabled = true;
		_delvieriesManager.StartDeliveries();
		FadingManager.instance.Fade(0);
	}
    private void OnFirstEncounterEnded()
    {
        _fpsChar.enabled = true;

        //_delvieriesManager.StartDeliveries();
        //FadingManager.instance.Fade(0);
    }
}
