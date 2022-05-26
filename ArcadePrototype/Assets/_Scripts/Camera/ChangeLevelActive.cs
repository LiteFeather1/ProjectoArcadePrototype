using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
public class ChangeLevelActive : MonoBehaviour
{
    [SerializeField] private GameObject _currentLevelLoaded;
    [SerializeField] private CinemachineConfiner _confiner;

    private PolygonCollider2D _myCollider;
    [SerializeField] private Vector2[] _points;

    private void Start()
    {
        _myCollider = GetComponent<PolygonCollider2D>();
        GetPoints();
    }
    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (_points !=  null)
        {
            Gizmos.DrawLine(_points[0], _points[_points.Length - 1]);
            for (int i = 1; i < _points.Length; i++)
            {
                Gizmos.DrawLine(_points[i], _points[i - 1]);
            }
        }
    }

    //GetsThe Points of the polygon collider to Draw the level size
    private void GetPoints()
    {
        _points = _myCollider.points;
        Vector2 position = transform.position;
        Vector2 size = new Vector2(transform.localScale.x, transform.localScale.y);
        for (int i = 0; i < _myCollider.points.Length; i++)
        {
            Vector2 realPoints = _myCollider.points[i] * size;
            realPoints += position;
            _points[i] = realPoints;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _currentLevelLoaded.SetActive(true);
            _confiner.m_BoundingShape2D = _myCollider;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _currentLevelLoaded.SetActive(false);
            _confiner.m_BoundingShape2D = null;
        }
    }
}
