using System.Collections.Generic;

[System.Serializable]
public struct GameData
{
    public int money;
    public int Language;
    public List<int> purchasedSkins;
    public GameData(int coins,int language, List<int> purchasedSkins)
    {
        this.money = coins;
        this.Language = language;
        this.purchasedSkins = purchasedSkins;
    }
}