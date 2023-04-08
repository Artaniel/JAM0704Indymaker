using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    private void Update()
    {
         if(Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
        }
         if(menu == true && Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(false);
        }
    }
}
