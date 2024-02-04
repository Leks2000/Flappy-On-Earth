using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkinChanger : MonoBehaviour
{
    [Header("Shop Settings")]
    [SerializeField] private Canvas canvasShop;
    [SerializeField] private Canvas canvasMain;
    [Header("Skin Settings")]
    [SerializeField] private string objName;
    [SerializeField] private int id;
    [SerializeField] private int skinCost;
    [SerializeField] private Text costSkin;
    [SerializeField] private ParticleSystem effectOpenSkin;
    [SerializeField] private Text notificationText;
    [Header("Player Money Settings")]
    [SerializeField] private SaveData saveData;
    [SerializeField] private Text textMoney;

    private void Start()
    {
        HideNotification();
        if (saveData.LoadPlayerData().purchasedSkins != null)
        {
            if (saveData.LoadPlayerData().purchasedSkins.Contains(id))
            {
                costSkin.enabled = false;
            }
        }
    }
    public void ShowMoney()
    {
        int money = saveData.LoadPlayerData().money;
        textMoney.text = (PlayerPrefs.GetInt("LanguageInd") == 0) ? $"Пиво имеется? {money}" : $"Have you some beer? {money}";
    }
    public void SaveIndex()
    {
        PlayerPrefs.SetInt(objName, id);
        canvasShop.enabled = false;
        canvasMain.enabled = true;
    }
    private void HideNotification()
    {
        notificationText.gameObject.SetActive(false);
    }
    public void OpenSkin()
    {
        if (costSkin.enabled)
        {
            if (saveData.LoadPlayerData().money >= skinCost)
            {
                saveData.AddCoins(-skinCost);
                saveData.PurchaseSkin(id);
                SaveIndex();
                ShowMoney();
                costSkin.enabled = false;
                effectOpenSkin.Play();
            }
            else
            {
                notificationText.gameObject.SetActive(true);
                Invoke("HideNotification", 2f);
            }
        }
        else if (!costSkin.enabled)
        {
            SaveIndex();
        }
    }
}
