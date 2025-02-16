using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class TankControls : MonoBehaviour
{
    [SerializeField] WheelCollider[] _leftWheels;
    [SerializeField] WheelCollider[] _rightWheels;
    [SerializeField] GameObject _turret;
    [SerializeField] GameObject _gun;

    TankControlInputs _tankControlInputs;
    [SerializeField] private float _tankTorque = 5000f;
    [SerializeField] private float _tankHealth = 500f;
    [SerializeField] private float _turretSpeed = 10f;
    [SerializeField] private float _barrelSpeed = 10f;

    [SerializeField] UIManager _uiManager;

    private void Start()
    {
        _tankControlInputs = new TankControlInputs();
        _tankControlInputs.Tank.Enable();

    }

    private void FixedUpdate()
    {
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
