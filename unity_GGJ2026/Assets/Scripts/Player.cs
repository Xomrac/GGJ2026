using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private FirstPersonController _fpsController;
    [SerializeField] private ObjectManager _objectManager;
    public int lives = 3;
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
        _objectManager.ForceRemoval();
        lives--;
                if (lives <= 0)
        {
            UIManager.instance.gameOver();
        }
    }
}
