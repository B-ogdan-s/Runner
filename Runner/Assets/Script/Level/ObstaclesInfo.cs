using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "ObstaclesInfo", menuName = "Level/ObstaclesInfo")]
    public class ObstaclesInfo : ScriptableObject
    {
        public GameObject[] _bottomObstacles;
        public GameObject[] _topObstacles;
    }
}
