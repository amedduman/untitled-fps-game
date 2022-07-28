namespace TheRig.GameEvents
{
    using UnityEngine;
    using TheRig.Gun;
    using System;

    public class GameEvents : MonoBehaviour
    {
        #region Gun
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

        public event Action<int> OnGunReloadComplete;
        public void InvokeGunReloadComplete(int maxAmmo)
        {
            OnGunReloadComplete?.Invoke(maxAmmo);
        }
        #endregion

        #region Player
        public event Action<int> OnPlayerXpChanged;
        public void InvokePlayerXpChanged(int xp)
        {
            OnPlayerXpChanged?.Invoke(xp);
        }

        public event Action<int> OnPlayerHealthChanged;
        public void InvokePlayerHealthChanged(int currentHealth)
        {
            OnPlayerHealthChanged?.Invoke(currentHealth);
        }

        public event Action OnPlayerDeath;
        public void InvokePlayerDeath()
        {
            OnPlayerDeath?.Invoke();
        }
        #endregion
    }
}
