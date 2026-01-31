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

		public event Action FirstDelivery;
		public int firstDeliveryDone = 0;

        [SerializeField,ReadOnly]private float _currentTime;
		[SerializeField,ReadOnly]private float _currentDeliveryTime;
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
			
		}
		
		public void stopAll()
		{
			StopAllCoroutines();
			gameObject.SetActive(false);
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
			DeliverableData itemToDeliver = _possibleItems[UnityEngine.Random.Range(0, _possibleItems.Count)];
			Debug.Log("Waited enough, creating delivery...");
			var dropoffZones = new List<DeskInteractionZone>();
			foreach (var desk in _possibleDestinations)
			{
				if (desk.DeskInteractionZone.PossibleItems.Contains(itemToDeliver))
				{
					continue;
				}
				dropoffZones.Add(desk.DeskInteractionZone);
			}
			DeskInteractionZone deliveryLocation = dropoffZones[UnityEngine.Random.Range(0, dropoffZones.Count)];
			Debug.Log("New Delivery Created!");
			CreateDelivery(itemToDeliver, deliveryLocation, 30f);
		}
		public void CreateDelivery(DeliverableData toDeliver, DeskInteractionZone deliveryLocation, float deliveryTime)
		{
			DeliveryData newDelivery = new DeliveryData(toDeliver, deliveryLocation,deliveryTime);
			Debug.Log($"Delivery Created for {toDeliver.Name} to {deliveryLocation.transform.parent.parent.name}!");
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
			firstDeliveryDone++;
            if (firstDeliveryDone==1)
			{
				FirstDelivery?.Invoke();
            }
		}
		
		public void FailDelivery()
		{
			StopAllCoroutines();
			DeliveryFailed?.Invoke(_currentDelivery);
			ScoreTracker.AddScore(0);
			StartCoroutine(WaitAndStartDelivery());
            firstDeliveryDone++;
            if (firstDeliveryDone == 1)
            {
                FirstDelivery?.Invoke();
            }
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

		public void StartDeliveries()
		{
			StartCoroutine(WaitAndStartDelivery());
		}
	}
	

}