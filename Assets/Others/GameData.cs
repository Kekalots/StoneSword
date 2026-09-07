using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public string sceneName = "Level_1";
    public bool restorePosition = true;
    public float posX, posY;
    public float hp;
    public float defense;
    public int coins;
    public string currentWeapon;
    public List<SaveItem> items = new List<SaveItem>();

    public int shopVisitTimes;
    public List<string> shopRemaining = new List<string>();

    public List<string> existingCoins = new List<string>();
    public List<string> openedChests = new List<string>();
}

[System.Serializable]
public class SaveItem
{
    public string itemName;
    public int healValue;
    public int stackCount;
}