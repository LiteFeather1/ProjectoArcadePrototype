using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadLevel : MonoBehaviour
{
    [SerializeField] private int _level;

    private void OnEnable()
    {
        if(IG_GameManager.Instance != null)
        {
            _level = IG_GameManager.Instance.NextLevel;;
        }
    }
    //Only works with Harry so far...
    public void OnPress()
    {
        SceneManager.LoadScene("Level" + _level.ToString());
        if(UiManager.Instance != null)
        {
            Destroy(UiManager.Instance.gameObject);
        }
    }
}
