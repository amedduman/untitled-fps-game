using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlockEntity : MonoBehaviour, IGun
{
    [field: SerializeField]
    public GunProperties MyProperties {get; private set;}
    [SerializeField] Transform _bulletSpawnPoint;

    public void Shoot()
    {
        GameObject bullet = Instantiate(MyProperties.AmmoPrefab, 
        _bulletSpawnPoint.position, 
        Quaternion.identity);

        if(bullet.TryGetComponent<IAmmo>(out IAmmo ammo))
        {
            ammo.FireUp(transform.rotation);
        }
    }
}
