using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Main_GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject>  _levels;

    public static Main_GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        DeactivateLevel();
    }

    private void DeactivateLevel()
    {
        foreach (var item in _levels)
        {
            item.SetActive(false);
        }
    }

    [ContextMenu("Get All Contents")]
    private void GetAllContents()
    {
        _levels = FindObjectsOfType<GameObject>().Where(t => t.name.ToLower().Contains("mycontent")).ToList();
    }
}
