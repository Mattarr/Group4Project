using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PunchScript : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 1;
    public AudioClip hitSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        //lets player punch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Attack();
        }
    }

    //punch animation plays when triggered
    void Attack()
    {
        animator.SetTrigger("Attack");
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit" + enemy.name);

            if (enemy.CompareTag("GroundEnemy"))
            {
                PlaySound(hitSound);
                enemy.GetComponent<GE>().TakeDamage(attackDamage);
            }

            else if(enemy.CompareTag("FlyingEnemy"))
            {
                PlaySound(hitSound);
                enemy.GetComponent<flyingBehavior>().TakeDamage(attackDamage);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
