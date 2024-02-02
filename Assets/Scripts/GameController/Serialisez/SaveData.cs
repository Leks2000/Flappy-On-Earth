using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [Header("Setting")]
    private string saveFilename = "PlayerCount";
    private string pathToSaveFile;
    private int language;

    private void Awake()
    {
        this.pathToSaveFile = Path.Combine(Application.persistentDataPath, saveFilename);
    }

    [ContextMenu("Show Data")]
    public void AddCoins(int additionalCoins)
    {
        GameData playerData = LoadPlayerData();
        playerData.money += additionalCoins;
        SavePlayerData(playerData);
    }

    public void SavePlayerData(GameData playerData)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(pathToSaveFile);

        binaryFormatter.Serialize(fileStream, playerData);
        fileStream.Close();
    }

    public GameData LoadPlayerData()
    {
        if (File.Exists(pathToSaveFile))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(pathToSaveFile, FileMode.Open);

            GameData playerData = (GameData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            return playerData;
        }
        else
        {
            return new GameData(0,language,new List<int>());
        }
    }
    public void PurchaseSkin(int skinId)
    {
        GameData playerData = LoadPlayerData();
        if (!playerData.purchasedSkins.Contains(skinId))
        {
            playerData.purchasedSkins.Add(skinId);
            SavePlayerData(playerData);
        }
    }
    private void OnApplicationQuit()
    {
        GameData playerData = LoadPlayerData();
        SavePlayerData(playerData);
    }
    public int GetLanguageIndex()
    {
        return language;
    }

    public void SetLanguageIndex(int newLanguageIndex)
    {
        language = newLanguageIndex;
    }
}
