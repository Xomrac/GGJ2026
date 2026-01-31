using DefaultNamespace;
using UnityEngine;

public class DeliveryData
{
	private DeliverableData _toDeliver;
	public DeliverableData ToDeliver => _toDeliver;
	
	private Desk _deliveryLocation;
	public Desk DeliveryLocation => _deliveryLocation;
	
	private float _deliveryTime;
	public float DeliveryTime => _deliveryTime;

	public DeliveryData(DeliverableData go, Desk delivery, float deliveryTime)
	{
		_toDeliver = go;
		_deliveryLocation = delivery;
		_deliveryTime = deliveryTime;
	}
	
}