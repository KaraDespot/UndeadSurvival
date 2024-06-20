using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgradeManager : MonoBehaviour
{
    public Text upgradeText;
    public GameObject[] weaponUpgrades;
    public string[] statUpgrades;

    private bool[] weaponObtained;
    private bool statUpgraded;

    private ActiveWeapon activeWeapon;

    void Start()
    {
        activeWeapon = FindObjectOfType<ActiveWeapon>();
        weaponObtained = new bool[weaponUpgrades.Length]; 
    }

    public void ObtainWeapon()
    {
        Debug.Log("Im working!");
        int randomIndex = Random.Range(0, weaponUpgrades.Length);
        if (!weaponObtained[randomIndex])
        {
            weaponObtained[randomIndex] = true;
            upgradeText.text = "New weapon available!";
            StartCoroutine(ClearUpgradeText(3f));
        }
    }

    public void ObtainStatUpgrade()
    {
        if (!statUpgraded)
        {
            int randomIndex = Random.Range(0, statUpgrades.Length);
            statUpgraded = true;
            upgradeText.text = statUpgrades[randomIndex];
            StartCoroutine(ClearUpgradeText(3f));
        }
    }

    IEnumerator ClearUpgradeText(float delay)
    {
        yield return new WaitForSeconds(delay);
        upgradeText.text = "";
    }

    public bool IsWeaponObtained(int index)
    {
        if (index >= 0 && index < weaponObtained.Length)
        {
            return weaponObtained[index];
        }
        return false;
    }
}


