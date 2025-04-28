using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform targetCharacter;
    public Vector3 targetOffset;

    private void Start()
    {
        targetOffset = transform.position - targetCharacter.position;
    }
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetCharacter.position + targetOffset, .125f);
    }
}
