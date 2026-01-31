using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;

public class DropoffZone : MonoBehaviour
{
	[SerializeField, ReadOnly] private DeliverableData _expectedItem;
	public string pagerCode;
	
	public void SetupForDelivery(DeliverableData item)
	{
		_expectedItem = item;
	}

	private void OnTriggerEnter(Collider other)
	{
		var deliverableItem = other.GetComponent<DeliverableItem>();
		if (deliverableItem && deliverableItem.Data == _expectedItem)
		{
			TryToDeliverItem(deliverableItem);
			return;
		}
		var player = other.GetComponent<Player>();
		
		if (!player) return;
		if (!player.ObjectManager.hasObject) return;
		
		TryToDeliverItem(player.ObjectManager.heldObject);
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