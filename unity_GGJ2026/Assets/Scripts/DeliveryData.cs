using DefaultNamespace;
using UnityEngine;

public class DeliveryData
{
	private DeliverableData _toDeliver;
	public DeliverableData ToDeliver => _toDeliver;
	
	private DeskInteractionZone _deliveryLocation;
	public DeskInteractionZone DeliveryLocation => _deliveryLocation;
	
	private float _deliveryTime;
	public float DeliveryTime => _deliveryTime;
	
	public float elapsedTime;
	public float score;

	public DeliveryData(DeliverableData go, DeskInteractionZone delivery, float deliveryTime)
	{
		_toDeliver = go;
		_deliveryLocation = delivery;
		_deliveryTime = deliveryTime;
		elapsedTime = 0f;
	}
	
}