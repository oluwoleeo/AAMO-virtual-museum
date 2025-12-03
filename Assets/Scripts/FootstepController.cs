using UnityEngine;
using UnityEngine.InputSystem;

public class FootstepController : MonoBehaviour
{
    private float minSpeed = 0.1f;

    [SerializeField]
    private AudioClip footstepClip;

    [SerializeField]
    private InputActionReference moveAction;

    private float stepInterval = 0.5f; // time between steps
    private float stepTimer = 0f;

    void OnEnable()
    {
        moveAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        bool isMoving = input.sqrMagnitude > minSpeed;

        if (isMoving)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                SoundEffectsManager.Instance.PlaySound(footstepClip);
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = stepInterval;
        }
    }
}

