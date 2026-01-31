using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Player")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }
}
