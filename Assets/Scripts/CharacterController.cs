using UnityEngine;

public class CharacterController : MonoBehaviour
{
    GameManager gameManager;
    public GameObject fightPlace;
    public float speed = .5f;

    [Header("EndGame")]
    public float fightCameraSmooth = 0.15f;
    public bool isEndGame;
    Camera _camera;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (isEndGame)
        {
            transform.position = Vector3.Lerp(transform.position, fightPlace.transform.position, fightCameraSmooth);
        }
        else
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
            gameManager.AgentSpawnManager(other.tag, number, other.transform);
        }        

        else if (other.CompareTag("EnemyVsTrigger"))
        {
            _camera.isEndGame = true;
            isEndGame = true;
            gameManager.EnemyTrigger();
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
