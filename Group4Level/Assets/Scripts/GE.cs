using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GE : MonoBehaviour
{
   public float min=1f;
    public float max=1.5f;
     [SerializeField] 
    private PlayerHealth Boxer;
    public int enemyHealth;
    public int enemyPower;
    // Use this for initialization
    void Start () {
       
        min=transform.position.x;
        max=transform.position.x+.5f;
   
    }
   
    // Update is called once per frame
    void Update () {
       
       
        transform.position =new Vector3(Mathf.PingPong(Time.time*.5f,max-min)+min, transform.position.y, transform.position.z);
       
    }
    void OnCollisionEnter2D(Collision2D col){
               
                    if(col.gameObject.tag.Equals ("Player")){

                        
                                
                                Boxer.playerHealth = Boxer.playerHealth-enemyPower;
                                   Debug.Log("COLLIDED!----PLAYER HEALTH: "+ Boxer.playerHealth);
                                  
                                if(Boxer.playerHealth <= 0){
                                    //Destroy (GameObject.FindWithTag("Player"));
                                    Debug.Log("PLAYER DIED");
                                }
                    }//else if collided with punch then enemy health - enemy power.
    }

    
 public void TakeDamage(int damage)
            {
                enemyHealth = enemyHealth - damage;
                
                if (enemyHealth <= 0)
                {
                    Die();
                }

                void Die()
                {
                    Debug.Log("walking Enemy has died!");
                    this.enabled = false;
                    GetComponent<Collider2D>().enabled = false;
                    Destroy(this.gameObject);

                }
            }

            
}
