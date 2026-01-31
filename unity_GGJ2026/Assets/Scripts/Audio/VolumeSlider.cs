using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    private enum VolumeType
    {
        MASTER,
        MUSIC,
        SFX
    }

    [Header("Type")]
    [SerializeField] private VolumeType volumeType;
    private Slider volumeSlider;
    //[SerializeField] private TextMeshProUGUI valueText;

    void Awake()
    {
        volumeSlider = GetComponentInChildren<Slider>();
    }

    void Start()
    {
        //UpdateVolumeText(volumeSlider.value);
    }

    void Update()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                volumeSlider.value = AudioManager.Instance.masterVolume;
                break;
            case VolumeType.MUSIC:
                volumeSlider.value = AudioManager.Instance.musicVolume;
                break;
            case VolumeType.SFX:
                volumeSlider.value = AudioManager.Instance.sfxVolume;
                break;
            default:
                Debug.LogWarning("Volume Type not supported: " + volumeType);
                break;
        }

        //UpdateVolumeText(volumeSlider.value);
    }

    public void OnSliderValueChanged()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                AudioManager.Instance.masterVolume = volumeSlider.value;
                break;
            case VolumeType.MUSIC:
                AudioManager.Instance.musicVolume = volumeSlider.value;
                break;
            case VolumeType.SFX:
                AudioManager.Instance.sfxVolume = volumeSlider.value;
                break;
            default:
                Debug.LogWarning("Volume Type not supported: " + volumeType);
                break;
        }

        //UpdateVolumeText(volumeSlider.value);
    }
    /*
        private void UpdateVolumeText(float value)
        {
            if (valueText != null)
            {
                valueText.text = value.ToString("F2");  
            }
        }
        */
}
