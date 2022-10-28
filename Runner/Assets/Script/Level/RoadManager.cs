using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class RoadManager : MonoBehaviour
    {
        public System.Action _Trigger;

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Player.Player>() != null)
            {
                _Trigger?.Invoke();
            }
        }
    }
}
