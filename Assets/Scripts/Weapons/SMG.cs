using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Weapon
{
    // Start is called before the first frame update
    private float fireRate;
    private float distance;
    private int damage;

    private void Start()
    {
        // Asignamos los valores del Scriptable Object
        fireRate = weaponData.FireRate;
        distance = weaponData.Distance;
        damage = weaponData.Damage;
    }
}
