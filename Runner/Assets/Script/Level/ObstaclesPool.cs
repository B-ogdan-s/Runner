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
        private float _shift;
        private float _length;
        private int _num;

        public static System.Action _CreateObstacles;

        public ObstaclesPool(Transform parent, RoadInfo info, float shift)
        {
            _obstaclesInfo = info._obstaclesInfo;
            _parent = parent;
            _shift = shift;
            _empty = info._empty;
            _num = info._numObstacles;
            _length = info._length;
            Spawn();
        }

        private void Spawn()
        {
            for(int i = 0; i < _num; i++)
            {
                GameObject obj = MonoBehaviour.Instantiate(_empty);
                obj.transform.SetParent(_parent);
                Debug.Log(-(_length / 2) + (_shift / 2f) + (_shift * i) / _parent.localScale.z);
                obj.transform.localPosition = new Vector3(0, 0, (-(_length / 2) + _shift / 2f + _shift * i) / _parent.localScale.z);
            }
        }
    }
}


