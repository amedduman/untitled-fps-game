namespace TheRig.Gun
{
    using UnityEngine;
    using TheRig.Handler;
    using ThirdParty.DependencyProvider;

    public abstract class Gun : MonoBehaviour
    {
        [field: SerializeField] public Sprite Crosshair { get; private set; }
        protected GunHandler _gunHandler 
        {
            get
            {
                return DependencyProvider.Instance.Get<GunHandler>();
            }
        }

        protected virtual void Start()
        {
            _gunHandler.GunChanged(this);
        }

        public virtual void Reload()
        {

        }

        public virtual void Shoot(Vector3 vel)
        {

        }

        public virtual void ForceReload()
        {

        }
    }
}
