namespace TheRig.Ammo
{
    using UnityEngine;
    using DG.Tweening;

    public abstract class Ammo : MonoBehaviour
    {
        public virtual void FireUp(Vector3 destination, float bulletSpeed, int damage)
        {

        }
    }
}
