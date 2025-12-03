using UnityEngine;
using static UnityEngine.GridBrushBase;

public class ExhibitButton : MonoBehaviour
{
    [SerializeField] ExhibitDataSO exhibitData;
    [SerializeField] GameObject UIPrefab;
    [SerializeField] Vector3 uiPosition = new Vector3(-1, 1.5f, 1.5f);
    [SerializeField] Vector3 uiRotation = new Vector3(0, 0, 0);
    GameObject uiInstance;
    [SerializeField] float rotationSpeed = 15f;
    bool rotateRight = true;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.root.CompareTag("Player"))
        {
            // Show Exhibit Information UI
            uiInstance = ShowExhibitInfo(uiInstance, transform, UIPrefab, exhibitData, uiPosition, uiRotation);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        // Reset variables and destroy prefab instances
        if (uiInstance != null)
        {
            Destroy(uiInstance);
        }
    }

    public static GameObject ShowExhibitInfo(GameObject uiInstance, Transform parentTransform, GameObject UIPrefab, ExhibitDataSO exhibitData, Vector3 uiPosition, Vector3 uiRotation)
    {
        // Show Exhibit Information UI
        if (uiInstance == null)
        {
            uiInstance = Instantiate(UIPrefab, parentTransform);
            Canvas canvas = uiInstance.GetComponent<Canvas>();
            if (canvas != null)
                canvas.worldCamera = Camera.main;
            ExhibitInfo uiScript = uiInstance.GetComponent<ExhibitInfo>();
            if (uiScript != null)
                uiScript.SetText(exhibitData);
        }

        // Set the UI position and rotation
        RectTransform uiTransform = uiInstance.GetComponent<RectTransform>();
        uiTransform.anchoredPosition3D = uiPosition;
        Quaternion rotation = Quaternion.RotateTowards(uiTransform.rotation, Quaternion.Euler(uiRotation), 360f);
        uiTransform.Rotate(rotation.eulerAngles);

        return uiInstance;
    }

    public void RotateRight()
    {
        rotateRight = true;
        float rotationDirection = rotateRight ? 1f : -1f;
        uiInstance.transform.Rotate(Vector3.up, rotationSpeed * rotationDirection * Time.deltaTime);
    }
    public void RotateLeft()
    {
        rotateRight = false;
        float rotationDirection = rotateRight ? 1f : -1f;
        uiInstance.transform.Rotate(Vector3.up, rotationSpeed * rotationDirection * Time.deltaTime);
    }
}
