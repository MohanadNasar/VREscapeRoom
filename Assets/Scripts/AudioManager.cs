using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Toggle muteToggle;

    private const string VolumeKey = "MusicVolume";
    private const string MuteKey = "IsMuted";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.5f);
        bool isMuted = PlayerPrefs.GetInt(MuteKey, 0) == 1;

        SetVolume(savedVolume);
        SetMute(isMuted);

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        if (muteToggle != null)
        {
            muteToggle.isOn = isMuted;
            muteToggle.onValueChanged.AddListener(SetMute);
        }
    }

    public void SetVolume(float volume)
    {
        if (volume <= 0.0001f) volume = 0.0001f; // Prevent log10(0)
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(VolumeKey, volume);

        if (volumeSlider != null && !muteToggle.isOn)
            volumeSlider.value = volume;
    }

    public void SetMute(bool mute)
    {
        audioMixer.SetFloat("MusicVolume", mute ? -80f : Mathf.Log10(PlayerPrefs.GetFloat(VolumeKey, 1f)) * 20);
        PlayerPrefs.SetInt(MuteKey, mute ? 1 : 0);
    }
}
