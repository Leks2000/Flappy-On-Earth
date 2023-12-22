using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkinChanger : MonoBehaviour
{
    [Header("Shop Settings")]
    [SerializeField] private Canvas CanvasShop;
    [SerializeField] private Canvas CanvasMain;
    [Header("Skin Settings")]
    [SerializeField] private string objName;
    [SerializeField] private int id;
    [SerializeField] private int skinCost;
    [SerializeField] private Text CostSkin;
    [Header("Player Money Settings")]
    [SerializeField] private SaveData saveData;
    [SerializeField] private Text textMoney;
    private void Start()
    {
        if (saveData.LoadPlayerData().purchasedSkins != null)
        {
            if (saveData.LoadPlayerData().purchasedSkins.Contains(id))
            {
                CostSkin.enabled = false;
            }
        }
    }
    public void ShowMoney()
    {
        int money = saveData.LoadPlayerData().money;
        textMoney.text = $"Кружок имеется?: {money.ToString()}";
    }
    public void SaveIndex()
    {
        PlayerPrefs.SetInt(objName, id);
    }
    public void OpenSkin()
    {
        if (CostSkin.enabled)
        {
            if (saveData.LoadPlayerData().money >= skinCost)
            {
                saveData.AddCoins(-skinCost);
                saveData.PurchaseSkin(id);
                SaveIndex();
                ShowMoney();
                CostSkin.enabled = false;
                Debug.Log("Skin opened!");
                //добавить визуальное уведомление об открытии скина!
            }
            else
            {
                Debug.Log("Not enough money to open the skin!");
                //добавить визуальное уведомление о нехватке денег!
            }
        }
        else if (CostSkin == null || !CostSkin.enabled)
        {
            SaveIndex();
            CanvasShop.enabled = false;
            CanvasMain.enabled = true;
        }
    }
}
