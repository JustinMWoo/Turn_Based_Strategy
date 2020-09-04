using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour
{
    // public Item containedItem

    // Remove this since it will be onteh item
    public string itemName = "Beeg Sword";

    public bool opened = false;
    public GameObject floatingTextPrefab;

    // Called by aninmation event when animation is starting
    void OpeningStart()
    {
        // Trigger floating text
        if (floatingTextPrefab)
        {
            var text = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
            text.GetComponent<TextMeshPro>().text = itemName;
        }
    }

    // Called by animation event after chest is opened
    void Opened()
    {
        // End the units turn
        TurnManager.EndAction(true, true);
        opened = true;
    }


}
