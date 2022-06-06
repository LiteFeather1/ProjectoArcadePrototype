using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cinemachine;

public class CustomGameObjectInstantiate : MonoBehaviour
{
    [MenuItem("GameObject/MyCategory/Level Template", false, 10)]

    static void CreateLevelTemplate(MenuCommand menuCommand)
    {
        // Create a custom game object
        //GameObject levelTemplate = new GameObject("Custom Game Object");
        GameObject resourcePrefab = Resources.Load<GameObject>("LevelTemplate");
        GameObject levelTemplate = Instantiate(resourcePrefab);
        levelTemplate.name = "Level Template";
        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(levelTemplate, menuCommand.context as GameObject);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(levelTemplate, "Create " + levelTemplate.name);
        Selection.activeObject = levelTemplate;

        ChangeLevelActive changeLevelActive = levelTemplate.GetComponent<ChangeLevelActive>();

        CinemachineConfiner confiner =  GameObject.Find("CM vcam1").GetComponent<CinemachineConfiner>();

        changeLevelActive.Confiner = confiner;
    }
}
