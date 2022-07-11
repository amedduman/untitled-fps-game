using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DummyEntity : MonoBehaviour
{
    [SerializeField] [Min(0)] float _health = 100;
    PlayerEntity _player;
    NavMeshAgent _agent;
    void Start()
    {
        _player = DependencyProvider.Instance.Get<PlayerEntity>();
        _agent = GetComponent<NavMeshAgent>();
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
}
