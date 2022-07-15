using UnityEngine;
using System.Collections;

public class GunHandler : MonoBehaviour
{
    GameManager GameManagerInstance
    {
        get
        {
            return DependencyProvider.Instance.Get<GameManager>();
        }
    }
    

    void Awake()
    {
        DependencyProvider.Instance.Register(this);
    }

    public void GunChanged(Gun newGun)
    {
        GameManagerInstance.GunChanged(newGun);
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