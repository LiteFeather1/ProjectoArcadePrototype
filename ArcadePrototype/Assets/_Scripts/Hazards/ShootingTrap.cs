using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrap : MonoBehaviour, IButtonable
{
    [Header("Stats")]
    [SerializeField] private float _speed = 10;
    [Range(0,3)][SerializeField] private int _damage = 1;
    [SerializeField] private float _stunDuration;
    [SerializeField] private float _distanceToShot = 1;

    [SerializeField] private float _fireRate = 1;
    private float _timePassed;

    [Header("Bullet")]
    [SerializeField] private int _howManyBullets = 1;
    [SerializeField] private float _timeBetweenBullets = 0.1f;
    [SerializeField] private float _spreadFactor;
    [SerializeField] private float _bulletLifeSpan = 10;

    [Header("Firing")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [HideInInspector] private Transform _objectToFace;

    [Header("Sprites")]
    [SerializeField] private Sprite s_Firing;
    [SerializeField] private Sprite s_Stopped;

    private bool _canShot = true;

    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, transform.position);
        if (distance < _distanceToShot)
        {
            //FacePlayer();
            _timePassed += Time.deltaTime;
            if (_timePassed > _fireRate)
            {
                if (_canShot)
                {
                    _timePassed = 0;
                    StartCoroutine(ShotInDirection());
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distanceToShot);
    }
    private void FacePlayer()
    {
        Vector2 objectPos = _objectToFace.position;

        float angle = MathHelper.AngleBetweenTwoPoints(-transform.position, -objectPos);

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    IEnumerator ShotInDirection()
    {
        for (int i = 0; i < _howManyBullets; i++)
        {
            GameObject newFire = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);
            newFire.transform.right = transform.right;

            newFire.transform.Rotate(new Vector3(0, 0, MathHelper.Spread(i, _spreadFactor, _howManyBullets)), Space.Self);
            Bullet newFireComponent = newFire.GetComponent<Bullet>();
            newFireComponent.Spawn(_damage, _stunDuration, _speed, newFire.transform.right, _bulletLifeSpan);
            yield return new WaitForSeconds(_timeBetweenBullets);
        }
    }

    public void ToInterract(bool state)
    {
        _canShot = state;
        if (_canShot)
            _sr.sprite = s_Firing;
        else
            _sr.sprite = s_Stopped;
    }
}

