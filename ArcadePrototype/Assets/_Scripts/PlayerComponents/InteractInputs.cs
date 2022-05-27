using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractInputs : MonoBehaviour
{
    [SerializeField] private LayerMask _deactivatableMask;

    private CapsuleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        GetAllMasksButPlayer();
    }

    void Update()
    {
        DeactivatePlatsBellow();
    }

    private void DeactivatePlatsBellow()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            float extraHeightText = .125f;
            RaycastHit2D hit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, extraHeightText, _deactivatableMask);
            if (hit.collider != null)
            {
                IIteractable iteractable = hit.collider.GetComponent<IIteractable>();

                if (iteractable != null)
                {
                    iteractable.ToInteract();
                }
            }
        }
    }

    private void GetAllMasksButPlayer()
    {
        _deactivatableMask -= LayerMask.GetMask("Player");
        _deactivatableMask -= LayerMask.GetMask("Level");
    }
}
