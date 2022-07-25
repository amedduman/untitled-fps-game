namespace TheRig.Gun
{
    using UnityEngine;
    using System.Collections;
    using MoreMountains.Feedbacks;
    using TheRig.Ammo;
    using TheRig.Other;
    using ThirdParty.DependencyProvider;

    public class GlockEntity : Gun
    {
        [SerializeField] float _range = 100;
        [SerializeField] int _damage = 10;
        [SerializeField] float _coolDownTime = .2f;
        [SerializeField][Min(0.1f)] float _bulletSpeed = 1;
        [SerializeField] Ammo _ammoPrefab;
        [Foldout("feedbacks", true)]
        [SerializeField] MMF_Player _rightGunFired;
        [SerializeField] MMF_Player _leftGunFired;
        [Space(20)]
        [Foldout("non-designer", false)][SerializeField] LayerMask _layers;
        [Foldout("non-designer", false)][SerializeField] Transform _bulletSpawnPointRight;
        [Foldout("non-designer", false)][SerializeField] Transform _bulletSpawnPointLeft;

        Camera _playerCam;
        bool _isRight = true;
        bool _inCoolDown = false;

        protected override void Start()
        {
            base.Start();
            _playerCam = DependencyProvider.Instance.Get<PlayerCameraEntity>().GetComponent<Camera>();
            _ammo = MaxAmmo;
        }

        IEnumerator CoolDown()
        {
            _inCoolDown = true;
            yield return new WaitForSecondsRealtime(_coolDownTime);
            _inCoolDown = false;
        }

        public override void Shoot(Vector3 vel)
        {
            if(_ammo <= 0) return;

            if (_inCoolDown) return;
            StartCoroutine(CoolDown());

            _ammo -= 1;
            _ammo = Mathf.Clamp(_ammo, 0, MaxAmmo);

            GunHasFired(_ammo);

            if (_ammo == 0)
            {
                Reload();
            }

            Transform spawnPoint = null;
            if (_isRight)
            {
                spawnPoint = _bulletSpawnPointRight;
                _rightGunFired.PlayFeedbacks();
                _isRight = false;
            }
            else
            {
                spawnPoint = _bulletSpawnPointLeft;
                _leftGunFired.PlayFeedbacks();
                _isRight = true;
            }

            Ammo bullet = Instantiate(_ammoPrefab,
            spawnPoint.position,
            Quaternion.identity) as Ammo;

            bullet.transform.position += vel;

            Vector3 bulletDestination = Vector3.zero;

            // RaycastHit hit;
            // if (Physics.Raycast(_playerCam.transform.position,
            //                     _playerCam.transform.forward,
            //                     out hit, _range,
            //                     _layers))
            // {
            //     Debug.DrawRay(_playerCam.transform.position, _playerCam.transform.forward * hit.distance, Color.green, 1);
            //     bulletDestination = hit.point;
            // }
            // else
            // {
            //     Debug.DrawRay(_playerCam.transform.position, _playerCam.transform.forward * _range, Color.red, 1);
            //     bulletDestination = _playerCam.transform.position + _playerCam.transform.forward * _range;
            // }
            bullet.FireUp(bulletDestination, _bulletSpeed, _damage);
            bullet.FireUp(transform.forward, _bulletSpeed, _damage);
        }
    }
}
