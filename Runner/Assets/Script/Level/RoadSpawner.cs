using Mono.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class RoadSpawner : MonoBehaviour
    {
        [SerializeField] RoadInfo _info;
        private RoadPool _pool = new RoadPool();

        private void Start()
        {
            _pool.StartSpawn(_info, this.transform);
        }
    }


    public class RoadPool
    {
        private List<Road> roads = new List<Road>();

        public void StartSpawn(RoadInfo inf, Transform parent)
        {
            float startPos = -(inf._width * inf._numRoads / 2f) + inf._width / 2f;
            float obstShift = inf._length / inf._numObstacles;

            if(inf._numRoads % 2 == 0)
            {
                startPos += inf._width / 2f;
            }

            for(int i = 0; i < inf._numPoolRoads; i++)
            {
                GameObject obj = MonoBehaviour.Instantiate(inf._empty);
                obj.transform.SetParent(parent);
                obj.transform.localPosition = new Vector3(0, 0, inf._length * i);
                roads.Add(new Road(obj.transform, inf, startPos, obstShift));
            }
        }
    }

    public class Road
    {
        private Transform _position;
        private List<ObstaclesPool> _obstaclesPool = new List<ObstaclesPool>();

        public System.Action _spawnerObstacles;

        public Road(Transform parent, RoadInfo inf, float startPos, float obstShift)
        {
            for(int i = 0; i < inf._numRoads; i++)
            {
                GameObject obj = MonoBehaviour.Instantiate(inf._roadPrefab);
                obj.transform.SetParent(parent);
                obj.transform.localPosition = new Vector3(startPos + inf._width * i, 0, 0);
                _obstaclesPool.Add(new ObstaclesPool(obj.transform, inf, obstShift));
            }

            if(inf._roadPrefab2 != null)
            {
                for (int i = 0; i <= inf._numRoads; i++)
                {
                    GameObject obj = MonoBehaviour.Instantiate(inf._roadPrefab2);
                    obj.transform.SetParent(parent);
                    obj.transform.localPosition = new Vector3(startPos - (inf._width / 2f) + inf._width * i, 0, 0);
                }
            }
        }
    }
}

