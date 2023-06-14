using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapWeapon : MonoBehaviour
{

    public int selectedWeapon = 0; // declares a public in to act as an index for the weapon holder

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // if "1" is pressed set select weapon variable to 0 and run select weapon method which will take the 0 and select the weapon placed in that slot
        {
            selectedWeapon = 0;
            SelectWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))// if "2" is pressed set select weapon variable to 1 and run select weapon method which will take the 1 and select the weapon placed in that slot
        {
            selectedWeapon = 1;
            SelectWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))// if "2" is pressed set select weapon variable to 1 and run select weapon method which will take the 1 and select the weapon placed in that slot
        {
            selectedWeapon = 2;
            SelectWeapon();
        }
    }

    void SelectWeapon()
    { // this method will create a loop to check if the current weapon is the one selected by the player to ensure only one weapon can be used at a time
        int i = 0;
        //foreach allows to loop through contained objects this will take all the transforms which are childs to the current transform (in this case the weapon hold) and loop each one refering to current weapon being inspected as weapon
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true); // takes the value of weapon and sets its state to active
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++; // add 1 to i on each loop
        }
    }
}
