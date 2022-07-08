using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlockEntity : Gun
{
    [field: SerializeField]
    public int Ammo {get; private set;}
    
    [SerializeField] Ammo _ammoPrefab;
    [SerializeField] Transform _bulletSpawnPoint;

    public override void Shoot()
    {
        Ammo bullet = Instantiate(_ammoPrefab, 
        _bulletSpawnPoint.position, 
        Quaternion.identity) as Ammo;

        bullet.FireUp(transform.rotation);
    }
}
