using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableSpike : MonoBehaviour
{
    [SerializeField] private float _delayToActivate;
    [SerializeField] private float _spikeSpeed;
    [SerializeField] private GameObject _spikes;
    [Range(-1, 1)][SerializeField] private int _x;
    [Range(-1, 1)][SerializeField] private int _y;
    private Vector3 _startPos;
    private Vector3 _pointToMove;
    private void Awake()
    {
        _startPos = _spikes.transform.position;
        _pointToMove = new Vector2(_spikes.transform.position.x + _x, _spikes.transform.position.y + _y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(MoveSpikes_Co());
        }
    }

    IEnumerator MoveSpikes_Co()
    {
        yield return new WaitForSeconds(_delayToActivate);
        while (_spikes.transform.position != _pointToMove)
        {
            _spikes.transform.position = Vector2.MoveTowards(_spikes.transform.position, _pointToMove, _spikeSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(_delayToActivate * 2);
        StartCoroutine(MoveBack());
    }

    protected virtual IEnumerator MoveBack()
    {
        while (_spikes.transform.position != _startPos)
        {
            _spikes.transform.position = Vector2.MoveTowards(_spikes.transform.position, _startPos, _spikeSpeed/2 * Time.deltaTime);
            yield return null;
        }
    }
}
