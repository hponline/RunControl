using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public GameObject fightPlace;
    public float speed = .5f;

    [Header("EndGame")]
    public float fightCameraSmooth = 0.15f;
    public bool isEndGame;
    Camera _camera;

    [Header("UI")]
    public Slider sliderProgresBar;
    public GameObject sliderTarget;

    private void Start()
    {
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        // Progres bar
        float distance = Vector3.Distance(transform.position, sliderTarget.transform.position);
        sliderProgresBar.maxValue = distance;
    }

    private void Update()
    {
        if (isEndGame)
        {
            transform.position = Vector3.Lerp(transform.position, fightPlace.transform.position, fightCameraSmooth);
            if (sliderProgresBar.value != 0)
            {
                sliderProgresBar.value -= .005f;
            }
        }
        else
        {
            // Progres bar
            float distance = Vector3.Distance(transform.position, sliderTarget.transform.position);
            sliderProgresBar.value = distance;

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
        
    }
    private void FixedUpdate()
    {
        if (!isEndGame)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Toplama") || other.CompareTag("Cikarma") || other.CompareTag("Carpma") || other.CompareTag("Bolme"))
        {
            int number = int.Parse(other.name); // Objenin string ismini sayýya çevirir
            GameManager.gameManagerInstance.AgentSpawnManager(other.tag, number, other.transform);
        }        

        else if (other.CompareTag("EnemyVsTrigger"))
        {
            _camera.isEndGame = true;
            isEndGame = true;
            GameManager.gameManagerInstance.EnemyTrigger();
        }

        else if (other.CompareTag("FreeNpc"))
        {
            GameManager.gameManagerInstance.agentObjectPool.Add(other.gameObject);            
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Direk") || other.CompareTag("Enemy"))
        {
            if (transform.position.x > 0)
                transform.position = new Vector3(transform.position.x - .2f, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z);
        }
    }
}
