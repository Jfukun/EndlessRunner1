using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Skin
{
    public Material m_SkinMaterial;
    public int m_Price;
    public bool m_IsUnlocked;
}

public class ShopManager : MonoBehaviour
{

    public string m_CoinPrefix = "Coins: ";
    public string m_GemPrefix = "Gems: ";

    public TextMeshProUGUI m_CoinsText;
    public TextMeshProUGUI m_GemsText;

    private int m_LastCoins = -1;
    private int m_LastGems = -1;

    public TextMeshProUGUI m_PriceText;
    public Button m_BuyButton;
    public Button m_EquipButton;

    public Button previous;
    public Button next;
    public Button back;

    public Skin[] m_Skins;
    private int m_CurrentSkinIndex = 0;


    public MaterialChanger m_MaterialChanger;

    private int coins;
    private int gems;

    private void Awake()
    {
        SaveGameManager.Load();
    }
    void Start()
    {
        next.onClick.AddListener(NextSkin);
        previous.onClick.AddListener(PreviousSkin);
        back.onClick.AddListener(ChangeScene);
        m_BuyButton.onClick.AddListener(BuySkin);
        m_EquipButton.onClick.AddListener(EquipSkin);

        // New  game
        if (GameManager.UnlockedSkins == null || GameManager.UnlockedSkins.Length != m_Skins.Length)
        {
            GameManager.UnlockedSkins = new bool[m_Skins.Length];
            GameManager.UnlockedSkins[0] = true;
        }
        for (int i = 0; i < m_Skins.Length; i++)
        {
            m_Skins[i].m_IsUnlocked = GameManager.UnlockedSkins[i];
        }
        m_CurrentSkinIndex = GameManager.EquippedSkinIndex;
        EquipSkin();
        m_CurrentSkinIndex = 0;
        UpdateUI();

    }

    void Update()
    {
        coins = GameManager.Coins;
        gems = GameManager.Gems;

        if (coins != m_LastCoins)
        {
            m_LastCoins = coins;
            m_CoinsText.text = m_CoinPrefix + coins;
        }

        if (gems != m_LastGems)
        {
            m_LastGems = gems;
            m_GemsText.text = m_GemPrefix + gems;
        }

    }

    public void NextSkin()
    {
        m_CurrentSkinIndex++;

        if (m_CurrentSkinIndex >= m_Skins.Length)
        {
            m_CurrentSkinIndex = 0;
        }

        UpdateUI();
    }
    public void PreviousSkin()
    {
        m_CurrentSkinIndex--;

        if (m_CurrentSkinIndex < 0)
        {
            m_CurrentSkinIndex = m_Skins.Length - 1;
        }

        UpdateUI();
    }

    public void BuySkin()
    {
        Skin currentSkin = m_Skins[m_CurrentSkinIndex];

        if (!currentSkin.m_IsUnlocked && GameManager.TrySpendCoins(currentSkin.m_Price))
        {
            currentSkin.m_IsUnlocked = true;

            GameManager.UnlockedSkins[m_CurrentSkinIndex] = true;
            SaveGameManager.Save();

            UpdateUI();
            Debug.Log("Skin acquired!");
        }
        else
        {
            Debug.Log("Not enough coins.");
        }
    }

    public void EquipSkin()
    {
        Skin currentSkin = m_Skins[m_CurrentSkinIndex];

        if (currentSkin.m_IsUnlocked)
        {
            GameManager.EquippedSkinIndex = m_CurrentSkinIndex;
            SaveGameManager.Save();
            Debug.Log("Skin equipped");
        }
    }

    private void UpdateUI()
    {
        Skin currentSkin = m_Skins[m_CurrentSkinIndex];
        m_MaterialChanger.m_NewMaterial = currentSkin.m_SkinMaterial;
        m_MaterialChanger.ChangeChildrenMaterials();

        if (currentSkin.m_IsUnlocked)
        {
            m_PriceText.text = "Unlocked";
            m_BuyButton.gameObject.SetActive(false);
            m_EquipButton.gameObject.SetActive(true);
        }
        else
        {
            m_PriceText.text = "Price: " + currentSkin.m_Price;
            m_BuyButton.gameObject.SetActive(true);
            m_EquipButton.gameObject.SetActive(false);

            m_BuyButton.interactable = (coins >= currentSkin.m_Price);
        }
    }

    private void ChangeScene()
    {
        SaveGameManager.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }
}
