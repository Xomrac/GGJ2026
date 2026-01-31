using System;
using UnityEngine;

namespace DefaultNamespace
{

	public class DeliverableItem : MonoBehaviour
	{
		[SerializeField] private Rigidbody _rigidbody;
		public Rigidbody Rigidbody => _rigidbody;

		private string _name;
		public string Name => _name;
		
		private DeliverableData _data;
		public DeliverableData Data => _data;
		

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
		}

		public void Setup(DeliverableData data)
		{
			_data = data;
			_name = data.Name;
		}

	}

}