using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    [SerializeField] private string _poolName;
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private int _poolSize;

    public int PoolSize { get => _poolSize; }
    public GameObject ObjectToSpawn { get => _objectToSpawn; }
    public string PoolName { get => _poolName; }
    public GameObject Parent { get => _parent; }
}
