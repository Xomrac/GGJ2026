using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioEventEmitter : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private EventReference eventReference;
    [SerializeField] private bool playOnStart = true;
    private EventInstance eventInstance;

    private void Start()
    {
        eventInstance = AudioManager.Instance.CreateEventInstance(eventReference, transform.position);

        if (playOnStart)
            eventInstance.start();
    }

    private void OnDestroy()
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        eventInstance.release();
    }
}
