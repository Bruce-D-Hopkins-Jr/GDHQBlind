using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image _gunSights;
    [SerializeField] TMP_Text ammoAmountText;
   
    [SerializeField] TMP_Text weaponName;

   [SerializeField] Image _weaponDisplay;
   
   
    public void UpdateAmmo(int min, int max)
    {
        //simple Ammo Display
        ammoAmountText.text = $"{min} / {max} ";
      

    }
    public void DisplayWeaponName(string name)
    {
        weaponName.text = name;
    }
    public void DisplayWeaponType(Sprite activeImage)
    {
       
        _weaponDisplay.sprite = activeImage;
        _weaponDisplay.gameObject.SetActive(true);
     
        // this could be combined into one central method.... 

    }
    public void DisableWeaponType()
    {
     _weaponDisplay.gameObject.SetActive(false);
      _weaponDisplay.sprite = null;

     
    }

    public void SetGunSights(Vector3 uiPosition, float distance)
    {
        _gunSights.enabled = true;
        _gunSights.rectTransform.position = (Vector2)uiPosition;
     
        // We probably should change the size of the sight based on the distance
    }

    internal void DisableGunSights()
    {
        _gunSights.enabled = false;
    }
}
