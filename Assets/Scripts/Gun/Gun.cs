namespace TheRig.Gun
{
    using UnityEngine;
    using System.Collections;
    using MoreMountains.Feedbacks;
    using TheRig.Handler;
    using TheRig.GameEvents;
    using ThirdParty.DependencyProvider;

    public abstract class Gun : MonoBehaviour
    {
        [field: SerializeField] public Sprite Crosshair { get; private set; }
        [field: SerializeField] public int AmmoMax { get; private set; } = 10;
        [SerializeField][Min(0)] float _reloadTime = 10;
        [SerializeField] MMF_Player _gunStartReloading;
        [SerializeField] MMF_Player _gunFinishReloading;


        protected int _ammo;
        protected bool _isReloading = false;



        protected GunHandler _gunHandler 
        {
            get
            {
                return DependencyProvider.Instance.Get<GunHandler>();
            }
        }
        GameEvents _gameEvents
        {
            get
            {
                return DependencyProvider.Instance.Get<GameEvents>();
            }
        }

        protected virtual void Start()
        {
            _gunHandler.GunChanged(this);
        }

        public virtual void ReloadComplete()
        {
            _isReloading = false;
            _gunFinishReloading.PlayFeedbacks();
        }

        public virtual void Shoot(Vector3 vel)
        {

        }

        public virtual void ForceReload()
        {
            if (_isReloading || _ammo == AmmoMax) return;
            Reload(this);
        }

        protected void Reload(Gun gun)
        {
            if(AmmoMax == 0)
            {
                Debug.LogError("max ammo cannot be zero, click this error to see which game object's max ammo is zero", gameObject);
                Debug.Break();
                return;
            }

            _isReloading = true;
            _gunStartReloading.PlayFeedbacks();

            StartCoroutine(CoGunAutOfAmmo()); 

            IEnumerator CoGunAutOfAmmo()
            {
                while(_ammo < AmmoMax)
                {
                    yield return new WaitForSecondsRealtime(_reloadTime / AmmoMax);
                    _ammo += 1;
                    _gameEvents.InvokeOnGunReloading(_ammo);
                }

                gun.ReloadComplete();
            }
        }

        protected void GunHasFired(int remainingAmmo)
        {
            _gameEvents.InvokeOnGunFire(remainingAmmo);
        }
    }
}
