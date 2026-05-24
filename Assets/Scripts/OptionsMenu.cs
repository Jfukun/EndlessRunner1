using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;



    [SerializeField] private string SceneName;



    public void ChangeScene()
    {
        SoundsManager.SoundButton();
        SceneManager.LoadScene(SceneName);
    }

    public void Out()
    {
        Application.Quit();
    }


}