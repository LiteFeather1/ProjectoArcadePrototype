using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyWhenScene : MonoBehaviour
{
    [SerializeField] private string _inWhatSceneToDestroy = "MainStart";
    void OnEnable()
    {
        SceneManager.activeSceneChanged += DestroOnSceneChange;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= DestroOnSceneChange;
    }

    private void DestroOnSceneChange(Scene current, Scene next)
    {
        Scene scene = SceneManager.GetActiveScene();
        print(scene.name);
        if (scene.name == _inWhatSceneToDestroy)
            Destroy(gameObject);
    }
}
