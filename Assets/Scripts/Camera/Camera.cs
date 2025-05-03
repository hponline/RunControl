using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform targetCharacter;
    public Vector3 targetOffset;
    public GameObject endCamera;
    public bool isEndGame;

    private void Start()
    {
        targetOffset = transform.position - targetCharacter.position;
    }
    private void LateUpdate()
    {
        if (!isEndGame)
            transform.position = Vector3.Lerp(transform.position, targetCharacter.position + targetOffset, .125f);
        else
            transform.position = Vector3.Lerp(transform.position, endCamera.transform.position, .015f);
    }
}
