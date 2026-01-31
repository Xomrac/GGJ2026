using System;
using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private FirstPersonController _fpsController;
    [SerializeField] private ObjectManager _objectManager;
    public ObjectManager ObjectManager => _objectManager;

    private void Start()
    {
        DeliveriesManager.instance.DeliveryFailed += OnDeliveryFailed;
        DeliveriesManager.instance.DeliveryCompleted += OnDeliveryCompleted;
    }

    private void OnDeliveryCompleted(DeliveryData _)
    {
        _objectManager.RemoveItem();
    }

    private void OnDeliveryFailed(DeliveryData _)
    {
        //_objectManager.ForceRemoval();
    }
}
