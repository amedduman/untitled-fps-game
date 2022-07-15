namespace TheRig.GameEvents
{
    using UnityEngine;
    using TheRig.Gun;
    using System;

    public class GameEvents : MonoBehaviour
    {
        public event Action<Gun> OnGunChanged;
        public event Action<int> OnGunFire;
        public event Action<int> OnGunReloading;

        public void InvokeOnGunChanged(Gun gun)
        {
            OnGunChanged?.Invoke(gun);
        }
        public void InvokeOnGunFire(int remainingAmmo)
        {
            OnGunFire?.Invoke(remainingAmmo);
        }
        public void InvokeOnGunReloading(int newAmmo)
        {
            OnGunReloading?.Invoke(newAmmo);
        }
    }
}
