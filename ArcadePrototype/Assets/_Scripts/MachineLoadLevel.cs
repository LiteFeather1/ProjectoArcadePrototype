using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MachineLoadLevel : MonoBehaviour, IIteractable
{
    [SerializeField] private string _levelToLoad;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            SceneManager.LoadScene(_levelToLoad);
    }
    public void ToInteract()
    {
        if (_levelToLoad != "")
            SceneManager.LoadScene(_levelToLoad);
    }
}
