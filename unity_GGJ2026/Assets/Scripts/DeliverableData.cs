using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "DeliverableData", menuName = "ScriptableObjects/DeliverableData", order = 1)]
public class DeliverableData : ScriptableObject
{
	[SerializeField] private DeliverableItem _prefab;
	[SerializeField] private string _name;
	public DeliverableItem Prefab => _prefab;
	public string Name => _name;

	public DeliverableItem SpawnProp()
	{
		var item = Instantiate(_prefab);
		item.Setup(this);
		return item;
	}
    
}