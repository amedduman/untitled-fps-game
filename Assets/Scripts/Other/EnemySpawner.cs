namespace TheRig.Other
{
    using System.Collections.Generic;
    using UnityEngine;
    using TheRig.Player;
    using ThirdParty.DependencyProvider;

    public class EnemySpawner : MonoBehaviour
    {
        PlayerEntity _player;

        [SerializeField] float _spawnRange = 1;
        [SerializeField] int _spawnPointCount = 5;
        [SerializeField] bool _drawGizmos = true;

        List<Transform> _spawnPoints = new List<Transform>();

        void Start()
        {
            _player = DependencyProvider.Instance.Get<PlayerEntity>();
        }

        [ContextMenu("generate spawners")]
        void CallGenerate()
        {
            GenerateSpawners(_spawnPointCount);
        }

        void GenerateSpawners(int spawnerCount  = 5)
        {
            _spawnPoints.Clear();
            for (int i = transform.childCount - 1; i > 0; i--)
            {
                if(Application.isPlaying)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
                else
                {
                    DestroyImmediate(transform.GetChild(i).gameObject);
                }
            }
            float degree = 360 / spawnerCount;
            for (int i = 0; i < spawnerCount; i++)
            {
                GameObject spawner = new GameObject();
                spawner.name = $"spawner {i + 1}";
                spawner.transform.position = transform.position;
                Vector3 pos = spawner.transform.position;
                Vector3 rotatedVector = Quaternion.AngleAxis(degree * i, Vector3.up) * pos;
                pos += rotatedVector.normalized * _spawnRange;
                spawner.transform.position = pos;
                spawner.transform.parent = transform;
                _spawnPoints.Add(spawner.transform);
            }
        }

        void OnDrawGizmos()
        {
            if(!_drawGizmos) return;
            Gizmos.DrawWireSphere(transform.position, _spawnRange);
            Gizmos.color = Color.red;
            foreach (var item in _spawnPoints)
            {
                Gizmos.DrawSphere(item.transform.position, .1f);
            }
        }
    }
}
