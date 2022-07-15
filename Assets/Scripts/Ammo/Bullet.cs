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
        [SerializeField] int _damage;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public override void FireUp(Vector3 destination, float bulletSpeed, Ease ease, int damage)
        {
            _damage = damage;
            transform.DOMove(destination, bulletSpeed).SetSpeedBased().SetEase(ease).OnComplete(() =>
            {
                _rb.isKinematic = false;
            });
        }

        private void OnCollisionEnter(Collision other)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            _rb.isKinematic = false;
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.GetDamage(_damage);
            }
        }
    }
}
