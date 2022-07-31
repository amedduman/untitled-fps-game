namespace TheRig.Other
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TheRig.Player;
    using TheRig.GameEvents;
    using ThirdParty.DependencyProvider;

    public class EnemySpawner : MonoBehaviour
    {
        PlayerEntity _player;
        GameEvents _gameEvents;

        [SerializeField] GameValues _gv;
        [SerializeField] float _spawnRange = 1;
        [SerializeField] int _spawnPointCount = 5;
        [SerializeField] bool _drawGizmos = true;
        [SerializeField] List<SpawnData> _spawnDataList = new List<SpawnData>();

        SpawnData _spawnData;
        int _currentGameTimeInSeconds;
        List<Transform> _spawnPoints = new List<Transform>();

        void Awake()
        {
            _gameEvents = DependencyProvider.Instance.Get<GameEvents>();
        }

        void OnEnable()
        {
            _gameEvents.OnGameplaySessionTimeInSecondsChange += UpdateGameTime;
        }

        void OnDisable()
        {
            _gameEvents.OnGameplaySessionTimeInSecondsChange -= UpdateGameTime;
        }

        void Start()
        {
            _player = DependencyProvider.Instance.Get<PlayerEntity>();
            StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
            int elapsedTimeInSeconds = (_gv.GameplaySessionTimeInMinutes * 60) - _currentGameTimeInSeconds;
            for (int i = 0; i < _spawnDataList.Count; i++)
            {
                if (elapsedTimeInSeconds < _spawnDataList[i].DurationInMinutes * 60)
                {

                }
            }

            yield return new WaitForSecondsRealtime(1);
        }

        void UpdateGameTime(int newTimeInSeconds)
        {
            _currentGameTimeInSeconds = newTimeInSeconds;
        }

        [ContextMenu("generate spawners")]
        void CallGenerate()
        {
            GenerateSpawners(_spawnPointCount);
        }

        void GenerateSpawners(int spawnerCount = 5)
        {
            _spawnPoints.Clear();
            for (int i = transform.childCount - 1; i > 0; i--)
            {
                if (Application.isPlaying)
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
            if (!_drawGizmos) return;
            Gizmos.DrawWireSphere(transform.position, _spawnRange);
            Gizmos.color = Color.red;
            foreach (var item in _spawnPoints)
            {
                Gizmos.DrawSphere(item.transform.position, .1f);
            }
        }
    }
}

[System.Serializable]
public struct SpawnData
{
    public int DurationInMinutes;
    public int SpawnPhaseEndTimeInMinutes;
    public int MaxEnemyLimitForSpawnPhase;
    public int RandomRangeMinLimitForSpawnPhaseInSeconds;
    public int RandomRangeMaxLimitForSpawnPhaseInSeconds;
}
