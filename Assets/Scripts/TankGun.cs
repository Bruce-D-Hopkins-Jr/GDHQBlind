using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class TankGun: MonoBehaviour
{
    [SerializeField] bool _showLogging = false;

    [SerializeField][Tooltip("These are the layers that the raycast will look for")] 
    LayerMask _includeLayerMask;
    [SerializeField] Transform _firePoint;

    TankControlInputs _tankControlInputs;
    [SerializeField] float _maxProjectileDistance = 100f;
    [SerializeField] TankWeapon _weapon;

    UIManager _uiManager;

    private void Start()
    {
        _tankControlInputs = new TankControlInputs();
        _tankControlInputs.Tank.Enable();

        _tankControlInputs.Tank.Fire.performed += Fire_performed;
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

        // Instantiate the projectile
        GameObject projectile = Instantiate(_weapon.GunProjectile, _firePoint.position, _firePoint.rotation);
        // Add force
        projectile.GetComponent<Rigidbody>().AddForce(_firePoint.forward * _weapon.MuzzleVelocity, ForceMode.VelocityChange);
        // Particle effects?
        if(_showLogging)
            Debug.Log("Boom!");
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
            if(_showLogging)
                Debug.Log($"{hitInfo.point} maps to {uiPosition}");
            // Call the UI Manager to move the gunsights to there.
            if(_uiManager!=null)
                _uiManager.SetGunSights(uiPosition, Vector3.Distance(_firePoint.position, hitInfo.point));
            else
                Debug.LogWarning("YO, the UI Manager didn't get set for some reason. It should happen in the InitWeapon()");
        } else
        {
            if(_uiManager!=null)
                _uiManager.DisableGunSights();
            else
                Debug.LogWarning("YO, the UI Manager didn't get set for some reason. It should happen in the InitWeapon()");
        }
    }

    private void Update()
    {
        UpdateGunSights();
    }
}
