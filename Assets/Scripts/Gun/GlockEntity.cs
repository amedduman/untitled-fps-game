using UnityEngine;
using UnityEngine.Events;

public class GlockEntity : Gun
{
    [field: SerializeField]
    public int Ammo {get; private set;}
    
    [SerializeField] Ammo _ammoPrefab;
    [SerializeField] Transform _bulletSpawnPointRight;
    [SerializeField] Transform _bulletSpawnPointLeft;
    [Foldout("feedbacks",true)]
    [SerializeField] UnityEvent _rightGunFired;
    [SerializeField] UnityEvent _leftGunFired;

    bool _isRight = true;

    public override void Shoot()
    {
        Transform spawnPoint = null;
        if(_isRight)
        {
            spawnPoint = _bulletSpawnPointRight;
            _rightGunFired.Invoke();
            _isRight = false;
        }
        else
        {
            spawnPoint = _bulletSpawnPointLeft;
            _leftGunFired.Invoke();
            _isRight = true;
        }

        Ammo bullet = Instantiate(_ammoPrefab, 
        spawnPoint.position, 
        Quaternion.identity) as Ammo;

        bullet.FireUp(transform.rotation);
    }
}
