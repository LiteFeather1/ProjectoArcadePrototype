using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnCol : ConstantMoviment
{
    [SerializeField] private Transform _whereToReset;
    private float _whereToHeight;
    private bool _canMove = true;

    protected override void Start()
    {
        base.Start();
        _whereToHeight = _whereToReset.localScale.y;
    }

    protected override void Update()
    {
        if (_canMove)
        {
            base.Update();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Destroyer")
        {
            _canMove = false;
            StartCoroutine(WaitToMove());
            float randomY = Random.Range(-_whereToHeight/2, _whereToHeight/2);
            gameObject.transform.position = new Vector2(_whereToReset.position.x, _whereToReset.position.y + randomY);
        }
    }

    IEnumerator WaitToMove()
    {
        float time = Random.Range(1, 2f);
        yield return new WaitForSeconds(time);
        _canMove = true;
        RandomSpeed();
    }
}
