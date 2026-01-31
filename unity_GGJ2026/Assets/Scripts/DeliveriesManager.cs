using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using NaughtyAttributes;
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
		
		
		[SerializeField,ReadOnly]private float _currentTime;
		[SerializeField,ReadOnly]private float _currentDeliveryTime;
		private DeliveryData _currentDelivery;

		[SerializeField] private DeliverableData _testItem;
		[SerializeField] private Desk _testDestination;
		[SerializeField] private float _timeBeforeDelivery= 10f;
		[SerializeField] private List<DeliverableData> _possibleItems;
		[SerializeField] private List<DropoffZone> _possibleDestinations;
		
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
			_currentDeliveryTime = 0f;
			Debug.Log("Started new delivery in a bit...");
			while (_currentDeliveryTime < _timeBeforeDelivery)
			{
				_currentDeliveryTime += Time.deltaTime;
				yield return null;
			}
			Debug.Log("Waited enough, creating delivery...");
			DropoffZone deliveryLocation = _possibleDestinations[UnityEngine.Random.Range(0, _possibleDestinations.Count)];
			DeliverableData itemToDeliver = _possibleItems[UnityEngine.Random.Range(0, _possibleItems.Count)];
			Debug.Log("New Delivery Created!");
			CreateDelivery(itemToDeliver, deliveryLocation, 30f);
		}
		public void CreateDelivery(DeliverableData toDeliver, DropoffZone deliveryLocation, float deliveryTime)
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
			var score = Mathf.InverseLerp(0, _currentDelivery.DeliveryTime , _currentDelivery.DeliveryTime - _currentDelivery.elapsedTime);
			Debug.Log(score);
			_currentDelivery.score = score;
			DeliveryCompleted?.Invoke(_currentDelivery);
			ScoreTracker.AddScore(score);
			Debug.Log("Delivery Completed!");
			StartCoroutine(WaitAndStartDelivery());
		}
		
		public void FailDelivery()
		{
			StopAllCoroutines();
			DeliveryFailed?.Invoke(_currentDelivery);
			ScoreTracker.AddScore(0);
			StartCoroutine(WaitAndStartDelivery());
		}

		private IEnumerator RunClock()
		{
			_currentTime = _currentDelivery.DeliveryTime;
			while (_currentTime > 0)
			{
				_currentTime -= Time.deltaTime;
				_currentDelivery.elapsedTime += Time.deltaTime;
				DeliveryTimeUpdated?.Invoke(_currentTime);
				yield return null;
			}
			FailDelivery();
		}
		
	}
	

}