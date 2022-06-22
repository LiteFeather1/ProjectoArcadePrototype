using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text txt_time;
    private PersistentTimer _persistentTimer;

    private void Awake()
    {
        _persistentTimer = PersistentTimer.Instance;
    }

    private void Update()
    {
        TimeToDisplay();
    }

    public void TimeToDisplay()
    {
        float minutes = Mathf.FloorToInt(_persistentTimer.Timer / 60);
        float seconds = (_persistentTimer.Timer % 60);

        txt_time.text = string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _persistentTimer.DisableMe();
        }
    }
}
