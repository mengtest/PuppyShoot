using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectsPoolMechine : MonoBehaviour
{
    public List<PooledObjectInfo> PooledObjectList;

    public PooledObjectInfo pooledObjectInfo;

    public bool AddToList()
    {
        for (int i = 0; i < PooledObjectList.Count; i++)
        {
            if (PooledObjectList[i].key == pooledObjectInfo.key)
            {
                return false;
            }
        }
        PooledObjectInfo poi = new PooledObjectInfo(pooledObjectInfo);
        PooledObjectList.Add(poi);
        return true;
    }

    public bool RemoveFromList()
    {
        PooledObjectInfo poi=null;
        for (int i = 0; i < PooledObjectList.Count; i++)
        {
            if (PooledObjectList[i].key == pooledObjectInfo.key)
            {
                poi = PooledObjectList[i];
                break;
            }
        }

        if (poi == null) return false;
        PooledObjectList.Remove(poi);
        return true;
    }

    public void ResetList()
    {
        PooledObjectList.Clear();
    }


    public void PoolAllObjects()
    {
        ObjectPooler.InitObjectPooler(PooledObjectList);
    }



}
