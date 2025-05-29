using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    [SerializeField] private int HP = 3;
    private int currentHP;
    [SerializeField] private GameObject prefabHP;
    private Animator animator;
    private Coroutine runable;
    private List<GameObject> hpIcons = new List<GameObject>();

    private void initHP()
    {
        animator = GetComponent<Animator>();
        currentHP = HP;
        runable = null;
        hpIcons.Clear();

        int countSideEl = HP / 2;
        int[] massOffset = new int[HP];
        for (int i = -countSideEl, j = 0; i <= countSideEl; i++, j++)
        {
            massOffset[j] = i;
        }

        for (int i = 0; i < HP; i++)
        {
            GameObject hp = Instantiate(prefabHP, transform);
            hp.transform.localPosition = new Vector3(massOffset[i] * 2f, 4f, 0);
            hpIcons.Add(hp);
        }
    }

    private void clearHP()
    {
        foreach (GameObject hp in hpIcons)
        {
            Destroy(hp);
        }
        hpIcons.Clear();
    }

    void Start()
    {
        initHP();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("TriggerStay with: " + collision.gameObject.tag);

        if ((collision.gameObject.CompareTag("Damage") || collision.gameObject.CompareTag("Enemy")) && runable == null)
        {
            if (currentHP > 0)
            {
                GameObject lastHP = hpIcons[currentHP - 1];
                if (lastHP != null)
                {
                    lastHP.GetComponent<HPAnimationController>().DestroyAnim();
                    runable = StartCoroutine(destroyHp(lastHP));
                    Debug.Log("HP lost. Current HP: " + currentHP);
                }
                else
                {
                    Debug.LogWarning("HP icon at index is null.");
                }
            }
        }
        else if (currentHP == 0 || collision.gameObject.name == "OutOfBounds")
        {
            Debug.Log("Player died or fell into DeadZone. Respawning...");
            clearHP();
            transform.position = GetComponent<CheckpointManager>().LastCheckpoint.transform.position + Vector3.up;
            initHP();
        }
    }

    IEnumerator destroyHp(GameObject obj)
    {
        currentHP--;
        yield return new WaitForSeconds(0.6f);
        hpIcons.Remove(obj);
        Destroy(obj);
        runable = null;
    }
}
