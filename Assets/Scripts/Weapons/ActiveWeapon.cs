using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public GameObject[] weapons; //creates a massive to switch between different weapons
    public GameObject[] weaponsImages;
    private int currentWeaponIndex = 0;

    private bool weapon1Available = false;
    private bool weapon2Available = false;
    private bool weapon3Available = false;

    void Start()
    {
        // turns off all weapons except the basic one
        for (int i = 1; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        // turns on basic weapon
        SwitchWeapon(currentWeaponIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0); // showel
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weapon1Available == true)
        {
                SwitchWeapon(1); // pitchfork
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && weapon2Available == true)
        {
                SwitchWeapon(2); // rifle
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && weapon3Available == true)
        {
                SwitchWeapon(3); // assault rifle
        }
    }

    void SwitchWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        weapons[index].SetActive(true);
        currentWeaponIndex = index;
    }

    public void ObtainWeapon()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, 3); 

            if (randomIndex == 0 && weapon1Available == false)
            {
                weapon1Available = true;
                weaponsImages[1].SetActive(true);
                break; 
            }
            else if (randomIndex == 1 && weapon2Available == false)
            {
                Debug.Log("2 Im working!");
                weaponsImages[2].SetActive(true);
                weapon2Available = true;
                break; 
            }
            else if (randomIndex == 2 && weapon3Available == false)
            {
                weaponsImages[3].SetActive(true);
                weapon3Available = true;
                break; 
            }
        }
    }
}

