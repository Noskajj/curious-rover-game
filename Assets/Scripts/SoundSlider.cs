using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum SliderType
{
    Music, Sfx
}

public class SoundSlider : MonoBehaviour
{

    [SerializeField]
    private SliderType sliderType;

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        if(sliderType == SliderType.Music)
        {
            slider.value = PlayerPrefs.GetFloat("MusicVol");
        }
        else
        {
            slider.value = PlayerPrefs.GetFloat("SoundVol");
        }

        slider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float value)
    {
        if (sliderType == SliderType.Music)
        {
            Settings.Instance.UpdateMusicVolume(value);
        }
        else
        {
            Settings.Instance.UpdateSoundVolume(value);
        }
    }
}
