using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class FadingManager : MonoBehaviour
{
	public static FadingManager instance;
	[SerializeField] private CanvasGroup _canvasGroup;

	
	
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

		_canvasGroup.DOFade(1, 0f);
	}

	public void Fade(float endValue, float duration = .8f, UnityAction OnComplete = null)
	{
		_canvasGroup.DOFade(endValue, duration).OnComplete(() => OnComplete?.Invoke());
	}
}
