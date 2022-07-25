namespace TheRig.Ammo
{
    using UnityEngine;
    using DG.Tweening;
    using TheRig.CommonInterfaces;

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Bullet : Ammo
    {
        Rigidbody _rb;
        int _damage;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public override void FireUp(Vector3 destination, float bulletSpeed, int damage)
        {
            _damage = damage;
            // Vector3 dir = (destination - transform.position).normalized;
            // _rb.AddForce(dir * bulletSpeed, ForceMode.VelocityChange);
            _rb.AddForce(destination * bulletSpeed, ForceMode.VelocityChange);
        }

        private void OnCollisionEnter(Collision other)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.GetDamage(_damage);
            }
            Destroy(gameObject);
        }
    }
}
