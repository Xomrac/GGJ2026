using AYellowpaper.SerializedCollections;
using UnityEngine;

public class CrushTimerManager : MonoBehaviour
{
	[SerializeField] private SerializedDictionary<LoveInterest,CrushTimer> serializedDictionary;

	private void Start()
	{
		foreach (var kvp in serializedDictionary)
		{
			LoveInterest loveInterest = kvp.Key;
			CrushTimer crushTimer = kvp.Value;

			loveInterest.CrushMadeRequest += crushTimer.StartTimer;
			loveInterest.CrushRequestCompleted += crushTimer.InterruptTimer;
		}
	}

}