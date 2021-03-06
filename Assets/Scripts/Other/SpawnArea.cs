namespace TheRig.Other
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TheRig.Handler;
    using ThirdParty.DependencyProvider;

    public class SpawnArea : MonoBehaviour
    {
        [SerializeField] DummyEntity _dummyPrefab;

        EnemySpawnHandler _spawnHandler
        {
            get
            {
                return DependencyProvider.Instance.Get<EnemySpawnHandler>();
            }
        }

        private void Awake()
        {
            if (_spawnHandler != null)
            {
                _spawnHandler.AddArea(this);
            }
            else
            {
                Debug.LogError("spawn handler should not be null");
                Debug.Break();
            }
        }

        public void Spawn()
        {
            Instantiate(_dummyPrefab, transform.position, transform.rotation, transform);
        }
    }
}
