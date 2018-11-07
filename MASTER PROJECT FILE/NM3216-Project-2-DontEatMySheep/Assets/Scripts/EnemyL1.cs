using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyL1 : MonoBehaviour
{

    public float health = 10.0f;
    public float moveSpeed = 0.05f;
    public float wallDamage = 10.0f;
    public Animator animator;
    public int enemyReward = 1;

    public bool shouldMove = true;
    public bool isDead = false;
    public bool isEnemyAttacking = false;

    public float attackRestTime = 10.0f;

    private GameObject prevWall;

    public Vector2 centerOfMap = new Vector2(0, 0);

    //	public void start() {
    //		playerGold = GameObject.Find ("shepherd").GetComponent<Player> ().gold;
    //	}
    public void Update()
    {
       if(prevWall == null)
        {
           // shouldMove = true;
            animator.SetBool("isEnemyAttacking", false);
        }

        if (shouldMove)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), centerOfMap, moveSpeed * Time.deltaTime);
        }
        LookAt();
    }

    public void OnAttack(bool isAttack)
    {
        isEnemyAttacking = isAttack;
        Debug.Log(isEnemyAttacking);
        animator.SetBool("isEnemyAttacking", isAttack);
    }

    void LookAt()
    {
        Vector3 center = new Vector3(0.02f, -0.18f, 0f);
        Vector3 targetVector = transform.position - center;
        float step = 100 * Time.deltaTime;
        transform.up = Vector2.MoveTowards(transform.up, targetVector, step);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0.0f)
        {
            Player.UpdateGold(enemyReward);
            Die();
            Debug.Log("KaChing KaChing!");
        }
    }

    void Die()
    {
        Destroy(gameObject);
        isDead = true;
        //SpawnController.killedEnemy ();
    }
    //	public void OnTriggerEnter2D (Collider2D other){
    //		if (other.gameObject.tag == "Wall") {
    //			wall = other.gameObject;
    //			StartCoroutine ("AttackWall");
    //			//other.gameObject.GetComponent<Wall> ().DamageToWall (wallDamage);
    //			//Destroy(other.gameObject);
    //		}
    //	}
    //	public void OnTriggerExit2D (Collider2D other){
    //		if (other.gameObject.tag == "Wall") {
    //			StopCoroutine ("AttackWall");
    //			//other.gameObject.GetComponent<Wall> ().DamageToWall (wallDamage);
    //		}
    //	}
    //
    //	IEnumerator AttackWall()  {
    //		while (true) {
    //			wall.GetComponent<Wall> ().DamageToWall(wallDamage);
    //			Debug.Log ("Wall is under siege!");
    //			yield return new WaitForSeconds (attackRestTime);
    //		}
    //	}

    public void AttackWall(GameObject wall)
    {
        wall.GetComponent<Wall>().DamageToWall(wallDamage);
        prevWall = wall;
        //Debug.Log ("Wall is under siege!");
    }

    //why are the enemies not taking damage
}
