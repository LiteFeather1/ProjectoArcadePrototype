using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSecretLevel : MonoBehaviour
{
    [SerializeField] private int _whatLevel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Harry")
        {
            SceneManager.LoadScene("SecretLevel" + _whatLevel);
        }
    }
}
