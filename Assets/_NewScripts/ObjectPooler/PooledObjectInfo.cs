using UnityEngine;
using System.Collections;
[System.Serializable]
public class PooledObjectInfo
{
    public string key;
    public GameObject prefab;
    public int prepolulate;
    public int maxCount;

    public PooledObjectInfo(PooledObjectInfo poi)
    {
        this.key = poi.key;
        this.prefab = poi.prefab;
        this.prepolulate = poi.prepolulate;
        this.maxCount = poi.maxCount;
    }
}
