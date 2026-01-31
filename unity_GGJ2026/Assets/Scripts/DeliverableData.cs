using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "DeliverableData", menuName = "ScriptableObjects/DeliverableData", order = 1)]
public class DeliverableData : ScriptableObject
{
	[SerializeField] private DeliverableItem _prefab;
	[SerializeField] private string _name;
	[SerializeField] private string _pagerCode;

	
	public DeliverableItem Prefab => _prefab;
	public string Name => _name;
	
	public string PagerCode => _pagerCode;
	public DeliverableItem SpawnProp()
	{
		var item = Instantiate(_prefab);
		item.Setup(this);
		return item;
	}
    
}