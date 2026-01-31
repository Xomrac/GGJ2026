using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrushTimer : MonoBehaviour
{
	[SerializeField] private GameObject _uiRoot;
	[SerializeField] private TextMeshProUGUI _timerText;
	[SerializeField] private Image _iconImage;
	private float _remainingTime = 300f;
	private LoveInterest _trackedCrush;

	private void Start()
	{
		_uiRoot.SetActive(false);
	}

	public void StartTimer(CrushRequestData data)
	{
		_uiRoot.SetActive(true);
		_remainingTime = data.timeToDeliver;
		_iconImage.sprite = data.requestedItem.Icon;
		_timerText.text =TimeSpan.FromSeconds(_remainingTime).ToString(@"mm\:ss");
		StartCoroutine(RunClock());
	}

	public void InterruptTimer()
	{
		StopAllCoroutines();
		_uiRoot.SetActive(false);
	}

	private IEnumerator RunClock()
	{
		while (_remainingTime > 0)
		{
			_remainingTime -= Time.deltaTime;
			_timerText.text = TimeSpan.FromSeconds(_remainingTime).ToString(@"mm\:ss");
			yield return null;
		}
		_trackedCrush.LevelDown();
		_uiRoot.SetActive(false);
	}
}