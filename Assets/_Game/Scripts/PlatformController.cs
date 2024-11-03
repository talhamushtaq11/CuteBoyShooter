using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class PlatformController : MonoBehaviour
    {
        public GameObject jeepObj;
        public GameObject aeroplaneObj;
        public GameObject obstaclesParent;
        public GameObject enemiesParent;
        public GameObject groundParent;
        public GameObject bgElementsParent;
        public GameObject aeroplaneObstaclesParent;

        Vector3 normalLevelBoundsSize = new Vector3(15.6f, 7.33f, 0);
        Vector3 aeroplaneLevelBoundsSize = new Vector3(31.2f, 14.66f, 0);

        void Start()
        {
            if(Random.Range(1, 10) <= 4)
            {
                if (!LevelManager.Instance.isJeepCollected)
                {
                    if (jeepObj != null)
                        jeepObj.SetActive(true);
                }
                else if (!LevelManager.Instance.isAeroplaneCollected)
                {
                    if (aeroplaneObj != null)
                        aeroplaneObj.SetActive(true);
                }
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
                if (!LevelManager.Instance.isAeroplaneCollected)
                {
                    obstaclesParent.SetActive(true);
                }
            }

            if (LevelManager.Instance.isAeroplaneCollected)
            {
                //LevelManager.Instance.DeathBounds = new Bounds(Vector3.zero, aeroplaneLevelBoundsSize);
                //LevelManager.Instance.PlayerJump();
                //LevelManager.Instance.SetPlayerJumpCount(100);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                groundParent.SetActive(false);
                obstaclesParent.SetActive(false);
                enemiesParent.SetActive(false);
                bgElementsParent.SetActive(false);
                aeroplaneObstaclesParent.SetActive(true);
            }
            else
            {
                if (!LevelManager.Instance.isJeepCollected)
                {
                    //LevelManager.Instance.DeathBounds = new Bounds(Vector3.zero, normalLevelBoundsSize);
                    //Debug.Log("DeathBounds = " + LevelManager.Instance.DeathBounds);
                    //LevelManager.Instance.PlayerJump();
                    //LevelManager.Instance.SetPlayerJumpCount(1);
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    groundParent.SetActive(true);
                    obstaclesParent.SetActive(true);
                    bgElementsParent.SetActive(true);
                    enemiesParent.SetActive(true);
                    aeroplaneObstaclesParent.SetActive(false);
                }
            }
        }
    }
}