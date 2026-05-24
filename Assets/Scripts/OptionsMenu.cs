using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;

    [SerializeField] private string SceneName;

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