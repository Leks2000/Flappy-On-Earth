using System.Collections.Generic;

[System.Serializable]
public struct GameData
{
    public int money;
    public List<int> purchasedSkins;
    public GameData(int coins, List<int> purchasedSkins)
    {
        this.money = coins;
        this.purchasedSkins = purchasedSkins;
    }
}