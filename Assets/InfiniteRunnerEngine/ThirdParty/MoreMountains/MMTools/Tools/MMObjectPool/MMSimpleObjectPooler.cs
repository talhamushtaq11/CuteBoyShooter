using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace MoreMountains.Tools
{
    /// <summary>
    /// A simple object pool outputting a single type of objects
    /// </summary>
    [AddComponentMenu("More Mountains/Tools/Object Pool/MMSimpleObjectPooler")]
    public class MMSimpleObjectPooler : MMObjectPooler 
	{
		public GameObject[] gameobjectsToPool;
	    /// the game object we'll instantiate 
		public GameObject GameObjectToPool;
	    /// the number of objects we'll add to the pool
		public int PoolSize = 20;
	    /// if true, the pool will automatically add objects to the itself if needed
		public bool PoolCanExpand = true;

	    /// the actual object pool
		protected List<GameObject> _pooledGameObjects;

		string firstObjectName;

	    /// <summary>
	    /// Fills the object pool with the gameobject type you've specified in the inspector
	    /// </summary>
	    public override void FillObjectPool()
	    {
		    if (GameObjectToPool == null)
            {
                return;
            }

		    // if we've already created a pool, we exit
		    if ((_objectPool != null) && (_objectPool.PooledGameObjects.Count > PoolSize))
		    {
			    return;
		    }

			CreateWaitingPool ();

			// we initialize the list we'll use to 
			_pooledGameObjects = new List<GameObject>();

            int objectsToSpawn = PoolSize;

            if (_objectPool != null)
            {
                objectsToSpawn -= _objectPool.PooledGameObjects.Count;
                _pooledGameObjects = new List<GameObject>(_objectPool.PooledGameObjects);
            }

            // we add to the pool the specified number of objects
            for (int i = 0; i < objectsToSpawn; i++)
	        {
				if(i == 0)
                {
					AddFirstObjectToThePool();
                }
                else
                {
					AddOneObjectToThePool();
                }
	        }
	    }

		/// <summary>
		/// Determines the name of the object pool.
		/// </summary>
		/// <returns>The object pool name.</returns>
		protected override string DetermineObjectPoolName()
		{
			return ("[SimpleObjectPooler] " + GameObjectToPool.name);	
		}

		int tempCount = 0;
	    /// <summary>
	    /// This method returns one inactive object from the pool
	    /// </summary>
	    /// <returns>The pooled game object.</returns>
		public override GameObject GetPooledGameObject()
		{
            //int count = _pooledGameObjects.Count;

            //// Ensure tempCount is within the valid range
            //if (count == 0 || tempCount >= count)
            //{
            //	tempCount = 0;
            //}

            //int startCount = tempCount;

            //// We go through the pool looking for an inactive object
            //for (int i = 0; i < count; i++)
            //{
            //	// Calculate the index to check, wrapping around the list if necessary
            //	int index = (tempCount + i) % count;

            //	if (!_pooledGameObjects[index].activeInHierarchy)
            //	{
            //		Debug.Log("GetPooledGameObject");
            //		// If we find one, we return it

            //		// Update tempCount to the next index
            //		tempCount = (index + 1) % count;
            //		return _pooledGameObjects[index];
            //	}
            //}






            //_pooledGameObjects.MMShuffle();

            //if (tempCount >= _pooledGameObjects.Count)
            //{
            //    tempCount = 1;
            //}

            // we go through the pool looking for an inactive object
            for (int i = tempCount; i < _pooledGameObjects.Count; i++)
            {
                if (!_pooledGameObjects[i].gameObject.activeInHierarchy/* && _pooledGameObjects[i].gameObject.name != firstObjectName*/)
                {
                    Debug.Log("GetPooledGameObject");
                    // if we find one, we return it

                    tempCount = i;
                    return _pooledGameObjects[i];
                }
            }
            // if we haven't found an inactive object (the pool is empty), and if we can extend it, we add one new object to the pool, and return it		
            if (PoolCanExpand)
			{
				return AddOneObjectToThePool();
			}
			// if the pool is empty and can't grow, we return nothing.
			return null;
		}
		
		/// <summary>
		/// Adds one object of the specified type (in the inspector) to the pool.
		/// </summary>
		/// <returns>The one object to the pool.</returns>
		//protected virtual GameObject AddOneObjectToThePool()
		//{
		//	if (GameObjectToPool==null)
		//	{
		//		Debug.LogWarning("The "+gameObject.name+" ObjectPooler doesn't have any GameObjectToPool defined.", gameObject);
		//		return null;
		//	}
		//	GameObject newGameObject = (GameObject)Instantiate(GameObjectToPool);
		//	SceneManager.MoveGameObjectToScene(newGameObject, this.gameObject.scene);
		//	newGameObject.gameObject.SetActive(false);
		//	if (NestWaitingPool)
		//	{
		//		newGameObject.transform.SetParent(_waitingPool.transform);	
		//	}
		//	newGameObject.name = GameObjectToPool.name + "-" + _pooledGameObjects.Count;

		//	_pooledGameObjects.Add(newGameObject);

  //          _objectPool.PooledGameObjects.Add(newGameObject);

  //          return newGameObject;
		//}

		protected virtual GameObject AddOneObjectToThePool()
		{
			GameObject temp = gameobjectsToPool[Random.Range(1, gameobjectsToPool.Length)];
			//GameObjectToPool = gameobjectsToPool[Random.Range(0, gameobjectsToPool.Length - 1)];
			Debug.Log("AddOneObjectToThePool() = " + temp.name);
			if (temp == null)
			{
				Debug.LogWarning("The " + temp.name + " ObjectPooler doesn't have any GameObjectToPool defined.", temp);
				return null;
			}
			GameObject newGameObject = (GameObject)Instantiate(temp);
			SceneManager.MoveGameObjectToScene(newGameObject, this.gameObject.scene);
			newGameObject.gameObject.SetActive(false);
			if (NestWaitingPool)
			{
				newGameObject.transform.SetParent(_waitingPool.transform);
			}
			newGameObject.name = temp.name + "-" + _pooledGameObjects.Count;

			_pooledGameObjects.Add(newGameObject);

			_objectPool.PooledGameObjects.Add(newGameObject);

			return newGameObject;
		}

		protected virtual GameObject AddFirstObjectToThePool()
		{
			GameObject temp = gameobjectsToPool[0];
			
			//GameObjectToPool = gameobjectsToPool[Random.Range(0, gameobjectsToPool.Length - 1)];
			Debug.Log("AddFirstObjectToThePool() = " + temp.name);
			if (temp == null)
			{
				Debug.LogWarning("The " + temp.name + " ObjectPooler doesn't have any GameObjectToPool defined.", temp);
				return null;
			}
			GameObject newGameObject = (GameObject)Instantiate(temp);
			SceneManager.MoveGameObjectToScene(newGameObject, this.gameObject.scene);
			newGameObject.gameObject.SetActive(false);
			newGameObject.transform.position = new Vector3(newGameObject.transform.position.x + 500, newGameObject.transform.position.y, newGameObject.transform.position.z);
			if (NestWaitingPool)
			{
				newGameObject.transform.SetParent(_waitingPool.transform);
			}
			newGameObject.name = temp.name + "-" + _pooledGameObjects.Count;
			firstObjectName = newGameObject.name;

			_pooledGameObjects.Add(newGameObject);

			_objectPool.PooledGameObjects.Add(newGameObject);

			return newGameObject;
		}
	}
}