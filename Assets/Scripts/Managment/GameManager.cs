using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region Events
    public event Action<Gun> OnGunChanged;
    #endregion

    private void Awake()
    {
        DependencyProvider.Instance.Register(this);
    }

    public void GunChanged(Gun gun)
    {
        OnGunChanged?.Invoke(gun);
    }
}
