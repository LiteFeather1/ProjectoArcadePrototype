using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Isaac _harry;

    private Queue<GameObject> _lightings = new Queue<GameObject>();

    private bool _canMove = true;
    private bool _isHarrySolid = true;

    [SerializeField] private Animator _ac;
    [SerializeField] private SpriteRenderer _sr;

    private Vector2 _position;
    private Vector2 _currentPosition;

    ObjectPooler _objectPooler;
    private void Start()
    {
        _position = transform.position;
        _currentPosition = transform.position;
        _objectPooler = ObjectPooler.Instance;
        StartCoroutine(Co_moviment());
    }

    public Isaac HarryComponent(Isaac harry)
    {
        _harry = harry;
        return _harry;
    }

    public bool IsHarrySolid(bool isHe)
    {
        _isHarrySolid = isHe;
        return _isHarrySolid;
    }

    IEnumerator Co_moviment()
    {
        while(_canMove) 
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime, Space.Self);
            _currentPosition = transform.position;
            float distance = Vector2.Distance(_currentPosition, _position);
            if (distance > .85f)
            {
                _position = transform.position;
                GameObject newLighting = _objectPooler.UsedObjectFromPool("Lightning", transform.position, transform.rotation);
                _lightings.Enqueue(newLighting);
                foreach (var item in _lightings)
                {
                    item.GetComponent<RandomSprite>().RandomizeSprite();
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void MoveHarry(GameObject whatToMove)
    {
        _canMove = false;

        int posY = Mathf.RoundToInt(transform.position.y);
        int posX = Mathf.RoundToInt(transform.position.x);

        _ac.SetTrigger("Impact");

        StartCoroutine(DestroySlowly());

        if (Vector2.Distance(whatToMove.transform.position, transform.position) > .75f)
        {
            _harry.CurrentMoveToPoint = _harry.Co_MoveToPoint(new Vector3(posX, posY));
            StartCoroutine(_harry.CurrentMoveToPoint);
        }
        else
        {
            _harry.ReturnLightningFalse();
        }
    }

    private void MoveTowardsHarry(GameObject whatToMove)
    {
        int posY = Mathf.RoundToInt(_harry.transform.position.y);
        int posX = Mathf.RoundToInt(_harry.transform.position.x);
        _canMove = false;
        _ac.SetTrigger("Impact");
        StartCoroutine(DestroySlowly());

        if (Vector2.Distance(whatToMove.transform.position, transform.position) > .75f)
        {
            whatToMove.GetComponent<Ilightnable>().TurnTrueLightning(new Vector3(posX, posY));
        }
        else
        {
            _harry.ReturnLightningFalse();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.Steel)
        {
            MoveHarry(_harry.gameObject);
        }

        if(collision.gameObject.tag == Tags.SteelBlock)
        {
            if(_isHarrySolid)
            {
                MoveTowardsHarry(collision.gameObject);
                StartCoroutine(_harry.Co_LightingReturnFalse(.9f));
            }
            else
            {
                MoveHarry(_harry.gameObject);
            }
        }

        if(collision.gameObject.tag == Tags.Ground)
        {
            _canMove = false;
            _ac.SetTrigger("Impact");
            StartCoroutine(DestroySlowly());
            _harry.ReturnLightningFalse();
        }
    }

    IEnumerator DestroySlowly()
    {
        foreach (var item in _lightings)
        {
            item.SetActive(false);
            yield return new WaitForSeconds(.015f);
        }
        _sr.enabled = false;
        Destroy(gameObject, 1f);
    }
}
