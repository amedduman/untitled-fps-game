using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class GlockEntity : Gun
{
    [SerializeField] LayerMask _layers;
    [field: SerializeField] int Ammo {get; set;}
    [SerializeField] float _range = 100;
    [SerializeField] [Min(0.1f)] float _bulletSpeed = 1; 
    [SerializeField] Ease _bulletTweenEase = Ease.Linear;
    [SerializeField] Ammo _ammoPrefab;
    [SerializeField] Transform _bulletSpawnPointRight;
    [SerializeField] Transform _bulletSpawnPointLeft;
    [Foldout("feedbacks",true)]
    [SerializeField] UnityEvent _rightGunFired;
    [SerializeField] UnityEvent _leftGunFired;

    Camera _playerCam;
    bool _isRight = true;

    private void Start()
    {
        _playerCam = DependencyProvider.Instance.Get<PlayerCameraEntity>().GetComponent<Camera>();
    }

    public override void Shoot(Vector3 vel)
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

        //Debug.Break();

        bullet.transform.position += vel;

        Vector3 bulletDestination = Vector3.zero;
        Ease ease = Ease.Linear;

        RaycastHit hit;
        if (Physics.Raycast(_playerCam.transform.position,
                            _playerCam.transform.forward,
                            out hit, _range,
                            _layers))
        {
            Debug.DrawRay(_playerCam.transform.position, _playerCam.transform.forward * hit.distance, Color.green, 1);
            bulletDestination = hit.point;
            ease = _bulletTweenEase;
        }
        else
        {
            Debug.DrawRay(_playerCam.transform.position, _playerCam.transform.forward * _range, Color.red, 1);
            bulletDestination = _playerCam.transform.position + _playerCam.transform.forward * _range;
            ease = Ease.Linear;
        }
        bullet.FireUp(bulletDestination, _bulletSpeed, ease);
    }
}
