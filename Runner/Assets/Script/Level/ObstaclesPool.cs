using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class ObstaclesPool
    {
        private ObstaclesInfo _obstaclesInfo;
        private GameObject _empty;
        private Transform _parent;
        private float _length;
        private int _minNum;
        private int _maxNum;

        private List<Obstacle> _obstacles = new List<Obstacle>();

        public ObstaclesPool(Transform parent, RoadInfo info)
        {
            _obstaclesInfo = info._obstaclesInfo;
            _parent = parent;
            _empty = info._obstaclesInfo._empty;
            _length = info._length;
            _minNum = _obstaclesInfo._minNumObstacles;
            _maxNum = _obstaclesInfo._maxNumObstacles;
            Spawn();
        }

        private void Spawn()
        {
            for(int i = 0; i < _maxNum; i++)
            {
                GameObject obj = MonoBehaviour.Instantiate(_empty);
                obj.transform.SetParent(_parent);
                obj.transform.localPosition = new Vector3(0, 0, 0);
                obj.SetActive(false);
                _obstacles.Add(new Obstacle(obj, _obstaclesInfo));
            }
        }

        public void Activate()
        {
            int rand = Random.Range(_minNum, _maxNum + 1);
            float shift = _length / rand;

            for(int i = 0; i < rand; i++)
            {
                _obstacles[i].Disable();
                _obstacles[i].Activate(new Vector3(0, 0, ((i * shift) - (_length / 2f) + (shift / 2f)) / _parent.transform.localScale.z));
            }
            for(int i = rand; i < _obstacles.Count; i++)
            {
                _obstacles[i]._parent.SetActive(false);
            }
        }


        private class Obstacle
        {
            public GameObject _parent;
            public List<GameObject> _topObstacles = new List<GameObject>();
            public List<GameObject> _bottomObstacles = new List<GameObject>();
            public List<GameObject> _fullObstacles = new List<GameObject>();
            private int[] _value;

            public Obstacle(GameObject parent, ObstaclesInfo obstaclesInfo)
            {
                _value = obstaclesInfo._value;
                _parent = parent;
                Create(obstaclesInfo);
            }

            public void Create(ObstaclesInfo obstaclesInfo)
            {
                foreach(GameObject obj in obstaclesInfo._topObstacles)
                {
                    GameObject newObj = MonoBehaviour.Instantiate(obj);
                    newObj.transform.SetParent(_parent.transform);
                    newObj.transform.localPosition = new Vector3(0, 0, 0);
                    newObj.SetActive(false);
                    _topObstacles.Add(newObj);
                }
                foreach (GameObject obj in obstaclesInfo._bottomObstacles)
                {
                    GameObject newObj = MonoBehaviour.Instantiate(obj);
                    newObj.transform.SetParent(_parent.transform);
                    newObj.transform.localPosition = new Vector3(0, 0, 0);
                    newObj.SetActive(false);
                    _bottomObstacles.Add(newObj);
                }
                foreach (GameObject obj in obstaclesInfo._fullObstacles)
                {
                    GameObject newObj = MonoBehaviour.Instantiate(obj);
                    newObj.transform.SetParent(_parent.transform);
                    newObj.transform.localPosition = new Vector3(0, 0, 0);
                    newObj.SetActive(false);
                    _fullObstacles.Add(newObj);
                }
            }

            public void Activate(Vector3 newPos)
            {
                _parent.transform.localPosition = newPos;
                _parent.SetActive(true);

                int maxRand = 0;

                foreach(int v in _value)
                {
                    maxRand += v;
                }

                int rand = Random.Range(0, maxRand);

                if(rand >= 0 && rand < _value[0])
                {
                    _parent.SetActive(false);
                    return;
                }
                rand -= _value[0];
                if(rand >= 0 && rand < _value[1])
                {
                    Variant(_topObstacles);
                    return;
                }
                rand -= _value[1];
                if (rand >= 0 && rand < _value[2])
                {
                    Variant(_bottomObstacles);
                    return;
                }
                rand -= _value[2];
                if (rand >= 0 && rand < _value[3])
                {
                    Variant(_fullObstacles);
                    return;
                }

                Debug.Log("Error");
            }

            public void Variant(List<GameObject> var)
            {
                var[0].SetActive(true);
            }

            public void Disable()
            {
                foreach(GameObject obj in _topObstacles)
                {
                    obj.SetActive(false);
                }
                foreach(GameObject obj in _bottomObstacles)
                {
                    obj.SetActive(false);
                }
                foreach(GameObject obj in _fullObstacles)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}


