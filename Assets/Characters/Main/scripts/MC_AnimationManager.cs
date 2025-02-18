using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MC_AnimationManager : MonoBehaviour
{
    // Singleton Design
    private static MC_AnimationManager _instance;
    public static MC_AnimationManager Instance => _instance;

    private Animator animator;

    private void Awake()
    {
        // Singleton pattern with explicit null check
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Get Animator component
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError($"{nameof(Animator)} component not found on the GameObject.");
        }
    }

    private void Update()
    {

    }

    // Set an integer parameter in the Animator
    public void SetInt(string parameterName, int value)
    {
        if (animator != null)
        {
            animator.SetInteger(parameterName, value);
        }
        else
        {
            Debug.LogWarning($"{nameof(Animator)} not set in {nameof(MC_AnimationManager)}.");
        }
    }

    // Set a float parameter in the Animator
    public void SetFloat(string parameterName, float value)
    {
        if (animator != null)
        {
            animator.SetFloat(parameterName, value);
        }
        else
        {
            Debug.LogWarning($"{nameof(Animator)} not set in {nameof(MC_AnimationManager)}.");
        }
    }

    // Set a boolean parameter in the Animator
    public void SetBool(string parameterName, bool value)
    {
        if (animator != null)
        {
            animator.SetBool(parameterName, value);
        }
        else
        {
            Debug.LogWarning($"{nameof(Animator)} not set in {nameof(MC_AnimationManager)}.");
        }
    }

    // Trigger a parameter in the Animator
    public void SetTrigger(string parameterName)
    {
        if (animator != null)
        {
            animator.SetTrigger(parameterName);
        }
        else
        {
            Debug.LogWarning($"{nameof(Animator)} not set in {nameof(MC_AnimationManager)}.");
        }
    }

    // Get the name of the currently playing animation asynchronously
    public string GetCurrentAnimationName()
    {
        int layerIndex = 0;
        if (animator == null)
        {
            Debug.LogWarning($"{nameof(Animator)} not set in {nameof(MC_AnimationManager)}.");
            return null;
        }


        // Wait for the Animator to finish transitioning
        if(animator.IsInTransition(layerIndex))
        {
            return "transition";
        }

        // Get the current AnimatorStateInfo
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(layerIndex);
        if (clipInfo.Length > 0)
        {
            return clipInfo[0].clip.name;
        }

        return null; // No animation found
    }
}
