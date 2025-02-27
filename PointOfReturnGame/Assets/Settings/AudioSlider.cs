using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;
    //[SerializeField]
    //private AudioSource audioSource;
    [SerializeField]
    private TextMeshProUGUI valueText;

    public void OnChangeSlider(float value)
    {
        valueText.SetText($"{(value * 100).ToString("N0")}" + "%");
        mixer.SetFloat("Volume", Mathf.Log10(value) * 20); // human sound perception is logarithmic
        // saving of settings could be implemented with these:
        //PlayerPrefs.SetFloat("Volume", value);
        //PlayerPrefs.Save();
    }

    // also for saving settings
    /*public void Start()
    {
        mixer.SetFloat("Volume", Mathf.Log10(PlayerPrefs.GetFloat("Volume", 1) * 20));
    }*/
}
