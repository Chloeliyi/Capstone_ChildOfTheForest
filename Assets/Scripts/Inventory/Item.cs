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
    public int quantity;
    public int maxitemQuantity = 3;
    public Sprite icon;
    public ItemType itemType;
    public string itemDesc;

    public enum ItemType
    {
        Potion,
        Torch,
        Axe,
        Spear,
        Food,
        Enemy,
        Workbench,
        Branch,
        Crystal,
        Stone,
        Wall,
    }

    public bool IsStackable() {
        switch (itemType)
        {
            default:
            case ItemType.Potion:
            case ItemType.Spear:
            case ItemType.Food:
            case ItemType.Branch:
            case ItemType.Crystal:
            case ItemType.Stone:
            return true;
            case ItemType.Torch:
            case ItemType.Axe:
            case ItemType.Workbench:
            case ItemType.Wall:
            return false;
        }
    }
}
