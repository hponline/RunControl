using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    GameManager gameManager;
    public float speed = .5f;


    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetAxis("Mouse X") < 0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3
                    (transform.position.x - .1f, transform.position.y, transform.position.z), .3f);
            }
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3
                    (transform.position.x + .1f, transform.position.y, transform.position.z), .3f);
            }            
        }
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "X2" || other.name == "+3" || other.name == "-4" || other.name == "/2")
        {
            gameManager.AgentSpawnManager(other.name, other.transform);            
        }
    }
}
