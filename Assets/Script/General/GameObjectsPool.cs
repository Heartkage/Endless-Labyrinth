using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class GameObjectsPool : MonoBehaviour
    {

        #region Singleton
        public static GameObjectsPool instance;
        void Awake()
        {
            instance = this;
        }
        #endregion

        [Tooltip("Assign a Prefab for this pool")]
        public GameObject prefab;

        private Stack<GameObject> inactiveObject = new Stack<GameObject>();

        public GameObject SpawnGameObject()
        {
            GameObject newObject = null;

            if (inactiveObject.Count > 0)
            {
                newObject = inactiveObject.Pop();
            }
            else
            {
                newObject = (GameObject)GameObject.Instantiate(prefab);
                newObject.AddComponent<Pool>().pool = this;
            }
            newObject.SetActive(true);
            newObject.transform.SetParent(null);
            
            return newObject;
        }

        public void StoreGameObject(GameObject storeObject)
        {
            Pool poolList = storeObject.GetComponent<Pool>();

            if ((poolList != null) && (poolList.pool == this))
            {
                storeObject.transform.SetParent(transform);
                storeObject.SetActive(false);

                inactiveObject.Push(storeObject);
            }
            else
            {
                Debug.Log("Storing object failed");
                Destroy(storeObject);
            }
               
        }
	}

    public class Pool : MonoBehaviour 
    {
        public GameObjectsPool pool;
    }
}

