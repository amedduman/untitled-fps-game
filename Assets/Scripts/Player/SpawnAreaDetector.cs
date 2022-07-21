namespace TheRig.Player
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TheRig.Other;
    using TheRig.Handler;
    using ThirdParty.DependencyProvider;

    public class SpawnAreaDetector : MonoBehaviour
    {
        EnemySpawnHandler _spawnHandler
        {
            get
            {
                return DependencyProvider.Instance.Get<EnemySpawnHandler>();
            }
        }

        [SerializeField][Min(10)] int _visualAngle = 40;
        [Foldout("non-designer", false)][SerializeField] LayerMask _allLayersToCheck;
        [Foldout("non-designer", false)][SerializeField] LayerMask _wantedLayer;
        List<SpawnArea> _visibleSpawnAreas = new List<SpawnArea>();
        [SerializeField] float _maxDistanceForRay = 2000;

        void Start()
        {
            _spawnHandler.Init(this);
        }

        public List<SpawnArea> GetVisibleSpawnAreas()
        {
            _visibleSpawnAreas.Clear();

            for (int i = -_visualAngle; i < _visualAngle; i++)
            {
                transform.localRotation = Quaternion.Euler(0, i, 0);

                RaycastHit hit;
                if (Physics.Raycast(transform.position,
                                transform.forward,
                                out hit, _maxDistanceForRay,
                                _allLayersToCheck))
                {
                    if (IsInLayerMask(hit.transform.gameObject, _wantedLayer))
                    {
                        if(hit.transform.gameObject.TryGetComponent(out SpawnArea spawnArea))
                        {
                            _visibleSpawnAreas.Add(spawnArea);
                        }
                        #if UNITY_EDITOR
                        Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green, 1);
                        #endif
                    }
                    else
                    {
                        #if UNITY_EDITOR
                        Debug.DrawRay(transform.position, transform.forward * _maxDistanceForRay, Color.red, 1);
                        #endif
                    }
                }
            }

            return _visibleSpawnAreas;
        }

        bool IsInLayerMask(GameObject obj, LayerMask layerMask)
        {
            return ((layerMask.value & (1 << obj.layer)) > 0);
        }
    }

}