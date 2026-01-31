using System;
using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PagerManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _pagerText;
	[SerializeField] private TextMeshProUGUI _timeText;
	[SerializeField] private RectTransform _pagerTransform;
	[SerializeField] private float _timeBeforeHide = 2f;
	[SerializeField] private float _animationsDuration = 0.5f;

	

	

	
	
	private DeliveriesManager _deliveriesManager;
	

	private void Awake()
	{
		_deliveriesManager = FindAnyObjectByType<DeliveriesManager>();
		_deliveriesManager.NewDeliveryCreated += OnNewDeliveryCreated;
		_deliveriesManager.DeliveryTimeUpdated += OnDeliveryTimeUpdated;
		_deliveriesManager.DeliveryFailed += OnDeliveryFailed;
		_deliveriesManager.DeliveryCompleted += OnDeliveryCompleted;
	}

	private void Start()
	{
		_pagerTransform.anchoredPosition = new Vector2(0, 0);
	}

	private void OnDeliveryCompleted(DeliveryData _)
	{
		_pagerText.text = "OK!";
		_timeText.text = "--:--";
		StartCoroutine(WaitAndSetPosY(0));
	}

	private void OnDeliveryFailed(DeliveryData _)
	{
		_pagerText.text = "KO!";
		_timeText.text = "--:--";
		StartCoroutine(WaitAndSetPosY(0));
	}

	private IEnumerator WaitAndSetPosY(float posY)
	{
		yield return new WaitForSeconds(_timeBeforeHide);
		_pagerTransform.DOAnchorPosY(posY, _animationsDuration).SetEase(Ease.InOutBack);
	}

	private void OnDeliveryTimeUpdated(float remainingTime)
	{
		_timeText.text = TimeSpan.FromSeconds(remainingTime).ToString(@"mm\:ss");
	}

	private void OnNewDeliveryCreated(DeliveryData deliveryData)
	{
		_pagerText.text = $"{deliveryData.ToDeliver.PagerCode} - {deliveryData.DeliveryLocation.PagerCode}";
		_timeText.text = TimeSpan.FromSeconds(deliveryData.DeliveryTime).ToString(@"mm\:ss");
		_pagerTransform.DOAnchorPosY(_pagerTransform.sizeDelta.y, _animationsDuration).SetEase(Ease.InOutBack);
	}
}