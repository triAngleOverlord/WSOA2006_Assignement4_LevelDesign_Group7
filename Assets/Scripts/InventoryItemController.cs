using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    private Item item;

    public Button RemoveButton;
    // Start is called before the first frame update
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
    }

    // Update is called once per frame
    public void AddItem(Item newItem)
    {
        item = newItem;
    }
}
