using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace DefaultNamespace
{

	public class DeliveriesManager : MonoBehaviour
	{
		public static DeliveriesManager instance;
	
		public event Action<DeliveryData> NewDeliveryCreated;
		public event Action<DeliveryData> DeliveryStarted;
		public event Action<float> DeliveryTimeUpdated;
		public event Action<DeliveryData> DeliveryCompleted;
		public event Action<DeliveryData> DeliveryFailed;
		
		
		private float _currentTime;
		private DeliveryData _currentDelivery;

		[SerializeField] private DeliverableData _testItem;
		[SerializeField] private Desk _testDestination;
		[SerializeField] private float _timeBeforeDelivery= 10f;
		[SerializeField] private List<DeliverableData> _possibleItems;
		[SerializeField] private List<Desk> _possibleDestinations;
		

		

		
		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			StartCoroutine(WaitAndStartDelivery());
		}
		
		
		private IEnumerator WaitAndStartDelivery()
		{
			yield return new WaitForSeconds(_timeBeforeDelivery);
			Desk deliveryLocation = _possibleDestinations[UnityEngine.Random.Range(0, _possibleDestinations.Count)];
			List<DeliverableData> availableItems = new List<DeliverableData>();
			foreach (DeliverableData item in _possibleItems)
			{
				if (item != deliveryLocation.DeliverableItem)
				{
					availableItems.Add(item);
				}
			}
			DeliverableData itemToDeliver = _possibleItems[UnityEngine.Random.Range(0, availableItems.Count)];
			CreateDelivery(itemToDeliver, deliveryLocation, 30f);
		}
		public void CreateDelivery(DeliverableData toDeliver, Desk deliveryLocation, float deliveryTime)
		{
			DeliveryData newDelivery = new DeliveryData(toDeliver, deliveryLocation,deliveryTime);
			deliveryLocation.SetupForDelivery(toDeliver);
			_currentDelivery = newDelivery;
			NewDeliveryCreated?.Invoke(newDelivery);
			StartCoroutine(RunClock());
			DeliveryStarted?.Invoke(newDelivery);
		}

		public void CompleteDelivery()
		{
			StopAllCoroutines();
			DeliveryCompleted?.Invoke(_currentDelivery);
			Debug.Log("Delivery Completed!");
			StartCoroutine(WaitAndStartDelivery());
		}
		
		public void FailDelivery()
		{
			StopAllCoroutines();
			DeliveryFailed?.Invoke(_currentDelivery);
			StartCoroutine(WaitAndStartDelivery());
		}

		private IEnumerator RunClock()
		{
			_currentTime = _currentDelivery.DeliveryTime;
			while (_currentTime > 0)
			{
				_currentTime -= Time.deltaTime;
				DeliveryTimeUpdated?.Invoke(_currentTime);
				yield return null;
			}
			DeliveryFailed?.Invoke(_currentDelivery);
		}
		
	}
	

}