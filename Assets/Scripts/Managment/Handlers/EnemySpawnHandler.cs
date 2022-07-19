namespace TheRig.Handler
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TheRig.Other;

    public class EnemySpawnHandler : MonoBehaviour
    {
        List<SpawnArea> _areas = new List<SpawnArea>();

        void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }

        public void AddArea(SpawnArea area)
        {
            if (area != null)
            {
                _areas.Add(area);
            }
            else
            {
                Debug.LogError("area should not be null");
                Debug.Break();
            }
        }

        IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                foreach (SpawnArea area in _areas)
                {
                    // if (area.IsVisible())
                    // {
                    //     continue;
                    // }

                    // area.Spawn();
                }
                yield return null;
            }
        }
    }
}
