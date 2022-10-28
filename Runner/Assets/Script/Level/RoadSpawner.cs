using Mono.Reflection;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

            if(inf._numRoads % 2 == 0)
            {
                startPos += inf._width / 2f;
            }

            for(int i = 0; i < inf._numPoolRoads; i++)
            {
                GameObject obj = MonoBehaviour.Instantiate(inf._empty);
                obj.transform.SetParent(parent);
                obj.transform.localPosition = new Vector3(0, 0, inf._length * i);
                BoxCollider col = obj.GetComponent<BoxCollider>();
                col.size = new Vector3(inf._width * inf._numRoads, 2, inf._length);
                col.center = new Vector3(0, 0, 0);
                roads.Add(new Road(obj.transform, inf, startPos));

                obj.GetComponent<RoadManager>()._Trigger += roads[i].Disable;
            }
            for(int i = 1; i < inf._numPoolRoads; i++)
            {
                roads[i].Activate();
            }
        }
        public void Destroy()
        {
            for(int i = 0; i < roads.Count; i++)
            {
                roads[i].Destroy();
            }
        }
    }

    public class Road
    {
        private Transform _position;
        private List<ObstaclesPool> _obstaclesPool = new List<ObstaclesPool>();

        private RoadInfo _inf;
        public Road(Transform parent, RoadInfo inf, float startPos)
        {
            _inf = inf;
            _position = parent;
            for (int i = 0; i < inf._numRoads; i++)
            {
                GameObject obj = MonoBehaviour.Instantiate(inf._roadPrefab);
                obj.transform.SetParent(parent);
                obj.transform.localPosition = new Vector3(startPos + inf._width * i, 0, 0);
                _obstaclesPool.Add(new ObstaclesPool(obj.transform, inf));
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

        public void Activate()
        {
            foreach (var obstacle in _obstaclesPool)
            {
                obstacle.Activate();
            }
        }
        public void Disable()
        {
            //Debug.Log(_position.localPosition);
            _position.localPosition += new Vector3(0, 0, _inf._length * _inf._numPoolRoads);
            Activate();
        }
        public void Destroy()
        {
            _position.GetComponent<RoadManager>()._Trigger -= Disable;
        }
    }
}

