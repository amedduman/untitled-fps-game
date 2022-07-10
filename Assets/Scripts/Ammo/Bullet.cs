using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))] [RequireComponent(typeof(SphereCollider))]
public class Bullet :  Ammo
{
    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public override void FireUp(Vector3 destination, float bulletSpeed, Ease ease)
    {
        transform.DOMove(destination, bulletSpeed).SetSpeedBased().SetEase(ease).OnComplete(()=>
        {
            _rb.isKinematic = false;
        });
    }
}
