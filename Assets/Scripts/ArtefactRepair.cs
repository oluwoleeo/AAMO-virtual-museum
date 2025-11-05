using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArtefactRepair : MonoBehaviour
{
    [SerializeField] GameObject fracturedPrefab;
    GameObject fracturedInstance;
    Vector3 initialRotation;
    [SerializeField] GameObject completedPrefab;
    GameObject completedInstance;
    // Game object that holds the puzzle slots
    GameObject slotInstance;
    [SerializeField] Transform completedPosition;
    [SerializeField] Transform fracturedPosition;
    [SerializeField] float spacing;
    GameObject[] pieceList;
    int pieceCount;
    [SerializeField] float socketRadius = 0.1f;

    void Start()
    {
        // Instantiate the fractured prefab as a child of the table
        fracturedInstance = Instantiate(fracturedPrefab, transform);
        fracturedInstance.transform.position = completedPosition.position;

        // Get all the individual pieces
        pieceCount = fracturedInstance.transform.childCount;
        pieceList = GetAllChildren(fracturedInstance.transform);

        // Create an empty game object where the pieces will go
        slotInstance = new GameObject("slots");
        slotInstance.transform.position = completedPosition.position;

        // Create child game objects for each slot and make them children of the slot instance
        for (int i = 0; i < pieceCount; i++)
        {
            GameObject slot = new GameObject();
            slot.transform.SetParent(slotInstance.transform);
            slot.transform.position = pieceList[i].transform.position;
            slot.transform.rotation = pieceList[i].transform.rotation;

            // Add the XR socket interactor component
            slot.AddComponent<XRSocketInteractor>();

            // Add the sphere collider
            SphereCollider sphere = slot.AddComponent<SphereCollider>();
            sphere.isTrigger = true;
            sphere.radius = socketRadius;
        }

        fracturedInstance.transform.position = fracturedPosition.position;

        // Attach a mesh collider to each child and the xr grab interactable script
        foreach (GameObject piece in pieceList)
        {
            MeshCollider mesh = piece.AddComponent<MeshCollider>();
            mesh.convex = true;
            XRGrabInteractable xrGrab = piece.AddComponent<XRGrabInteractable>();

            // Enable dynamic attachment
            xrGrab.useDynamicAttach = true;
            xrGrab.snapToColliderVolume = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Get all children from the parent game object
    GameObject[] GetAllChildren(Transform root)
    {
        int childCount = root.childCount;
        GameObject[] allChildren = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            allChildren[i] = root.GetChild(i).gameObject;
        }
        return allChildren;
    }
}
