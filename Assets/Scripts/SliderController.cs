using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SliderController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;

    [Header("Mixer")]
    public AudioMixer mixer;
    public Slider volumeSlider;

    [SerializeField] private string SceneName;

    private const string PREF_KEY = "MasterVolume";
    private const string MIXER_PARAM = "MasterVolume";

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(PREF_KEY, 1f);

        if (volumeSlider != null)
        {
            volumeSlider.SetValueWithoutNotify(savedVolume);
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        ApplyVolume(savedVolume);
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat(PREF_KEY, volume);
        PlayerPrefs.Save();

        ApplyVolume(volume);
    }

    private void ApplyVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f);

        float volumeDB = Mathf.Log10(volume) * 20f;

        mixer.SetFloat(MIXER_PARAM, volumeDB);
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void Out()
    {
        Application.Quit();
    }
}