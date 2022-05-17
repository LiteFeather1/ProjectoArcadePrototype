using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : ComplexSingleton<ObjectPooler>
{
    [SerializeField] private List<Pool> _availablePools = new List<Pool>();

    Dictionary<string, Queue<GameObject>> _pools = new Dictionary<string, Queue<GameObject>>();

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        foreach (var item in _availablePools)
        {
            Queue<GameObject> objects = new Queue<GameObject>();
            for (int i = 0; i < item.PoolSize; i++)
            {
                GameObject createdObject = Instantiate(item.ObjectToSpawn, transform.position, Quaternion.identity);
                createdObject.transform.SetParent(item.Parent.transform);
                createdObject.SetActive(false);
                objects.Enqueue(createdObject);
            }
            _pools.Add(item.PoolName, objects);
        }
    }

    public GameObject UsedObjectFromPool(string poolName, Vector3 positionToUse, Quaternion quaternionToUse)
    {
        if (!_pools.ContainsKey(poolName))
        {
            Debug.Log("This poool doens't exist");
            return null;
        }
        GameObject usedObject = _pools[poolName].Dequeue();
        usedObject.SetActive(true);
        usedObject.transform.position = positionToUse;
        usedObject.transform.rotation = quaternionToUse;
        _pools[poolName].Enqueue(usedObject);

        return usedObject;
    }
}
