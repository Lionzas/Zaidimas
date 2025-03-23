using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class SoundManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _sliderText;
    [SerializeField] private Slider volumeSlider;

    void Start()
    {
        volumeSlider.onValueChanged.AddListener((v) => {
            _sliderText.text = v.ToString("0.00");
        });

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        _sliderText.text = volumeSlider.value.ToString("0.00");  
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}