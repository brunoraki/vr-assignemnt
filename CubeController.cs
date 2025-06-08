using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class CubeController : MonoBehaviour
{
    public string correctPlaceholderTag;
    [HideInInspector] public Vector3 originalPosition;
    private XRGrabInteractable grabInteractable;
    private bool isPlaced = false;

    private void Start()
    {
        originalPosition = transform.position;
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} triggered with: {other.tag}");

        if (isPlaced) return;

        if (other.CompareTag(correctPlaceholderTag))
        {
            Debug.Log($"{gameObject.name} placed correctly on {other.tag}");

            // Snap to placeholder position
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;

            // Lock interaction
            grabInteractable.enabled = false;
            isPlaced = true;

            // Notify game manager
            FindObjectOfType<GameManager>().CubePlaced();
        }
        else if (other.CompareTag("RedPlaceholder") || other.CompareTag("GreenPlaceholder") || other.CompareTag("BluePlaceholder"))
        {
            Debug.Log($"{gameObject.name} placed on WRONG placeholder: {other.tag}");
            StartCoroutine(ResetPosition());
        }
    }

    private IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(0.2f);
        transform.position = originalPosition;

        // Optional buzz feedback
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null) audio.Play();
    }
}
