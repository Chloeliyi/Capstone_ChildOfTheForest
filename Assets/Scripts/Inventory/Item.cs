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
                return false;
                Debug.Log("Cannot eat. Health is full");
            }
            else
            {
                return true;
                Debug.Log("Eat Berry : " + value);
                GameManager.Instance.IncreaseFood(value);
            }
        }

        if (itemType == ItemType.Meat)
        {
            if (GameManager.Instance.CurrentFood == GameManager.Instance.MaxFood)
            {
                return false;
                Debug.Log("Cannot eat. Health is full");
            }
            else
            {
                return true;
                Debug.Log("Eat Meat : " + value);
                GameManager.Instance.IncreaseFood(value);
            }
        }

        if (itemType == ItemType.Branch)
        {
            return false;
            Debug.Log("Branch should be draggable");
        }

        if (itemType == ItemType.Crystal)
        {
            return false;
            Debug.Log("Crystal should be draggable");
        }

        if (itemType == ItemType.Torch)
        {
            Debug.Log("Spawn Torch");
            GameManager.Instance.SpawnTorch();
            return true;
        }

        if (itemType == ItemType.Axe)
        {
            return true;
            GameManager.Instance.SpawnAxe(durability);
        }

        if (itemType == ItemType.Spear)
        {
            return true;
            GameManager.Instance.SpawnSpear(durability);
        }
        return false;
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
