using System.Collections;
using UnityEngine;

public class ExhibitAnimation : MonoBehaviour
{
    int currentState = 0;
    [SerializeField] int maxState = 2;
    Animator animator;
    [SerializeField] float minTime = 5f;
    [SerializeField] float maxTime = 10f;
    bool checkTransition = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        SetWaitTime();
    }

    void Update()
    {
        // Only call UpdateState when the animator is not in transition
        if (checkTransition)
        {
            if (!animator.IsInTransition(0))
            {
                checkTransition = false;
                SetWaitTime();
            }
        }
    }

    void UpdateState()
    {
        currentState++;
        if (currentState > maxState)
            currentState = 0;

        animator.SetInteger("currentState", currentState);

        // Check if the animator is transitioning in the Update method
        StartCoroutine(SetTransitionCheck());
    }

    void SetWaitTime()
    {
        float waitTime = Random.Range(minTime, maxTime);

        // Ensure the full roar animation plays before switching states
        if (currentState == maxState)
        {
            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
            float animDuration = state.length / state.speed;
            float cycles = Mathf.Floor(waitTime / animDuration) + 1;

            // Remove the time already spent in the current animation cycle from the wait time
            float currentProgress = state.normalizedTime % 1;
            waitTime = cycles * animDuration - (currentProgress * animDuration);
        }
        Invoke("UpdateState", waitTime);
    }

    IEnumerator SetTransitionCheck()
    {
        yield return null;
        checkTransition = true;
    }
}