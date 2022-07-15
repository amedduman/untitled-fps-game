using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using MoreMountains.Feedbacks;

public class DummyEntity : MonoBehaviour, IDamageable
{
    [SerializeField] [Min(0)] float _maxHealth = 100;
    [SerializeField] [Min(0)] float _damageImpactFactor = .1f;
    [SerializeField] Transform _model;
    [Foldout("Feedbacks", true)]
    [SerializeField] MMF_Player _onDamage;
    [SerializeField] MMF_Player _onDeath;
    float _health;
    PlayerEntity _player;
    NavMeshAgent _agent;
    void Start()
    {
        _player = DependencyProvider.Instance.Get<PlayerEntity>();
        _agent = GetComponent<NavMeshAgent>();
        _health = _maxHealth;
        StartCoroutine(Follow());

    }

    IEnumerator Follow()
    {
        while(_health > 0)
        {
            _agent.SetDestination(_player.transform.position);
            yield return new WaitForSecondsRealtime(1);
        }
    }

    public void GetDamage(int damage)
    {
        if(_health <= 0) return;
        _onDamage.PlayFeedbacks();
        _health -= damage;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        if(_health <= 0)
        {
            _onDeath.PlayFeedbacks();
            _agent.enabled = false;
            GetComponent<Collider>().enabled = false;
            transform.DORotate(transform.rotation.eulerAngles + new Vector3(90, 0, 0), .5f).SetEase(Ease.InFlash);
        }
        else
        {
            // if(!DOTween.IsTweening(_model))
            // {
            //     _model.DOPunchPosition(Vector3.up * _damageImpactFactor, _damageImpactFactor);
            // }
        }
    }
}
