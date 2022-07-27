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
        protected GunHandler _gunHandler;
        GameEvents _gameEvents;
        CursorHandler _cursorHandler;

        [field: SerializeField] public Texture2D Crosshair { get; private set; }
        [field: SerializeField] public int MaxAmmo { get; private set; } = 30;
        [SerializeField][Min(0)] float _reloadTime = 10;
        [Foldout("feedbacks", true)]
        [SerializeField] MMF_Player _gunStartReloading;
        [SerializeField] MMF_Player _gunFinishReloading;

        protected int _ammo;

        protected virtual void Start()
        {
            _gameEvents = DependencyProvider.Instance.Get<GameEvents>();
            _gunHandler = DependencyProvider.Instance.Get<GunHandler>();
            _cursorHandler = DependencyProvider.Instance.Get<CursorHandler>();

            _cursorHandler.ChangeCursor(Crosshair);
            _gunHandler.GunChanged(this);
        }

        public virtual void Shoot(Vector3 vel)
        {

        }

        public virtual void ForceReload()
        {
            if (_ammo == 0 || _ammo == MaxAmmo) return;
            _ammo = 0;
            Reload();
        }

        protected void Reload()
        {

            StartCoroutine(CoGunAutOfAmmo()); 

            IEnumerator CoGunAutOfAmmo()
            {
                _gunStartReloading.PlayFeedbacks();

                yield return new WaitForSecondsRealtime(_reloadTime);
                
                ReloadComplete();
            }
        }

        public virtual void ReloadComplete()
        {
            _ammo = MaxAmmo;
            _gameEvents.InvokeGunReloadComplete(MaxAmmo);
            _gunFinishReloading.PlayFeedbacks();
        }

        protected void GunHasFired(int remainingAmmo)
        {
            _gameEvents.InvokeOnGunFire(remainingAmmo);
        }
    }
}
