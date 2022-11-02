using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Level
{
    public class RoadPool : MonoBehaviour
    {
        [SerializeField] RoadInfo _info;
        private List<Road> roads = new List<Road>();

        private void Awake()
        {
            DeathingState._Exit += Restart;
        }

        private void Start()
        {
            StartSpawn();
        }

        public void StartSpawn()
        {
            float startPos = -(_info._width * _info._numRoads / 2f) + _info._width / 2f;

            if(_info._numRoads % 2 == 0)
            {
                startPos += _info._width / 2f;
            }

            for(int i = 0; i < _info._numPoolRoads; i++)
            {
                GameObject obj = MonoBehaviour.Instantiate(_info._empty);
                obj.transform.SetParent(this.transform);
                obj.transform.localPosition = new Vector3(0, 0, _info._length * i);
                BoxCollider col = obj.GetComponent<BoxCollider>();
                col.size = new Vector3(_info._width * _info._numRoads, 2, _info._length);
                col.center = new Vector3(0, 0, 0);
                roads.Add(new Road(obj.transform, _info, startPos));

                obj.GetComponent<RoadManager>()._Trigger += roads[i].Disable;
            }
            for(int i = 1; i < _info._numPoolRoads; i++)
            {
                roads[i].Activate();
            }
        }

        private void Restart()
        {
            Debug.Log("1");
            for(int i = 0; i < roads.Count; i++)
            {
                roads[i].Restart(_info._length * i);
            }
            for (int i = 1; i < roads.Count; i++)
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

        private void OnDestroy()
        {
            DeathingState._Exit -= Restart;
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
            _position.localPosition += new Vector3(0, 0, _inf._length * _inf._numPoolRoads);
            Activate();
        }
        public void Restart(float newPos)
        {
            foreach (var obstacle in _obstaclesPool)
            {
                obstacle.Deactivate();
            }
            _position.localPosition = new Vector3(0, 0, newPos);
        }

        public void Destroy()
        {
            _position.GetComponent<RoadManager>()._Trigger -= Disable;
        }
    }
}

