using System.Collections;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    private bool canShoot = true;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float delayShoot = 2f;
    private bool direction;
    void Start()
    {
        direction = GameObject.Find("Player").GetComponent<PlayerController>().LookRight;
    }


    void Update()
    {
        if (canShoot & Input.GetKey(KeyCode.F))
        {
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        canShoot = false;
        Instantiate(bullet, transform.position + (direction ? Vector3.right : Vector3.left /5) , Quaternion.identity);
        yield return new WaitForSeconds(delayShoot);
        canShoot = true;
    }
}
