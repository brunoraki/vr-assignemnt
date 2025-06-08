using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    private int placedCount = 0;
    public GameObject successUI;
    public Button restartButton;

    public void CubePlaced()
    {
        placedCount++;
        if (placedCount == 3)
        {
            successUI.SetActive(true);
        }
    }

    public void RestartPuzzle()
    {
        placedCount = 0;
        successUI.SetActive(false);
        CubeController[] cubes = FindObjectsOfType<CubeController>();
        foreach (var cube in cubes)
        {
            cube.transform.position = cube.originalPosition;
            XRGrabInteractable grab = cube.GetComponent<XRGrabInteractable>();
            if (grab != null) grab.enabled = true;
        }
    }
}
