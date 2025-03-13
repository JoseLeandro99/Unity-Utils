using UnityEngine;

// Simple player interaction with raycast objects

public class PlayerInteraction : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float interactionRange;
    [SerializeField] private LayerMask interactionLayer;

    private Transform mainCamera;
    private RaycastHit _raycastHit;
    private Ray _ray;

    [Header("Interaction")]
    [SerializeField] private GameObject interactionObject;

    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        _ray.origin = mainCamera.position;
        _ray.direction = mainCamera.forward;

        if (Physics.Raycast(_ray, out _raycastHit, interactionRange, interactionLayer))
        {
            interactionObject = _raycastHit.collider.gameObject;

            if (interactionObject.CompareTag("Door"))
            {
                interactionObject.SendMessage("OpenDoor", SendMessageOptions.DontRequireReceiver);
            }

            if (interactionObject.CompareTag("Lever"))
            {
                interactionObject.SendMessage("PullLever", SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            interactionObject = null;
        }
    }
}
