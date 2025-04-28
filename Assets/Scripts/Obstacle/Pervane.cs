using System.Collections;
using UnityEngine;

public class Pervane : MonoBehaviour
{
    public Animator animator;
    public BoxCollider wind;
    public float duration;

    public void AnimationState(string state)
    {
        if (state == "true")
        {
            animator.SetBool("isBool", true);
            wind.enabled = true;
        }
        else
        {
            animator.SetBool("isBool", false);
            wind.enabled = false;
            StartCoroutine(AnimationPlay());
        }
    }

    IEnumerator AnimationPlay()
    {
        yield return new WaitForSeconds(duration);
        AnimationState("true");
    }
}
