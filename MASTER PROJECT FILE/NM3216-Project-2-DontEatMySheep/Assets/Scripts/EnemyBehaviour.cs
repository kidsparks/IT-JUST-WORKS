using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    GameObject wall;
    GameObject player;
    //Vector2 currentPos;

    public float attackRestTime = 1.0f;

    EnemyL1 parent;

    //	// Use this for initialization
    void Start()
    {
        parent = transform.parent.gameObject.GetComponent<EnemyL1>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            wall = other.gameObject;
            //currentPos = new Vector2 (transform.position.x, transform.position.y);
            StartCoroutine("AttackWall");
            parent.shouldMove = false;
            //other.gameObject.GetComponent<Wall> ().DamageToWall (wallDamage);
            //Destroy(other.gameObject);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            StopCoroutine("AttackWall");
            transform.parent.GetComponent<EnemyL1>().OnAttack(false);
            parent.shouldMove = true;
            //other.gameObject.GetComponent<Wall> ().DamageToWall (wallDamage);
        }
    }
    private void Update()
    {
        if (wall == null)
        {
            parent.shouldMove = true;
        }
    }

    IEnumerator AttackWall()
    {
        while (true)
        {
            //transform.position = currentPos;
            if (wall != null)
            {
                parent.AttackWall(wall);
                transform.parent.GetComponent<EnemyL1>().OnAttack(true);
            }
            //transform.parent.GetComponent<EnemyL1> ().OnAttack(false);

            yield return new WaitForSeconds(attackRestTime);
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            wall = other.gameObject;
            //currentPos = new Vector2 (transform.position.x, transform.position.y);
            //StartCoroutine("AttackWall");
            parent.shouldMove = false;
            //other.gameObject.GetComponent<Wall> ().DamageToWall (wallDamage);
            //Destroy(other.gameObject);
        }
    }
}
