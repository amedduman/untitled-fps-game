namespace TheRig.Gun
{
    using UnityEngine;
    using UnityEngine.Events;
    using DG.Tweening;
    using System.Collections;
    using TheRig.Ammo;
    using TheRig.Handler;
    using TheRig.Other;

    public class GlockEntity : Gun
    {
        [field: SerializeField] int _ammoMax { get; set; } = 10;
        [SerializeField] float _range = 100;
        [SerializeField] int _damage = 10;
        [SerializeField][Min(0)] float _reloadTime = 10;
        [SerializeField] float _coolDownTime = .2f;
        [SerializeField][Min(0.1f)] float _bulletSpeed = 1;
        [SerializeField] Ease _bulletTweenEase = Ease.Linear;
        [SerializeField] Ammo _ammoPrefab;
        [Foldout("feedbacks", true)]
        [SerializeField] UnityEvent _rightGunFired;
        [SerializeField] UnityEvent _leftGunFired;
        [Foldout("non-designer", false)][SerializeField] LayerMask _layers;
        [Foldout("non-designer", false)][SerializeField] Transform _bulletSpawnPointRight;
        [Foldout("non-designer", false)][SerializeField] Transform _bulletSpawnPointLeft;

        Camera _playerCam;
        bool _isRight = true;
        bool _inCoolDown = false;
        int _ammo;

        protected override void Start()
        {
            base.Start();
            _playerCam = DependencyProvider.Instance.Get<PlayerCameraEntity>().GetComponent<Camera>();
            _gunHandler = DependencyProvider.Instance.Get<GunHandler>();
            _ammo = _ammoMax;
        }

        IEnumerator CoolDown()
        {
            _inCoolDown = true;
            yield return new WaitForSecondsRealtime(_coolDownTime);
            _inCoolDown = false;
        }

        public override void ForceReload()
        {
            if (_ammo == 0) return;
            _ammo = 0;
            transform.GetChild(0).transform.DOLocalRotate(Vector3.right * 90, 2);
            _gunHandler.GunAutOfAmmo(this, _reloadTime);
        }

        public override void Reload()
        {
            transform.GetChild(0).transform.DOLocalRotate(Vector3.right * 0, .5f).OnComplete(() =>
            {
                _ammo = _ammoMax;
            });
        }

        public override void Shoot(Vector3 vel)
        {
            if (_ammo <= 0) return;

            if (_inCoolDown) return;
            StartCoroutine(CoolDown());

            _ammo -= 1;
            _ammo = Mathf.Clamp(_ammo, 0, _ammoMax);
            _gunHandler.GunHasShoot(_ammo);
            if (_ammo == 0)
            {
                transform.GetChild(0).transform.DOLocalRotate(Vector3.right * 90, 2);
                _gunHandler.GunAutOfAmmo(this, _reloadTime);
            }

            Transform spawnPoint = null;
            if (_isRight)
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
            bullet.FireUp(bulletDestination, _bulletSpeed, ease, _damage);
        }
    }
}
