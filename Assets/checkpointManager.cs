using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private GameObject lastCheckpoint;
    public GameObject LastCheckpoint { get => lastCheckpoint; set => lastCheckpoint = value; }

    void Start()
    {
        LastCheckpoint = GameObject.Find("Flag1");
        DeactivateAll();
        lastCheckpoint.GetComponent<Animator>().SetBool("activated", true);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Respawn"){
            LastCheckpoint = collision.gameObject;
        }
    }
     public void Respawn()
    {
        if (LastCheckpoint != null)
        {
            transform.position = LastCheckpoint.transform.position;
        }
        else
        {
            Debug.Log("Нет сохраненного чекпоинта, перезапуск уровня.");
        }
    }
    public void DeactivateAll()
    {
        GameObject [] mass = GameObject.FindGameObjectsWithTag("Respawn");
        foreach (GameObject obj in mass)
        {
            if(obj != LastCheckpoint)
            {
                obj.GetComponent<Checkpoint>().Deactivate();
                obj.GetComponent<Checkpoint>().Activated = false;
            }
        }
    }
}
