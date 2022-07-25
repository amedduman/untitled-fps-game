namespace TheRig.Other
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.AI;
    using MoreMountains.Feedbacks;
    using TheRig.CommonInterfaces;
    using TheRig.Player;
    using TheRig.GameEvents;
    using ThirdParty.DependencyProvider;

    public class DummyEntity : MonoBehaviour, IDamageable
    {
        GameEvents _gameEvents
        {
            get
            {
                return DependencyProvider.Instance.Get<GameEvents>();
            }
        }
        [field: SerializeField] public int XpToDrop
        {
            get;
            private set;
        } = 5;

        [SerializeField][Min(0)] float _maxHealth = 100;
        [SerializeField] float _attackRange = 5;
        [SerializeField] float _attackInterval = 1;
        [SerializeField] int _damage = 4;
        [Foldout("Feedbacks", true)]
        [SerializeField] MMF_Player _onDamage;
        [SerializeField] MMF_Player _onDeath;
        [SerializeField] MMF_Player _onAttack;
        float _health;
        PlayerEntity _player;
        NavMeshAgent _agent;

        void Start()
        {
            _player = DependencyProvider.Instance.Get<PlayerEntity>();
            _agent = GetComponent<NavMeshAgent>();
            if(_agent.stoppingDistance > _attackRange)
            {
                Debug.LogError($"attack range is too small for enemy");
            }
            _health = _maxHealth;
            StartCoroutine(Follow());
            StartCoroutine(CheckForPlayerToAttack());

        }

        IEnumerator Follow()
        {
            while (_health > 0)
            {
                _agent.SetDestination(_player.transform.position);
                yield return new WaitForSecondsRealtime(1);
            }
        }

        IEnumerator CheckForPlayerToAttack()
        {
            while (_health > 0 && _player.CurrentHealth > 0)
            {
                if (Vector3.Distance(transform.position, _player.transform.position) < _attackRange)
                {
                    _player.GetDamage(_damage);
                    _onAttack.PlayFeedbacks();
                }

                yield return new WaitForSecondsRealtime(_attackInterval);
            }

        }

        public void GetDamage(int damage)
        {
            if (_health <= 0) return;
            _onDamage.PlayFeedbacks();
            _gameEvents.InvokeEnemyGetDamaged();
            _health -= damage;
            _health = Mathf.Clamp(_health, 0, _maxHealth);
            if (_health <= 0)
            {
                HandleDeath();
            }
        }

        void HandleDeath()
        {
            _onDeath.PlayFeedbacks();
            _agent.enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }

}