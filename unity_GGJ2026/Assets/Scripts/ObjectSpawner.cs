using DefaultNamespace;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
	private DeliverableData _deliverableItem;

	private DeliverableItem _spawnedObject;
	private Player _player;

	public void Setup(DeliverableData deliverableItem)
	{
		_deliverableItem = deliverableItem;
	}
	private void OnTriggerEnter(Collider other)
	{
		_player = other.GetComponent<Player>();
		if (!_player) return;
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