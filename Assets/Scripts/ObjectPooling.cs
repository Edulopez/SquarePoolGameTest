using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour
{
    public int size = 10;

    public GameObject basePooledObject;
    public List<GameObject> pooledObjects;
    public bool willGrow = true;
    
    protected void Start()
    {
        // Current = this;
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < size; i++)
        {
            var obj = (GameObject)Instantiate(basePooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy) continue;

            pooledObjects[i].SetActive(true);
            return pooledObjects[i];
        }

        if (willGrow)
        {
            var obj = (GameObject)Instantiate(basePooledObject);
            pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }

    public int GetActiveAmount()
    {
        int res = 0;
        for (int i = 0; i < pooledObjects.Count; i++)
            if (pooledObjects[i].activeInHierarchy)
                res++;
        return res;
    }

    public void DestroyAll()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
            pooledObjects[i].SetActive(false);
    }
}
