namespace TheRig.Player
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TheRig.Other;

    public class SpawnAreaDetector : MonoBehaviour
    {
        [SerializeField][Min(10)] int _visualAngle = 40;
        [Foldout("non-designer", false)][SerializeField] LayerMask _allLayersToCheck;
        [Foldout("non-designer", false)][SerializeField] LayerMask _wantedLayer;
        List<SpawnArea> _visibleSpawnAreas = new List<SpawnArea>();
        [SerializeField] float _maxDistanceForRay = 2000;

        void FixedUpdate()
        {
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
                        Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, transform.forward * _maxDistanceForRay, Color.red);
                    }
                }
                // {
                //     Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
                // }
                // else
                // {
                //     Debug.DrawRay(transform.position, transform.forward * _maxDistanceForRay, Color.red);
                // }
            }
        }

        public bool IsInLayerMask(GameObject obj, LayerMask layerMask)
        {
            return ((layerMask.value & (1 << obj.layer)) > 0);
        }
    }

}