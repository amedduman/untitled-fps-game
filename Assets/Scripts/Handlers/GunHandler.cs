using UnityEngine;
using System.Collections;

public class GunHandler : MonoBehaviour
{
    void Awake()
    {
        DependencyProvider.Instance.Register(this);
    }

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
