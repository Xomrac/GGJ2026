using System;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;

public class DeskInteractor : MonoBehaviour
{
	[SerializeField, ReadOnly] private DeliverableData _expectedItem;

	private Player _player;
	private DeliverableData _deliverableItem;
	private DeliverableItem _spawnedObject;
	private bool _interactionsAreBlocked;

	public void Setup(DeliverableData deliverableItem)
	{
		_deliverableItem = deliverableItem;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (_interactionsAreBlocked) return;
		
		var deliverableItem = other.GetComponent<DeliverableItem>();
		if (deliverableItem && deliverableItem.Data == _expectedItem)
		{
			TryToDeliverItem(deliverableItem);
			_interactionsAreBlocked = true;
			return;
		}
		_player = other.GetComponent<Player>();
		if (!_player) return;
		GrabObject();
		_interactionsAreBlocked = true;
	}

	private void OnTriggerExit(Collider other)
	{
		var player = other.GetComponent<Player>();
		if (!player) return;
		_interactionsAreBlocked = false;
	}

	private void GrabObject()
	{
		if (_spawnedObject == null)
		{
			_spawnedObject = _deliverableItem.SpawnProp();
		}
		_player.ObjectManager.GrabObject(_spawnedObject);
	}

	public void SetupForDelivery(DeliverableData item)
	{
		_expectedItem = item;
	}

	private void TryToDeliverItem(DeliverableItem deliverableItem)
	{
		if (deliverableItem.Data == _expectedItem)
		{
			DeliveriesManager.instance.CompleteDelivery();
		}
		else
		{
			DeliveriesManager.instance.FailDelivery();
		}
		Destroy(deliverableItem.gameObject);
	}
}