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
    public Sprite icon;
    public ItemType itemType = new ItemType();
    public string itemDesc;

    public bool UseItem()
    {
        if (itemType == ItemType.Berry)
        {
            if (GameManager.Instance.CurrentFood == GameManager.Instance.MaxFood)
            {
                Debug.Log("Cannot eat. Health is full");
                return false;
            }
            else
            {
                Debug.Log("Eat Berry : " + value);
                GameManager.Instance.IncreaseFood(value);
                return true;
            }
        }

        if (itemType == ItemType.Meat)
        {
            if (GameManager.Instance.CurrentFood == GameManager.Instance.MaxFood)
            {
                Debug.Log("Cannot eat. Health is full");
                return false;
            }
            else
            {
                Debug.Log("Eat Meat : " + value);
                GameManager.Instance.IncreaseFood(value);
                return true;
            }
        }
        return false;

        if (itemType == ItemType.Branch)
        {
            Debug.Log("Branch should be draggable");
            return true;
        }

        if (itemType == ItemType.Crystal)
        {
            Debug.Log("Crystal should be draggable");
            return true;
        }

        if (itemType == ItemType.Torch)
        {
            GameManager.Instance.SpawnTorch();
            return true;
        }

        if (itemType == ItemType.Axe)
        {
            GameManager.Instance.SpawnAxe(durability);
            return true;
        }

        if (itemType == ItemType.Spear)
        {
            GameManager.Instance.SpawnSpear(durability);
            return true;
        }
    }

    public enum ItemType
    {
        Potion,
        Torch,
        Axe,
        Spear,
        Berry,
        Meat,
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
            case ItemType.Berry:
            case ItemType.Meat:
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
