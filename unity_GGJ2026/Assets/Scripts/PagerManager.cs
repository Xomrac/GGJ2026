using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class PagerManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _pagerText;
	[SerializeField] private TextMeshProUGUI _timeText;
	
	private DeliveriesManager _deliveriesManager;
	

	private void Awake()
	{
		_deliveriesManager = FindAnyObjectByType<DeliveriesManager>();
		_deliveriesManager.NewDeliveryCreated += OnNewDeliveryCreated;
		_deliveriesManager.DeliveryTimeUpdated += OnDeliveryTimeUpdated;
		_deliveriesManager.DeliveryFailed += OnDeliveryFailed;
		_deliveriesManager.DeliveryCompleted += OnDeliveryCompleted;
	}

	private void OnDeliveryCompleted(DeliveryData _)
	{
		_pagerText.text = "Delivery Completed!";
		_timeText.text = "--:--";
	}

	private void OnDeliveryFailed(DeliveryData _)
	{
		_pagerText.text = "Delivery Failed! - FAI CACARE";
		_timeText.text = "--:--";
	}

	private void OnDeliveryTimeUpdated(float remainingTime)
	{
		_timeText.text = TimeSpan.FromSeconds(remainingTime).ToString(@"mm\:ss");
	}

	private void OnNewDeliveryCreated(DeliveryData deliveryData)
	{
		_pagerText.text = $"{deliveryData.ToDeliver.name} to {deliveryData.DeliveryLocation.DeskName}";
	}
}