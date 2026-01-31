using System;
using DefaultNamespace;
using UnityEngine;
using Yarn.Unity;

public class IntroManager : MonoBehaviour
{
	[SerializeField] private FirstPersonController _fpsChar;

	[SerializeField] private DeliveriesManager _delvieriesManager;
	[SerializeField] private DialogueReference _introDialogue;

	private void Awake()
	{
		_fpsChar.enabled = false;
	}

	private void Start()
	{
		var runner = FindAnyObjectByType<DialogueRunner>();
		runner.onDialogueComplete.AddListener(OnDialogueEnded);
		FadingManager.instance.Fade(0, OnComplete:() =>
		{
			runner.StartDialogue(_introDialogue);
		});
	}

	private void OnDialogueEnded()
	{
		_fpsChar.enabled = true;
		_delvieriesManager.StartDeliveries();
	}
}
