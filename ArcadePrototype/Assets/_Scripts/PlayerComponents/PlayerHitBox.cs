using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour, IDamageable
{
    [SerializeField] private int _hitsToReset = 3;
    [SerializeField] private float _invulnerabilityTime = 1f;
    private bool _canGetHit = true;
    private Vector2 _resetPos;

    [Header("PlayerComponent")]
    private HorizontalMoviment _hm;
    private Jump _jump;

    public int HitsToReset { get => _hitsToReset; set => _hitsToReset = Mathf.Clamp(value, 0, 3); }

    private void Awake()
    {
        _resetPos = transform.position;
        _hm = GetComponent<HorizontalMoviment>();
        _jump = GetComponent<Jump>();
    }
    private void Start()
    {
        Main_UiManager.Instance.HealthToDisplay(HitsToReset, 3);
    }

    public void TakeDamage(int hitAmount, float stunDuration)
    {
        if (_canGetHit)
        {
            _canGetHit = false;
            HitsToReset -= hitAmount;
            StartCoroutine(Co_invulnerability());
            StartCoroutine(StunDuration_Co(stunDuration));
            Main_UiManager.Instance.HealthToDisplay(HitsToReset, 3);
            if (HitsToReset <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        transform.position = _resetPos;
        HitsToReset = 3;
        Main_UiManager.Instance.HealthToDisplay(HitsToReset, 3);
    }

    IEnumerator Co_invulnerability()
    {
        yield return new WaitForSeconds(_invulnerabilityTime);
        _canGetHit = true;
    }

    IEnumerator StunDuration_Co(float stunDuration)
    {
        _jump.enabled = false;
        _hm.enabled = false;
        yield return new WaitForSeconds(stunDuration);
        _jump.enabled = true;
        _hm.enabled = true;
    }

    public void RestoreHealth(int amountToRestore)
    {
        HitsToReset += amountToRestore;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {
            _resetPos = collision.transform.position;
        }
    }
}
