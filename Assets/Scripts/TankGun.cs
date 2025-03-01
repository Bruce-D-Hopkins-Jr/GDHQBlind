using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankGun : MonoBehaviour
{
    [SerializeField] bool _showLogging = false;

    [SerializeField]
    [Tooltip("These are the layers that the raycast will look for")]
    LayerMask _includeLayerMask;
    [SerializeField] Transform _firePoint;

    TankControlInputs _tankControlInputs;
    [SerializeField] float _maxProjectileDistance = 100f;
    [SerializeField] TankWeapon _weapon;

    // this is optional
   [SerializeField] List<TankWeapon> _allWeapons;

   int weaponIndex;

    UIManager _uiManager;

    bool displayWeapon;

    [SerializeField] int ammo;
    [SerializeField] int maxAmmo;
 
        /// if you want to delete the ability to cycle weapons
        /// switch from _allWeapons[weaponIndex] to _weapon for every method and variables
        /// example: ammo = _weapon.currentAmmo
        /// example: uiManager.DisplayName(_weapon.name)
        /// Also, this can work with the "Clip size" variable 
    private void Start()
    {
        _tankControlInputs = new TankControlInputs();
        _tankControlInputs.Tank.Enable();
     

        _tankControlInputs.Tank.Fire.performed += Fire_performed;
        _tankControlInputs.Tank.CycleWeapons.performed += Cycle_performed;
        InitStats();
    }

    private void Cycle_performed(InputAction.CallbackContext context)
    {
       weaponIndex++;
       if(weaponIndex >= _allWeapons.Count)
       {
        weaponIndex = 0;
       }
       _weapon = _allWeapons[weaponIndex];
       _uiManager.UpdateAmmo(_allWeapons[weaponIndex].currentAmmo, _allWeapons[weaponIndex].maxAmmo);
       _uiManager.DisplayWeaponName(_allWeapons[weaponIndex].name);
       _uiManager.DisplayWeaponType(_allWeapons[weaponIndex].DisplayImage);
        
        ammo = _allWeapons[weaponIndex].currentAmmo;
        maxAmmo = _allWeapons[weaponIndex].maxAmmo;
        /// change class
        /// This is optional.
        /// 

        /// Sound Here


       
    }

    private void InitStats()
    {
       
        ammo = _allWeapons[weaponIndex].currentAmmo;
        maxAmmo = _allWeapons[weaponIndex].maxAmmo;
        displayWeapon = true;
        
       _weapon = _allWeapons[0];
       _allWeapons[weaponIndex].Start();
       ammo = maxAmmo;

    }

    public void InitWeapon(TankWeapon weapon, UIManager uiManager)
    {
        _weapon = weapon;
        _uiManager = uiManager;

        //UIManager.UpdateWeaponDisplayImage = _weapon.DisplayImage
        // Handle UI changes?


    }


    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        FireGun();
    }

    private void FireGun()
    {

        if (_weapon == null)
        {
            Debug.LogError("Weapon is null");
            return;
        }

        if (_showLogging)
        {
            Debug.Log(_weapon);
            Debug.Log(_weapon.name);
            Debug.Log(_weapon.GunProjectile);
        }

        if (ammo > 0)
        {
            displayWeapon = true;
            ammo--;
            AudioManager.Instance.PlaySFX(_allWeapons[weaponIndex].soundNum);
          // _allWeapons[weaponIndex].currentAmmo--;
            // Instantiate the projectile
            GameObject projectile = Instantiate(_weapon.GunProjectile, _firePoint.position, _firePoint.rotation);
            // Add force
            projectile.GetComponent<Rigidbody>().AddForce(_firePoint.forward * _weapon.MuzzleVelocity, ForceMode.VelocityChange);
            // Particle effects?
            if (_showLogging)
                Debug.Log("Boom!");
            if (ammo == 0)
            {
                AudioManager.Instance.StopPlayingSFX(_allWeapons[weaponIndex].soundNum);
                displayWeapon = false;

            //while the weapons amount is greater than zero
                if(_allWeapons.Count > 0)
                {
                  
                    _allWeapons.Remove(_weapon);
                    //_weapon = _allWeapons[weaponIndex - 1];
                }
                
            }
        }




    }

    // Not working right now... not sure why, but it's too much trouble.
    private void UpdateGunSights()
    {
        if (_weapon == null)
            return;

        // Find where the gun will fire
        // We'll assume a raycast for now
        RaycastHit hitInfo;
        Debug.DrawRay(_firePoint.position, _firePoint.forward * _maxProjectileDistance);
        if (Physics.Raycast(_firePoint.position, _firePoint.forward, out hitInfo, _maxProjectileDistance, _includeLayerMask))
        {
            Vector3 uiPosition = Camera.main.WorldToScreenPoint(hitInfo.point);
            if (_showLogging)
                Debug.Log($"{hitInfo.point} maps to {uiPosition}");
            // Call the UI Manager to move the gunsights to there.
            if (_uiManager != null)
                _uiManager.SetGunSights(uiPosition, Vector3.Distance(_firePoint.position, hitInfo.point));
            else
                Debug.LogWarning("YO, the UI Manager didn't get set for some reason. It should happen in the InitWeapon()");
        }
        else
        {
            if (_uiManager != null)
                _uiManager.DisableGunSights();
            else
                Debug.LogWarning("YO, the UI Manager didn't get set for some reason. It should happen in the InitWeapon()");
        }
    }

    private void Update()
    {
        UpdateGunSights();
        if (ammo <= 0)
        {
            ammo = 0;

        }

        if (displayWeapon)
        {

            _uiManager.DisplayWeaponType(_allWeapons[weaponIndex].DisplayImage);
            _uiManager.DisplayWeaponName(_allWeapons[weaponIndex].name);

        }
        else if (!displayWeapon)
        {
            _uiManager.DisableWeaponType();
            _uiManager.DisplayWeaponName(string.Empty);
          
            
            //it works, but it is better not to null the weapon
             // _weapon = null;
        }

        
        // _uiManager.UpdateAmmo(_allWeapons[weaponIndex].currentAmmo,_allWeapons[weaponIndex].maxAmmo);
           _uiManager.UpdateAmmo(ammo,maxAmmo);

    }
}
