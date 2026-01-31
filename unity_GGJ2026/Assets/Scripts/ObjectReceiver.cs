using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;

public class ObjectReceiver : MonoBehaviour
{
	[SerializeField,ReadOnly] private DeliverableData _expectedItem;
	private Player _player;
   
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
		}
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