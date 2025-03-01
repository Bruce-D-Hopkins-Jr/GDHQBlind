using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankControls : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] TankGun _tankGun;
    [SerializeField] WheelCollider[] _leftWheels;
    [SerializeField] WheelCollider[] _rightWheels;
    [SerializeField] GameObject _turret;
    [SerializeField] GameObject _gun;

    TankControlInputs _tankControlInputs;
    [SerializeField] private TankClass _tankClass;

    [SerializeField] private float _tankTorque = 5000f;
    [SerializeField] private float _tankHealth = 500f;
    [SerializeField] private float _turretSpeed = 10f;
    [SerializeField] private float _barrelSpeed = 10f;

    [SerializeField] UIManager _uiManager;


    [SerializeField] int musicSound;

    private void Start()
    {
        _tankControlInputs = new TankControlInputs();
        _tankControlInputs.Tank.Enable();
       
      

        if (_tankClass != null)
        {
            _tankTorque = _tankClass.tankTorque;
            _tankHealth = _tankClass.tankHealth;
            _turretSpeed = _tankClass.turretSpeed;
            _barrelSpeed = _tankClass.barrelSpeed;

            _rb.mass = _tankClass.mass;

            if (_tankGun != null)
                _tankGun.InitWeapon(_tankClass.weapon, _uiManager);
        }

       
        
    }

     void Update()
    {
       
     if(_tankControlInputs.Tank.LeftTread.WasPressedThisFrame() || _tankControlInputs.Tank.RightTread.WasPressedThisFrame())
       {
        AudioManager.Instance.PlayLongSFX(musicSound);
       }
       else if(_tankControlInputs.Tank.LeftTread.WasReleasedThisFrame() || _tankControlInputs.Tank.RightTread.WasReleasedThisFrame())
       {
        AudioManager.Instance.StopPlayingSFX(musicSound);
       }
    
    /// can make a sound for the turrets rotating
    /// can make a sound for the turret moving
       
    }

    private void FixedUpdate()
    {
        
           // AudioManager.Instance.PlayLongSFX(musicSound);
        float _lTreadInput = _tankControlInputs.Tank.LeftTread.ReadValue<float>();
        float _rTreadInput = _tankControlInputs.Tank.RightTread.ReadValue<float>();

        foreach (var wheel in _leftWheels)
        { 
            wheel.motorTorque = _lTreadInput * _tankTorque * Time.fixedDeltaTime;
        }

        foreach (var wheel in _rightWheels)
        {        
             
            wheel.motorTorque = _rTreadInput * _tankTorque * Time.fixedDeltaTime;
        }
        
        Vector2 _turretVal = _tankControlInputs.Tank.Turret.ReadValue<Vector2>();
  
        _turret.transform.Rotate(transform.up * _turretVal.x * _turretSpeed * Time.fixedDeltaTime, Space.Self);
        _gun.transform.Rotate(-transform.right * _turretVal.y * _barrelSpeed * Time.fixedDeltaTime, Space.Self);
    }
}
