using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;



    [SerializeField] private string SceneName;

    private void Awake()
    {
        SaveGameManager.Load();
    }

    public void ChangeScene()
    {
        SaveGameManager.Save();
        SoundsManager.SoundButton();
        SceneManager.LoadScene(SceneName);
    }

    public void Out()
    {
        Application.Quit();
    }


}