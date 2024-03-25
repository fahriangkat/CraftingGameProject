using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    public static InventoryManager Instance;

    public List<Item> Items = new List<Item>();
    public Transform ItemContent;
    public GameObject InventoryItem;
    public Toggle EnableRemove;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
        Debug.Log("Item added to inventory: " + item.itemName);
        // After adding an item, update the inventory UI
        ListItem();
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
        // After removing an item, update the inventory UI
        ListItem();
    }

   public void ListItem()
{
    // Check if ItemContent is null
    if (ItemContent == null)
    {
        Debug.LogWarning("ItemContent is not assigned!");
        return;
    }

    // Clear existing UI elements
    foreach (Transform item in ItemContent)
    {
        Destroy(item.gameObject);
    }

    // Instantiate UI elements for each item in the inventory
    foreach (var item in Items)
    {
        // Null-check InventoryItem prefab
        if (InventoryItem == null)
        {
            Debug.LogWarning("InventoryItem prefab is not assigned!");
            return;
        }

        // Instantiate UI element with a valid parent (ItemContent)
        GameObject obj = Instantiate(InventoryItem, ItemContent);
        if (obj == null)
        {
            Debug.LogWarning("Failed to instantiate InventoryItem prefab!");
            return;
        }

        // Get references to UI components and update them
        var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
        var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

        itemName.text = item.itemName;
        itemIcon.sprite = item.icon;
    }

        // Update the visibility of remove buttons
        EnableItemsRemove();
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove == null)
        {
            Debug.LogWarning("EnableRemove is not assigned!");
            return;
        }

        foreach (Transform item in ItemContent)
        {
            GameObject removeButton = item.Find("RemoveButton")?.gameObject;
            if (removeButton != null)
            {
                removeButton.SetActive(EnableRemove.isOn);
            }
        }
    }
}
