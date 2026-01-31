using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private List<EventInstance> eventInstances = new List<EventInstance>();

    [Range(0, 1)] public float masterVolume = 1f;
    [Range(0, 1)] public float musicVolume = 1f;
    [Range(0, 1)] public float sfxVolume = 1f;

    private VCA masterVCA;
    private VCA musicVCA;
    private VCA sfxVCA;

    private void Awake()
    {
        Instance = this;

        masterVCA = RuntimeManager.GetVCA("vca:/Master");
        musicVCA = RuntimeManager.GetVCA("vca:/Music");
        sfxVCA = RuntimeManager.GetVCA("vca:/Sfx");
    }

    void Update()
    {
        masterVCA.setVolume(masterVolume);
        musicVCA.setVolume(musicVolume);
        sfxVCA.setVolume(sfxVolume);
    }

    public void PlayOneShot(EventReference eventName, Vector3 worldPosition = default)
    {
        RuntimeManager.PlayOneShot(eventName, worldPosition);
    }

    public EventInstance CreateEventInstance(EventReference eventName, Vector3 worldPosition = default)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventName);
        eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(worldPosition));
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public void SetEventParameter(EventInstance eventInstance, string parameterName, float parameterValue)
    {
        eventInstance.setParameterByName(parameterName, parameterValue);
    }

    public void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.release();
        }
    }

    void OnDestroy()
    {
        CleanUp();
    }
}