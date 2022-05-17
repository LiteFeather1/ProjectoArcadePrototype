using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightableBlock : MonoBehaviour, Ilightnable
{
    private bool _lightning;
    private float _pullSpeed = 17.5f;
    private Vector3 _whereTo;
    Vector2 dir;
    private bool _once;

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
        print(dir);
        transform.Translate(dir * _pullSpeed * Time.deltaTime, Space.World);
    }

    public void TurnTrueLightning(Vector3 whereTo)
    { 
        _lightning = true;
        _once = false;
        _whereTo = whereTo;
    }

    public bool ReturnLightningFalse()
    {
        return _lightning = false;
    }

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
            print("1");
            Stop();
        }
    }
}
