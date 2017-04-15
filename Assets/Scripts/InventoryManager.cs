using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
    [SerializeField]
    Text[] buttons;
    [SerializeField]
    GameObject inventoryPanel;

    //[HideInInspector]
    public List<InventoryObject> inventoryObjects;


    //Call at the start of the game into the lower screen and when
    public void OpenInventoryManager() {
        InventoryObject[] inventory = inventoryObjects.ToArray();

        if (inventory.Length < 1)
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].text = "Empty";

        else if(inventory.Length < buttons.Length)
            for (int i = 0; i < inventory.Length; i++)
                buttons[i].text = inventory[i].NameForMenuList;

        else if(buttons.Length < inventory.Length)
            for(int i = 0; i < buttons.Length; i++)
                inventoryPanel.SetActive(true);
    }


    //DEBUG CODE Input should be on the PlayerController
    private void Update() {
        if (Input.GetButtonDown("Cancel"))
            OpenInventoryManager();
    }
}
