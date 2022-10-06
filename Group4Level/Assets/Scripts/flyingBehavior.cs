using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingBehavior : MonoBehaviour
{

    public GameObject player1;
    public float speed;
    private float distance;
    public Transform player;
    private float followRange;
    private bool collided;
       [SerializeField] 
    private PlayerHealth Boxer;
    public int enemyPower;
        Rigidbody2D enemy;
        Vector3 initPos;
       
    // Start is called before the first frame update
    void Start()
    {
       
        enemy = GetComponent<Rigidbody2D>();
        followRange = 3f;
        collided = false;
        speed = 1f;
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()

    {
        float distancePlayer = Vector2.Distance(transform.position, player.position);

        if(collided == true){
    pushedBack();
        }


            else if(distancePlayer < followRange){

            distance = Vector2.Distance(transform.position,player1.transform.position);
             Vector2 direction =player1.transform.position -transform.position;

             transform.position = Vector2.MoveTowards(this.transform.position, player1.transform.position, speed* Time.deltaTime);
        }else {

            
        upDown();
        }
    }

    //------------------------

    void upDown(){

transform.position = new Vector3( initPos.x, Mathf.Sin(Time.time)*1f + initPos.y,0);
 



    }


    //-------------------
           void pushedBack(){
                float distancePush = Vector2.Distance(transform.position, player.position);
                if (distancePush <=2){
                       
              transform.position = Vector2.MoveTowards(this.transform.position, player1.transform.position, -speed* Time.deltaTime);
                    
                     }else{

        collided = false;
    }
            }

            
            void OnCollisionEnter2D(Collision2D col){
               
                    if(col.gameObject.tag.Equals ("Player")){

                        
                                
                                Boxer.playerHealth = Boxer.playerHealth-enemyPower;
                                   Debug.Log("COLLIDEDDDDDDDD!----PLAYER HEALTH: "+ Boxer.playerHealth);
                                    collided = true;
                                if(Boxer.playerHealth <= 0){
                                    //Destroy (GameObject.FindWithTag("Player"));
                                    Debug.Log("PLAYER DIED");
                                }
                    }//else if collided with punch then enemy health - enemy power.

            }

}
