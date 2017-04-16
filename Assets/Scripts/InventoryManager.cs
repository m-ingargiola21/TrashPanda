using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
    [SerializeField]
    Text[] buttonsTexts;
    [SerializeField]
    GameObject inventoryPanel;

    //[HideInInspector]
    public List<InventoryObject> inventoryObjects;


    //Call at the start of the game into the lower screen and when
    public void UpdateInventoryManager() {
        InventoryObject[] inventory = inventoryObjects.ToArray();

        if (inventory.Length < 1)
            for (int i = 0; i < buttonsTexts.Length; i++)
                buttonsTexts[i].text = "Empty";

        else if(inventory.Length < buttonsTexts.Length)
            for (int i = 0; i < inventory.Length; i++)
                buttonsTexts[i].text = inventory[i].NameForMenuList;

        else if(buttonsTexts.Length < inventory.Length)
            for(int i = 0; i < buttonsTexts.Length; i++)
                inventoryPanel.SetActive(true);
    }


    //DEBUG CODE Input should be on the PlayerController
    private void Update() {
        if (Input.GetButtonDown("Cancel"))
            UpdateInventoryManager();
    }
}
