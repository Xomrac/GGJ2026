using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class Desk : MonoBehaviour
{
   [SerializeField] private string _deskName;
   public string DeskName => _deskName;
   [SerializeField] private TextMeshProUGUI _deskLabel;
   [SerializeField] private ObjectSpawner _objectSpawner;
   [SerializeField] private ObjectReceiver _objectReceiver;
   [SerializeField] private DeliverableData _deliverableItem;
   public DeliverableData DeliverableItem => _deliverableItem;

   private void Awake()
   {
      _deskLabel.text = _deskName;
      _objectSpawner.Setup(_deliverableItem);
   }
   
   public void SetupForDelivery(DeliverableData item)
   {
      _objectReceiver.SetupForDelivery(item);
   }
   
   
}
