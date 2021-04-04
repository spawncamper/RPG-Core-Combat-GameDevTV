using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingWrapper : MonoBehaviour
{
    const string defaultSaveFile = "save";

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.G))
        {
            //           GetComponent<SavingSystem>().Save(defaultSaveFile);
            print("Ctrl + G");

            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }    
    }
}
