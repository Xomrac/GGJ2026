using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class Desk : MonoBehaviour
{
   [SerializeField] private string _deskName;
   public string DeskName => _deskName;
   [SerializeField] private string _pagerCode;
   [SerializeField] private TextMeshProUGUI _deskLabel;
   [SerializeField] private DeskInteractor _deskInteractor;
   [SerializeField] private DeliverableData _deliverableItem;
   public DeliverableData DeliverableItem => _deliverableItem;
   public string PagerCode => _pagerCode;

   private void Awake()
   {
      _deskLabel.text = $"{_pagerCode} - {_deskName}";
      _deskInteractor.Setup(_deliverableItem);
   }
   
   public void SetupForDelivery(DeliverableData item)
   {
      _deskInteractor.SetupForDelivery(item);
   }
   
   
}
