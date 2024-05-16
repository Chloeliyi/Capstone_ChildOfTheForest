using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Item", menuName ="Item/Create New Item")]

public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public int durability;
    public Sprite icon;
    public ItemType itemType;
    public string itemDesc;

    public enum ItemType
    {
        Potion,
        Weapon,
        Food,
        
    }
}
