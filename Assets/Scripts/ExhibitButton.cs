using UnityEngine;

public class ExhibitButton : MonoBehaviour
{
    [SerializeField] ExhibitDataSO exhibitData;
    [SerializeField] GameObject UIPrefab;
    [SerializeField] Vector3 uiPosition = new Vector3(-1, 1.5f, 1.5f);
    [SerializeField] Vector3 uiRotation = new Vector3(0, 0, 0);
    GameObject uiInstance;
    RectTransform uiTransform;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.root.CompareTag("Player"))
        {
            // Show Exhibit Information UI
            if (uiInstance == null)
            {
                uiInstance = Instantiate(UIPrefab, transform);
                Canvas canvas = uiInstance.GetComponent<Canvas>();
                if (canvas != null)
                    canvas.worldCamera = Camera.main;
                ExhibitInfo uiScript = uiInstance.GetComponent<ExhibitInfo>();
                if (uiScript != null)
                    uiScript.SetText(exhibitData);

                uiTransform = uiInstance.GetComponent<RectTransform>();
            }

            // Set the UI position and rotation
            uiTransform.anchoredPosition3D = uiPosition;
            Quaternion rotation = Quaternion.RotateTowards(uiTransform.rotation, Quaternion.Euler(uiRotation), 360f);
            uiTransform.Rotate(rotation.eulerAngles);
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
}
