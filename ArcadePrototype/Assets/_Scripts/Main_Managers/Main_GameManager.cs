using UnityEngine;

public class Main_GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _levels;
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
        DontDestroyOnLoad(gameObject);
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
}
