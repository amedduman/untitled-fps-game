namespace TheRig.Management
{
    using UnityEngine;
    using System;
    using TheRig.Gun;

    public class GameManager : MonoBehaviour
    {
        #region Events
        public event Action<Gun> OnGunChanged;
        #endregion

        public void GunChanged(Gun gun)
        {
            OnGunChanged?.Invoke(gun);
        }
    }
}
