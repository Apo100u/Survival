using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace SurvivalGame.Gameplay.Helpers
{
    public class ObjectPools : MonoBehaviour
    {
        private Dictionary<GameObject, ObjectPool<GameObject>> poolsByPrefabs = new();
        
        public GameObject GetFromPool(GameObject prefab)
        {
            EnsurePoolExists(prefab);

            return poolsByPrefabs[prefab].Get();
        }

        public void ReturnToPool(GameObject prefab, GameObject gameObject)
        {
            EnsurePoolExists(prefab);
            
            poolsByPrefabs[prefab].Release(gameObject);
        }

        private void EnsurePoolExists(GameObject prefab)
        {
            if (!poolsByPrefabs.ContainsKey(prefab))
            {
                GameObject CreateFunc()                         => Instantiate(prefab);
                void ActionOnGet(GameObject objectFromPool)     => objectFromPool.SetActive(true);
                void ActionOnRelease(GameObject releasedObject) => releasedObject.SetActive(false);

                poolsByPrefabs.Add(prefab, new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease));
            }
        }
    }
}
