using TMPro;
using UnityEngine;

public class Desk : MonoBehaviour
{
	[SerializeField] private string _deskName;
	public string DeskName => _deskName;
	[SerializeField] private string _pagerCode;
	[SerializeField] private TextMeshProUGUI _deskLabel;
	[SerializeField] private PickupZone pickupZone;
	[SerializeField] private DropoffZone _dropoffZone;

	[SerializeField] private DeliverableData _deliverableItem;
	public DeliverableData DeliverableItem => _deliverableItem;
	public string PagerCode => _pagerCode;

	private void Awake()
	{
		_deskLabel.text = $"{_pagerCode} - {_deskName}";
		pickupZone?.Setup(_deliverableItem);
		if (_dropoffZone)
		{
			_dropoffZone.pagerCode = _pagerCode;
		}
	}
}