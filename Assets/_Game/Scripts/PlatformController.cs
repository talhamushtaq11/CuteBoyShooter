using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class PlatformController : MonoBehaviour
    {
        public GameObject jeepObj;
        public GameObject obstaclesParent;
        public GameObject enemiesParent;

        void Start()
        {
            if(!LevelManager.Instance.isJeepCollected && Random.Range(1, 10) <= 4)
            {
                jeepObj.SetActive(true);
            }
        }

        void Update()
        {
            if (LevelManager.Instance.isJeepCollected)
            {
                obstaclesParent.SetActive(false);
            }
            else
            {
                obstaclesParent.SetActive(true);
            }
        }
    }
}