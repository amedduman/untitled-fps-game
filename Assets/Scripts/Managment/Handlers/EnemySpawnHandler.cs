namespace TheRig.Handler
{
    using UnityEngine;
    using TheRig.Other;
    using TheRig.Player;
    using ThirdParty.DependencyProvider;

    public class EnemySpawnHandler : MonoBehaviour
    {
        PlayerEntity _player;

        [SerializeField] GameValues _gv;
        [SerializeField] float _spawnRange = 1;
        [SerializeField] int _spawnPoints = 1;
        [SerializeField] bool _drawGizmos = true;

        void Start()
        {
            _player = DependencyProvider.Instance.Get<PlayerEntity>();
        }

        void OnDrawGizmos()
        {
            if(!_drawGizmos) return;

            PlayerEntity player = FindObjectOfType<PlayerEntity>();
            Gizmos.DrawWireSphere(player.transform.position, _spawnRange);
            if(_spawnPoints <= 0) return;
            float degree = 360 / _spawnPoints;
            Vector3 center = player.transform.position;

            for (int i = 0; i < _spawnPoints; i++)
            {
                var x = Quaternion.AngleAxis(degree * i, Vector3.up);
                Vector3 rotatedVector = Quaternion.AngleAxis(degree * i, Vector3.up) * center;
                if(i == 0)
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawSphere(center + rotatedVector.normalized * _spawnRange, .11f);
            }
        }
    }
}
