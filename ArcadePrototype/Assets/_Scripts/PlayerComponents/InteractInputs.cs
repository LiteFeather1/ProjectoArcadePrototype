using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractInputs : MonoBehaviour
{
    [SerializeField] private LayerMask _deactivatableMask;

    [SerializeField] private LayerMask _interactMask;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        GetAllMasksButPlayer();
    }

    void Update()
    {
        DeactivatePlatsBellow();
        InteractWithMachine();
    }

    private void DeactivatePlatsBellow()
    {
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            float extraHeightText = .125f;
            RaycastHit2D hit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, extraHeightText, _deactivatableMask);
            Color rayColor;
            if (hit.collider != null)
            {
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
            }
            Debug.DrawRay(transform.position, transform.right * 1.33f, rayColor, 0);
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

    private void InteractWithMachine()
    {
        if(Input.GetButtonDown("Interact"))
        {
            RaycastHit2D hit = Physics2D.Raycast(_collider.bounds.center, transform.right, .25f, _interactMask);

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
        _deactivatableMask -= LayerMask.GetMask("Machine");
    }
}
