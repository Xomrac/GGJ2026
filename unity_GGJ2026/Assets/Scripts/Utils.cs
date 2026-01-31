using UnityEngine;

namespace DefaultNamespace
{

	public static class Utils
	{
		public static int LayerMaskToLayer(LayerMask mask)
		{
			int val = mask.value;
			for (int i = 0; i < 32; i++)
			{
				if ((val & (1 << i)) != 0) return i;
			}
			return 0;
		}
	}

}