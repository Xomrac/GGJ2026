using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances = new List<EventInstance>();

    [Range(0, 1)] public float masterVolume = 1f;
    [Range(0, 1)] public float musicVolume = 1f;
    [Range(0, 1)] public float ambienceVolume = 1f;
    [Range(0, 1)] public float sfxVolume = 1f;

    private VCA masterVCA;
    private VCA musicVCA;
    private VCA ambienceVCA;
    private VCA sfxVCA;

    private void Awake()
    {
        masterVCA = RuntimeManager.GetVCA("vca:/Master");
        musicVCA = RuntimeManager.GetVCA("vca:/Music");
        ambienceVCA = RuntimeManager.GetVCA("vca:/Ambience");
        sfxVCA = RuntimeManager.GetVCA("vca:/Sfx");
    }

    void Update()
    {
        masterVCA.setVolume(masterVolume);
        musicVCA.setVolume(musicVolume);
        ambienceVCA.setVolume(ambienceVolume);
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