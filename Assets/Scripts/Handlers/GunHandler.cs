namespace TheRig.Handler
{
    using UnityEngine;
    using TheRig.Gun;
    using TheRig.GameEvents;
    using ThirdParty.DependencyProvider;

    public class GunHandler : MonoBehaviour
    {
        GameEvents _gameEvents
        {
            get
            {
                return DependencyProvider.Instance.Get<GameEvents>();
            }
        }

        public void GunChanged(Gun newGun)
        {
            _gameEvents.InvokeOnGunChanged(newGun);
        }
    }
}
