namespace TheRig.Handler
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TheRig.Other;
    using TheRig.Player;

    public class EnemySpawnHandler : MonoBehaviour
    {
        SpawnAreaDetector _detector;
        [SerializeField] float _spawnIntervals; // game values?
        List<SpawnArea> _areas = new List<SpawnArea>();
        List<SpawnArea> _visibleAreas = new List<SpawnArea>();

        public void Init(SpawnAreaDetector detector)
        {
            _detector = detector;
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
                _visibleAreas.Clear();
                if(_detector == null) Debug.Log($"detector null");
                _visibleAreas = _detector.GetVisibleSpawnAreas();

                foreach (SpawnArea area in _areas)
                {
                    if (_visibleAreas.Contains(area))
                    {
                        continue;
                    }

                    area.Spawn();
                }

                yield return new WaitForSecondsRealtime(_spawnIntervals);
            }
        }
    }
}
