using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
[Header("Movement")]
    public float enemySpeed = 2f;
    Rigidbody2D enemy;
    private float enemyPosition;
    private float enemyPatrolDistanceRight;
     private float enemyPatrolDistanceLeft;
     private float enemyDistanceTravel;
     [Header("Follow")]
     public Transform player;
     public float followRange;


    void Start()
    {
        
        enemy = GetComponent<Rigidbody2D>();
        enemyPosition = enemy.position.x;
        enemyDistanceTravel = 2f;
        enemyPatrolDistanceRight = enemyPosition + enemyDistanceTravel;
        enemyPatrolDistanceLeft = enemyPosition - enemyDistanceTravel;
          followRange = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        //distance between player and enemy
        float distancePlayer = Vector2.Distance(transform.position, player.position);
        
        if(distancePlayer < followRange){
            //Chase player
            followPlayer();

        }else{


        
//WHILE PLAYER IS NOT CLOSE TO ENEMY THEN IDLE BACK AND FORTH 
      stopFollowing();
      backForth();
    
    }




    }
private void followPlayer(){

if(transform.position.x < player.position.x){
//
    enemy.velocity = new Vector2(enemySpeed +.5f, 0);
    transform.localScale = new Vector2(1,1);

}else if (transform.position.x > player.position.x){

    enemy.velocity = new Vector2(-(enemySpeed+.5f),0);
     transform.localScale = new Vector2(-1,1);
}
}
private void stopFollowing(){

enemy.velocity = new Vector2(0,0);
}

    private void backForth(){
   if(isFacingRight()){
      
        //move right
        enemy.velocity= new Vector2(enemySpeed, 0f);

      //update enemy position
        enemyPosition = enemy.position.x;

    //when enemy travels certain disrance turn around
        if(enemyPosition >=  enemyPatrolDistanceRight){
            //changes boolean to make it go left
               transform.localScale =  new Vector2(-(Mathf.Sign(enemy.velocity.x)), transform.localScale.y);
        }
                }else{
                    //move left
             enemy.velocity= new Vector2(-enemySpeed, 0f);

        

         enemyPosition = enemy.position.x;
    // when enemy reaches certain left side distance move right
        if(enemyPosition <=  enemyPatrolDistanceLeft){
  
            // changes boolean to make it go right
               transform.localScale =  new Vector2(-(Mathf.Sign(enemy.velocity.x)), transform.localScale.y);
        }
       }

    }
    private bool isFacingRight()
    {

        return transform.localScale.x > Mathf.Epsilon;

    }
    //when you hit an object turn the other way
   private void OnTriggerExit2D( Collider2D collision){

    transform.localScale = new Vector2(-(Mathf.Sign(enemy.velocity.x)), transform.localScale.y);

   }
    
}
