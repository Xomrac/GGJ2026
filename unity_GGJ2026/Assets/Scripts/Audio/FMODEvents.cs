using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance { get; private set; }

    [field: SerializeField] public EventReference playerFootsteps { get; private set; }
    [field: SerializeField] public EventReference playerJump { get; private set; }
    [field: SerializeField] public EventReference itemPickup { get; private set; }
    [field: SerializeField] public EventReference itemThrow { get; private set; }
    [field: SerializeField] public EventReference pagerNotification { get; private set; }
    [field: SerializeField] public EventReference timerTheme { get; private set; }
    [field: SerializeField] public EventReference romanceTheme { get; private set; }
    [field: SerializeField] public EventReference deliveryComplete { get; private set; }
    [field: SerializeField] public EventReference deliveryFailed { get; private set; }
    [field: SerializeField] public EventReference menuButton { get; private set; }
    [field: SerializeField] public EventReference menuPlay { get; private set; }
    [field: SerializeField] public EventReference menuBack { get; private set; }

        private void Awake()
    {
        Instance = this;
    }
}
