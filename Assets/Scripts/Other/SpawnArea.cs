namespace TheRig.Other
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TheRig.Handler;
    using ThirdParty.DependencyProvider;

    public class SpawnArea : MonoBehaviour
    {
        [SerializeField] Renderer _rend;

        EnemySpawnHandler _spawnHandler
        {
            get
            {
                return DependencyProvider.Instance.Get<EnemySpawnHandler>();
            }
        }

        private void Awake()
        {
            if(_spawnHandler != null)
            {
                _spawnHandler.AddArea(this);
            }
            else
            {
                Debug.LogError("spawn handler should not be null");
                Debug.Break();
            }


        }

        public bool IsVisible()
        {
            bool visible = _rend.isVisible;
            return visible;
        }

        public void Spawn()
        {
            _rend.material.color = Color.green;
        }

        void OnBecameInvisible()
        {
            Debug.Log($"invinsible");
            _rend.material.color = Color.red;
        }
    }
}
