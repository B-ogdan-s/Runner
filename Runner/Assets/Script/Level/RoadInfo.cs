using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "RoadInfo", menuName = "Level/RoadInfo")]
    public class RoadInfo : ScriptableObject
    {
        public int _numPoolRoads;
        public int _numRoads;
        public int _numObstacles;
        public float _width;
        public float _length;
        public GameObject _roadPrefab;
        public GameObject _roadPrefab2;
        public GameObject _empty;
        public ObstaclesInfo _obstaclesInfo;
    }
}
