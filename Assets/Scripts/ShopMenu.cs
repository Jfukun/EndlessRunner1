using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public Button shop;
    void Start()
    {
        shop.onClick.AddListener(ChangeScene);
    }

    private void ChangeScene()
    {
        SaveGameManager.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
    }
}
