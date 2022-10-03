using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour
{
    public int enemyPower;
    public int enemyHealth;
    public int playerHealth;
   // Rigidbody2D enemy;
    // Start is called before the first frame update
    void Start()
    {
       // enemy = GetComponent<Rigidbody2D>();
        enemyPower = 1;
        enemyHealth = 3;

        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void enemyTakeDamage(){
           enemyHealth = enemyHealth -1;
        if(enemyHealth<= 0){
            Destroy(this.gameObject);

        }
    }
   private void onCollisionEnter2D(Collision2D collision){

if(collision.gameObject.tag =="Player"){

            enemyTakeDamage();
        }
    }
}
