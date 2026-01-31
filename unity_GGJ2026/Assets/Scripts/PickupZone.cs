using System;
using DefaultNamespace;
using UnityEngine;

public class PickupZone : MonoBehaviour
{

	private Player _player;
	private DeliverableData _deliverableItem;
	private DeliverableItem _spawnedObject;
	private bool _interactionsAreBlocked;

	private void Awake()
	{
		_player = FindAnyObjectByType<Player>();
	}

	public void Setup(DeliverableData deliverableItem)
	{
		_deliverableItem = deliverableItem;
	}

	private void OnTriggerEnter(Collider other)
	{
		GrabObject();
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