using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Desk : MonoBehaviour
{
	[SerializeField] private string _deskName;
	public string DeskName => _deskName;
	[SerializeField] private string _pagerCode;
	[SerializeField] private TextMeshProUGUI _deskLabel;
	[SerializeField] private DeskInteractionZone deskInteractionZone;

	[SerializeField] private List<DeliverableData> _availableItems;
	public List<DeliverableData> DeliverableItems => new();
	public string PagerCode => _pagerCode;
	public DeskInteractionZone DeskInteractionZone => deskInteractionZone;

	private void Awake()
	{
		_deskLabel.text = $"{_pagerCode} - {_deskName}";
		deskInteractionZone.SetPool(_availableItems);
		deskInteractionZone?.Setup(_availableItems[Random.Range(0, _availableItems.Count)], _pagerCode);
	}
	
	public void TrySetSpecificItem(DeliverableData item)
	{
		if (!_availableItems.Contains(item)) return;
		deskInteractionZone?.Setup(item, _pagerCode);
	}
}