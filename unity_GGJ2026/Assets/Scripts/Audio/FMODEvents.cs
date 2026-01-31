using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance { get; private set; }

    [field: Header("Player")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }
    [field: SerializeField] public EventReference playerJump { get; private set; }
    [field: SerializeField] public EventReference itemPickup { get; private set; }
    [field: SerializeField] public EventReference itemThrow { get; private set; }

        private void Awake()
    {
        Instance = this;
    }
}
