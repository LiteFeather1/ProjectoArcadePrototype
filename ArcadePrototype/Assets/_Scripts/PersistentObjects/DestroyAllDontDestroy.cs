using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllDontDestroy : MonoBehaviour
{
    private void Start()
    {
        if (PersistentTimer.Instance != null)
            Destroy(PersistentTimer.Instance.gameObject);

        if (PersistentDeathCount.Instance != null)
            Destroy(PersistentDeathCount.Instance.gameObject);
    }
}
