using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public float wallHealth = 50.0f;
    //public float cost = 5.0f,to be implemented later
    public Animator animator;
    private void Start()
    {
        //animator.SetFloat("wallHealth", wallHealth);
    }
    public void DamageToWall(float amount) {
        wallHealth -= amount;
        if (wallHealth <= 0.0f) {
            Destroy(gameObject);
        }
      
    
        //animator.SetFloat("wallHealth",amount);
    }   
}
