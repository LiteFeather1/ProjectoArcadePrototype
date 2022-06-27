using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightableBlock : MonoBehaviour, Ilightnable
{
    private bool _lightning;
    private float _pullSpeed = 35;
    private Vector3 _whereTo;
    Vector2 dir;
    private bool _once;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(_lightning)
        {
            Moviment(_whereTo);
        }
        else
        {
            RoundPosition();
        }
    }

    public IEnumerator Co_LightingReturnFalse(float time)
    {
        yield return new WaitForSeconds(time);
        _lightning = false;
        _once = false;
    }
    // Moviment of the lightableblock,  Has to move just in the direction
    // Maybe I should've used vector2.up.down..etc to have the direction be straight better
    private void Moviment(Vector3 whereTo)
    {
        if (!_once)
        {
            dir = (whereTo - transform.position).normalized;
            _once = true;
        }

        if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
        {
            dir.x = 0;
        }
        else if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            dir.y = 0;
        }

        dir.x = Mathf.RoundToInt(dir.x);
        dir.y = Mathf.RoundToInt(dir.y);
        //print(dir);
        Vector2 posToMove = Vector2.MoveTowards(transform.position, whereTo, _pullSpeed * Time.deltaTime);
        _rb.MovePosition(posToMove);
        //transform.Translate(dir * _pullSpeed * Time.deltaTime, Space.World);
    }

    public void TurnTrueLightning(Vector3 whereTo)
    { 
        _lightning = true;
        _once = false;
        _whereTo = whereTo;
        print(_whereTo);
    }

    public bool ReturnLightningFalse()
    {
        return _lightning = false;
    }
    // rounds the position when not moving AKA on _lighting "state"
    private void RoundPosition()
    {
        int posY = Mathf.RoundToInt(transform.position.y);
        int posX = Mathf.RoundToInt(transform.position.x);
        transform.position = new Vector2(posX, posY);
    }

    private void Stop()
    {
        _lightning = false;
        _whereTo = transform.position;
        RoundPosition();
        _once = false;
    }

   // it also collects coins
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectable collectable = collision.gameObject.GetComponent<ICollectable>();
        if (collectable != null)
        {
            collectable.ToCollect();
        }

        if (collision.gameObject.tag == Tags.Grid)
        {
            Stop();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != Tags.Spike && collision.gameObject.tag != Tags.Ladder )
        {
            Stop();
        }
    }
}
