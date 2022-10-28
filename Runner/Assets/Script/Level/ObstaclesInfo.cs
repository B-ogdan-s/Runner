using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "ObstaclesInfo", menuName = "Level/ObstaclesInfo")]
    public class ObstaclesInfo : ScriptableObject
    {
        public int _minNumObstacles;
        public int _maxNumObstacles;
        public GameObject _empty;
        public GameObject[] _bottomObstacles;
        public GameObject[] _topObstacles;
        public GameObject[] _fullObstacles;

        public int[] _value = new int[4];
    }
}
