using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] [RequireComponent(typeof(SphereCollider))]
public class Bullet :  Ammo
{
    [SerializeField] float _bulletInitialForce = 100;
    Rigidbody _rb;

    public override void FireUp(Quaternion spawnPointRot)
    {
        transform.rotation = spawnPointRot;
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * _bulletInitialForce);
    }
}
