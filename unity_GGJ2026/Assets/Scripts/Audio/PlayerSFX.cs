using FMOD.Studio;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    private FirstPersonController player;
    private EventInstance playerFootsteps;

    void Start()
    {
        player = GetComponent<FirstPersonController>();
        playerFootsteps = AudioManager.Instance.CreateEventInstance(FMODEvents.Instance.playerFootsteps);
    }

    void FixedUpdate()
    {
        PLAYBACK_STATE playbackState;
        playerFootsteps.getPlaybackState(out playbackState);

        if (player.isWalking)
        {
            if (playbackState == PLAYBACK_STATE.STOPPED || playbackState == PLAYBACK_STATE.STOPPING)
            {
                playerFootsteps.start();
            }
        }
        else
        {
            if (playbackState == PLAYBACK_STATE.PLAYING)
            {
                playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }

        if (player.isSprinting)
        {
            AudioManager.Instance.SetEventParameter(playerFootsteps, "Speed", 1f);
        }
        else
        {
            AudioManager.Instance.SetEventParameter(playerFootsteps, "Speed", 0f);
        }
    }

    public void PlayThrowSFX()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.itemThrow, transform.position);
    }
    
    public void PlayJumpSFX()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.playerJump, transform.position);
    }
}
