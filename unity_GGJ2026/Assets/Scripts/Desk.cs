using TMPro;
using UnityEngine;

public class Desk : MonoBehaviour
{
	[SerializeField] private string _deskName;
	public string DeskName => _deskName;
	[SerializeField] private string _pagerCode;
	[SerializeField] private TextMeshProUGUI _deskLabel;
	[SerializeField] private DeskInteractionZone deskInteractionZone;

	[SerializeField] private DeliverableData _deliverableItem;
	public DeliverableData DeliverableItem => _deliverableItem;
	public string PagerCode => _pagerCode;
	public DeskInteractionZone DeskInteractionZone => deskInteractionZone;

	private void Awake()
	{
		_deskLabel.text = $"{_pagerCode} - {_deskName}";
		deskInteractionZone?.Setup(_deliverableItem, _pagerCode);
	}
}