using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour, ICollectable
{
    public void ToCollect()
    {
        if(IG_GameManager.Instance != null)
        { 
            IG_GameManager.Instance.ColletingCoins();
        }
        AudioManager.PlaySound("Coins");
        Destroy(gameObject);
    }
}
