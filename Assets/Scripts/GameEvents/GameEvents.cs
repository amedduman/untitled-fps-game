namespace TheRig.GameEvents
{
    using UnityEngine;
    using TheRig.Gun;
    using System;

    public class GameEvents : MonoBehaviour
    {
        public event Action<Gun> OnGunChanged; 

        public void FireGunChanged(Gun gun)
        {
            OnGunChanged?.Invoke(gun);
        }
    }
}
