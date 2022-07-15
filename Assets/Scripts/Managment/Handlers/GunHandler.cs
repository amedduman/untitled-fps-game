namespace TheRig.Handler
{
    using UnityEngine;
    using System.Collections;
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
            _gameEvents.FireGunChanged(newGun);
        }

        // what is this method for??
        public void GunHasShoot(int remainingAmmo)
        {

        }

        public void GunAutOfAmmo(Gun gun, float reloadTime)
        {
            StartCoroutine(CoGunAutOfAmmo());

            IEnumerator CoGunAutOfAmmo()
            {
                yield return new WaitForSecondsRealtime(reloadTime);
                gun.Reload();
            }
        }
    }
}
