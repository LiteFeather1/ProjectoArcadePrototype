using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grip2ndJumpDashReplinisher : MonoBehaviour
{
    [SerializeField] private float _timeToRespawn;
    [SerializeField] private bool _restoreStamina = true;
    [SerializeField] private bool _restoreSecondJump = true;
    [SerializeField] private bool _restoreDash = true;

    private Collider2D _myColl;
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _pS;

    private void Awake()
    {
        _myColl = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _pS = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (_restoreStamina) collision.GetComponent<WallStamina>().ReplenishStamina();
            if (_restoreSecondJump) collision.GetComponent<Jump>().ReplenishSecondaryJump();
            if (_restoreDash) collision.GetComponent<Dash>().ReplenishDash();
            StartCoroutine(DisableSrC());
            _pS.Play();
        }
    }

    IEnumerator DisableSrC()
    {
        _myColl.enabled = false;
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(_timeToRespawn);
        _myColl.enabled = true;
        _spriteRenderer.enabled = true;
    }
}
