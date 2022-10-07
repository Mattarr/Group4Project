using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundEnemyBehavior : MonoBehaviour
{
    public float speed;
    private bool movingRight = true;
     [SerializeField] 
    private PlayerHealth Boxer;
    public Transform groundDetection;
    public int enemyHealth;
    public int enemyPower;
   

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = 3;
        enemyPower = 1;
       

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * -speed* Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        if(groundInfo.collider == false){

            if(movingRight == true){
                transform.eulerAngles = new Vector3( 0, -180, 0);
                movingRight = false;
            }else{
                    transform.eulerAngles = new Vector3(0,0,0);
                    movingRight = true;

            }
        }
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
    public void TakeDamage(int damage){
        enemyHealth = enemyHealth - damage;
        

        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Ground enemy Died!");
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject);
    }
}
