using UnityEngine;

public class KeyboardGrabber : MonoBehaviour
{
    public float grabRange = 3f;
    public KeyCode grabKey = KeyCode.G;
    private GameObject heldObject;
    public Transform holdPosition; // Empty GameObject to hold items

    void Update()
    {
        if (Input.GetKeyDown(grabKey))
        {
            if (heldObject == null)
            {
                TryGrab();
            }
            else
            {
                Release();
            }
        }

        if (heldObject)
        {
            heldObject.transform.position = holdPosition.position;
        }
    }

    void TryGrab()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange))
        {
            if (hit.collider.CompareTag("Grabbable"))
            {
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().useGravity = false;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    void Release()
    {
        if (heldObject)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
            heldObject = null;
        }
    }
}
