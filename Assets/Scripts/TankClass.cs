using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TankClass", menuName = "Tank/TankClass", order = 0)]
public class TankClass : ScriptableObject
{
    public TankWeapon weapon;
    public float mass = 50000f;
    public float tankSpeed = 15f;
    public float tankTorque = 5000f;
    public float turretSpeed = 10f;
    public float barrelSpeed = 10f;
    public float tankHealth = 500f;
}
