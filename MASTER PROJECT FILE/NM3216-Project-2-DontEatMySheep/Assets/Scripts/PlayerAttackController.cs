using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public Player player;
    List<EnemyL1> enemies = new List<EnemyL1>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemies.Add(other.gameObject.GetComponent<EnemyL1>());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemies.Remove(other.gameObject.GetComponent<EnemyL1>());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("ATTACK " + enemies.Count);
            transform.parent.GetComponent<RotationController>().OnAttack(true);


            for (int i = 0; i < enemies.Count; i++)
            {
                EnemyL1 enemy = enemies[i];
                if (enemy == null)
                {
                    enemies.Remove(enemy);
                    i--;
                    continue;
                }
                enemy.TakeDamage(player.playerDamage);
            }
        }
        else
        {
            transform.parent.GetComponent<RotationController>().OnAttack(false);
        }
    }
}
