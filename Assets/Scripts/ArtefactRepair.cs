using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArtefactRepair : MonoBehaviour
{
    [SerializeField] GameObject fracturedPrefab;
    GameObject fracturedInstance;
    [SerializeField] GameObject completedPrefab;
    GameObject completedInstance;
    GameObject guideInstance;
    [SerializeField] Material outlineMaterial;
    [SerializeField] GameObject particleSystemPrefab;
    // Game object that holds the puzzle slots
    GameObject slotInstance;
    [SerializeField] Transform completedPosition;
    [SerializeField] Transform fracturedPosition;
    [SerializeField] float spacing;
    GameObject[] pieceList;
    GameObject[] slotList;
    int pieceCount;
    int correctPiecees = 0;
    [SerializeField] float socketRadius = 0.1f;

    void Start()
    {
        ResetPuzzle();
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

    void CompletePuzzle()
    {
        // Instantiate the completed prefab as a child of the table at the completed position
        completedInstance = Instantiate(completedPrefab, transform);
        completedInstance.transform.position = completedPosition.position;

        // todo: play completion sound or provide feedback for puzzle completion

        // Instantiate and play particle effect
        Instantiate(particleSystemPrefab, completedPosition.position, Quaternion.identity);

        // Destroy the fractured instance, slot instance and guide instance
        Destroy(fracturedInstance);
        Destroy(slotInstance);
        Destroy(guideInstance);
    }

    bool CompareIndices(GameObject piece, GameObject slot)
    {
        // Check if the piece is in the correct slot
        if (Array.IndexOf(pieceList, piece) == Array.IndexOf(slotList, slot))
        {
            // Increase the correct pieces count
            correctPiecees++;

            // Check if all pieces are in the correct slots
            if (correctPiecees >= pieceCount)
            {
                CompletePuzzle();
            }
            return true;
        }
        return false;
    }

    void OnObjectSnap(SelectEnterEventArgs args)
    {
        // Get the xr grab interactable component from the snapped object
        XRGrabInteractable grabbedObject = args.interactableObject as XRGrabInteractable;

        bool correctPiece = CompareIndices(grabbedObject.gameObject, args.interactorObject.transform.gameObject);

        // Disable the xr interactable/interactor components if the piece is correct to prevent further interaction
        if (correctPiece)
        {
            // Place the piece exactly at the slot position and rotation
            grabbedObject.transform.position = args.interactorObject.transform.position;
            grabbedObject.transform.rotation = args.interactorObject.transform.rotation;

            // Disable the socket interactor
            XRSocketInteractor socket = args.interactorObject as XRSocketInteractor;
            socket.enabled = false;

            // Disable the grab interactableon the piece
            grabbedObject.enabled = false;

            // Disable the rigidbody physics
            Rigidbody rb = grabbedObject.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = false;

            // Freeze the piece rotation and transform
            rb.constraints = RigidbodyConstraints.FreezeAll;

            // todo: Play success sound or provide feedback for correct placement
        }
        else
        {
            // todo: Play error sound or provide feedback for incorrect placement
        }
    }

    void OnObjectUnsnap(SelectExitEventArgs args)
    {
        // Get the xr grab interactable component from the snapped object
        XRGrabInteractable grabbedObject = args.interactableObject as XRGrabInteractable;
    }

    void DestroyInstances()
    {
        if (fracturedInstance != null)
            Destroy(fracturedInstance);
        if (slotInstance != null)
            Destroy(slotInstance);
        if (guideInstance != null)
            Destroy(guideInstance);
        if (completedInstance != null)
            Destroy(completedInstance);
    }

    public void ResetPuzzle()
    {
        // Destroy existing instances if they exist
        DestroyInstances();

        // Instantiate the completed prefab as a guide
        guideInstance = Instantiate(completedPrefab, transform);
        guideInstance.transform.position = completedPosition.position;

        // Replace the guide material with the outline material
        MeshRenderer renderer = guideInstance.GetComponentInChildren<MeshRenderer>();
        if (renderer != null)
            renderer.material = outlineMaterial;

        // Instantiate the fractured prefab as a child of the table
        fracturedInstance = Instantiate(fracturedPrefab, transform);
        fracturedInstance.transform.position = completedPosition.position;

        // Get all the individual pieces
        pieceCount = fracturedInstance.transform.childCount;
        pieceList = GetAllChildren(fracturedInstance.transform);
        slotList = new GameObject[pieceCount];

        // Create an empty game object where the pieces will go
        slotInstance = new GameObject("slots");
        slotInstance.transform.position = completedPosition.position;

        // Create child game objects for each slot and make them children of the slot instance
        for (int i = 0; i < pieceCount; i++)
        {
            GameObject slot = new GameObject("slot " + i);
            slot.transform.SetParent(slotInstance.transform);
            slot.transform.position = pieceList[i].transform.position;
            slot.transform.rotation = pieceList[i].transform.rotation;

            // Add the slot to the slot list
            slotList[i] = slot;

            // Add the XR socket interactor component
            XRSocketInteractor socket = slot.AddComponent<XRSocketInteractor>();

            // Subscribe to the select entered and exited events
            socket.selectEntered.AddListener(OnObjectSnap);
            socket.selectExited.AddListener(OnObjectUnsnap);

            // Add the sphere collider
            SphereCollider sphere = slot.AddComponent<SphereCollider>();
            sphere.isTrigger = true;
            sphere.radius = socketRadius;
        }

        // Reposition the fractured instance to the other side of the table
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
}
