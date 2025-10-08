using UnityEngine;

public class ExhibitButton : MonoBehaviour
{
    [SerializeField] ExhibitDataSO exhibitData;
    [SerializeField] GameObject UIPrefab;
    GameObject uiInstance;
    bool isVisible = false;
    bool inRange = false;

    void Update()
    {
        // todo: replace with vr input
        if (Input.GetKeyDown(KeyCode.B) && inRange)
        {
            Debug.Log("Exhibit Button Pressed");
            if (!isVisible)
            {
                // Show Exhibit Information UI
                isVisible = true;
            }
            else
            {
                // Hide Exhibit Information UI
                isVisible = false;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = true;
            isVisible = true;

            // Show Exhibit Information UI
            if (uiInstance == null)
            {
                uiInstance = Instantiate(UIPrefab, transform);
                ExhibitInfo uiScript = uiInstance.GetComponent<ExhibitInfo>();
                if (uiScript != null)
                    uiScript.SetText(exhibitData);
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        // Reset variables and destroy prefab instances
        if (uiInstance != null)
        {
            Destroy(uiInstance);
        }
        inRange = false;
        isVisible = false;
    }
}
