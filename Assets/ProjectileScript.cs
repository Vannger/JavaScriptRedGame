using Mono.Cecil.Cil;
using UnityEngine;

public class BulletContoller : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 3f;
    private Camera camera;
    private bool direction = false;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        direction = player.GetComponent<PlayerController>().LookRight;
        camera = UnityEngine.Camera.main;

    }

    void Update()
    {
        transform.Translate(new Vector3(bulletSpeed * (direction ? 1 : -1), 0, 0) * Time.deltaTime);

        Vector3 viewPos = camera.WorldToViewportPoint(transform.position);
        if (viewPos.x < 0 || viewPos.x > 1)
        {
            Debug.Log("Destroy bullet");
            Destroy(gameObject);
        }
        
    }
}
