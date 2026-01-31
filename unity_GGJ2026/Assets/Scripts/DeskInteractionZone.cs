using System;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;

public class DeskInteractionZone : MonoBehaviour
{

	private Player _player;
	private DeliverableData _deliverableItem;
	private DeliverableItem _spawnedObject;
	[SerializeField,ReadOnly]private DeliverableData _expectedItem;
	public DeliverableData ExpectedItem => _expectedItem;
	public DeliverableData DeliverableItem => _deliverableItem;
	private bool _interactionsAreBlocked;
	private string _pagerCode;
	public string PagerCode => _pagerCode;
	
	private bool _canInteract = true;

	private void Awake()
	{
		_player = FindAnyObjectByType<Player>();
	}
	
	public void SetupForDelivery(DeliverableData item)
	{
		_expectedItem = item;
	}

	public void Setup(DeliverableData deliverableItem, string code)
	{
		_deliverableItem = deliverableItem;
		_pagerCode = code;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!_canInteract ) return;
		var deliverableItem = other.GetComponent<DeliverableItem>();
		if (deliverableItem != null)
		{
			TryToDeliverItem(deliverableItem);
			_canInteract = false;
			return;
		}
		var player = other.GetComponent<Player>();
		
		if (!player) return;
		if (player.ObjectManager.hasObject&&DeliveriesManager.instance.isActiveAndEnabled)
		{
			TryToDeliverItem(player.ObjectManager.heldObject);
			_canInteract = false;
			return;
		}
		GrabObject();
		_canInteract = false;
	}

	private void OnTriggerExit(Collider other)
	{
		var player = other.GetComponent<Player>();
		if (!player) return;
		_canInteract = true;
	}

	private void TryToDeliverItem(DeliverableItem deliverableItem)
	{
		if (deliverableItem.Data == _expectedItem)
		{
			DeliveriesManager.instance.CompleteDelivery();
		}
		else
		{
			Debug.Log(deliverableItem.Data + " does not match " + _expectedItem);
			DeliveriesManager.instance.FailDelivery();
		}
		Destroy(deliverableItem.gameObject);
	}

	private void GrabObject()
	{
		if (_spawnedObject == null)
		{
			_spawnedObject = _deliverableItem.SpawnProp();
		}
		_player.ObjectManager.GrabObject(_spawnedObject);
	}

}