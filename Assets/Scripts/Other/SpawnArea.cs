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
        [SerializeField] Collider _col;
        Camera _cam;

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

            _cam = Camera.main;
        }

        public bool IsVisible()
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_cam);

            if (GeometryUtility.TestPlanesAABB(planes, _col.bounds))
            {
                _rend.material.color = Color.green;
                return true;
            }
            else
            {
                _rend.material.color = Color.red;
                return false;
            }
        }

        public void Spawn()
        {
            
        }
    }
}
