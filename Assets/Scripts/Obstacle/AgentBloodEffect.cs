using System.Collections;
using UnityEngine;

public class AgentBloodEffect : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }    
}
