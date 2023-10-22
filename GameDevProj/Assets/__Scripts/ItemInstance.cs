
[System.Serializable]
public class ItemInstance
{
    public ItemData itemType;
    public int goldValue;
    public ItemInstance(ItemData itemData)
    {
        itemType = itemData;
        goldValue = itemData.startingGoldValue;
    }
}
