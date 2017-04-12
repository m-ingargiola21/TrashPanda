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



    public void OpenInventoryManager() {
        InventoryObject[] inventory = inventoryObjects.ToArray();
        for (int i = 0; i < inventory.Length; i++)
        {

            buttons[i].text = inventory[i].NameForMenuList;
        }

        inventoryPanel.SetActive(true);
    }


    //DEBUG CODE Input should be on the PlayerController
    private void Update() {
        if (Input.GetButtonDown("Cancel"))
            OpenInventoryManager();
    }
}
