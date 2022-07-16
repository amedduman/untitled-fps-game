namespace TheRig.GameEvents
{
    using UnityEngine;
    using TheRig.Gun;
    using System;

    public class GameEvents : MonoBehaviour
    {
        public event Action<Gun> OnGunChanged;
        public void InvokeOnGunChanged(Gun gun)
        {
            OnGunChanged?.Invoke(gun);
        }

        public event Action<int> OnGunFire;
        public void InvokeOnGunFire(int remainingAmmo)
        {
            OnGunFire?.Invoke(remainingAmmo);
        }
        
        public event Action<int> OnGunReloading;
        public void InvokeOnGunReloading(int newAmmo)
        {
            OnGunReloading?.Invoke(newAmmo);
        }

        public event Action OnEnemyGetDamaged;
        public void InvokeEnemyGetDamaged()
        {
            OnEnemyGetDamaged?.Invoke();
        }
        
        
    }
}
