using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHitBox : MonoBehaviour, IDamageable
{
    [SerializeField] private int _hitsToReset = 3;
    [SerializeField] private float _invulnerabilityTime = 1f;
    private bool _canGetHit = true;
    private Vector2 _resetPos;

    [Header("Event")]
    [SerializeField] private UnityEvent _death;

    [Header("PlayerComponent")]
    private HorizontalMoviment _hm;
    private Jump _jump;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    public int HitsToReset { get => _hitsToReset; set => _hitsToReset = Mathf.Clamp(value, 0, 3); }
    public UnityEvent Death { get => _death; set => _death = value; }

    private void Awake()
    {
        _resetPos = transform.position;
        _hm = GetComponent<HorizontalMoviment>();
        _jump = GetComponent<Jump>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        Main_InGameUiManager.Instance.HealthToDisplay(HitsToReset, 3);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) TakeDamage(1, 0);
    }

    public void TakeDamage(int hitAmount, float stunDuration)
    {
        if (_canGetHit)
        {
            _canGetHit = false;
            HitsToReset -= hitAmount;
            StartCoroutine(Co_invulnerability());
            StartCoroutine(StunDuration_Co(stunDuration));
            StartCoroutine(BlinkRed());
            Main_InGameUiManager.Instance.HealthToDisplay(HitsToReset, 3);
            if (HitsToReset <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        _rb.velocity = Vector2.zero;
        transform.position = _resetPos;
        HitsToReset = 3;
        Main_InGameUiManager.Instance.HealthToDisplay(HitsToReset, 3);
        Death?.Invoke();
    }

    IEnumerator Co_invulnerability()
    {
        yield return new WaitForSeconds(_invulnerabilityTime);
        _canGetHit = true;
    }

    IEnumerator StunDuration_Co(float stunDuration)
    {
        if (stunDuration == 0) yield break;
        _jump.enabled = false;
        _hm.enabled = false;
        yield return new WaitForSeconds(stunDuration);
        _jump.enabled = true;
        _hm.enabled = true;
    }

    IEnumerator BlinkRed()
    {
        for (int i = 0; i < 3; i++)
        {
            _sr.color = new Color(0.9f, 0, 0);
            yield return new WaitForSeconds(0.025f);
            _sr.color = Color.white;
            yield return new WaitForSeconds(0.025f);
        }
    }

    public void RestoreHealth(int amountToRestore)
    {
        HitsToReset += amountToRestore;
        Main_InGameUiManager.Instance.HealthToDisplay(HitsToReset, 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {
            RestoreHealth(2);
            _resetPos = collision.transform.position;
        }
    }
}
