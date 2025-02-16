using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class TankGun: MonoBehaviour
{
    [SerializeField] Transform _firePoint;

    TankControlInputs _tankControlInputs;
    [SerializeField] float _maxProjectileDistance = 100f;
    [SerializeField] TankWeapon _weapon;

    [SerializeField] UIManager _uiManager;

    private void Start()
    {
        _tankControlInputs = new TankControlInputs();
        _tankControlInputs.Tank.Enable();

        _tankControlInputs.Tank.Fire.performed += Fire_performed;
    }

    public void SetWeapon(TankWeapon weapon)
    {
        _weapon = weapon;

        // Handle UI changes?
    }

    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        FireGun();
    }

    private void FireGun()
    {
        Debug.Log(_weapon);
        Debug.Log(_weapon.name);
        Debug.Log(_weapon.GunProjectile);
        // Instantiate the projectile
        GameObject projectile = Instantiate(_weapon.GunProjectile, _firePoint.position, _firePoint.rotation);
        // Add force
        projectile.GetComponent<Rigidbody>().AddForce(_firePoint.forward * _weapon.MuzzleVelocity, ForceMode.VelocityChange);
        // Particle effects?
        Debug.Log("Boom!");
    }

    // Not working right now... not sure why, but it's too much trouble.
    private void UpdateGunSights()
    {
        // Find where the gun will fire
        // We'll assume a raycast for now
        RaycastHit hitInfo;
        Debug.DrawRay(_firePoint.position, _firePoint.forward * _maxProjectileDistance);
        if (Physics.Raycast(_firePoint.position, _firePoint.forward, out hitInfo, _maxProjectileDistance))
        {
            Vector3 uiPosition = Camera.main.WorldToScreenPoint(hitInfo.point);
            Debug.Log($"{hitInfo.point} maps to {uiPosition}");
            // Call the UI Manager to move the gunsights to there.
            _uiManager.SetGunSights(uiPosition, Vector3.Distance(_firePoint.position, hitInfo.point));
        } else
        {
            _uiManager.DisableGunSights();
        }
    }

    private void Update()
    {
        UpdateGunSights();
    }
}
