using System;
using System.Collections;
using UnityEngine;


enum EnemyState
{
    None,
    ChasePlayer,
    Attack,
    WaitForAttack
}

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public int speed = 1;

    public bool inZone = false;

    private EnemyState state = EnemyState.None;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (inZone == false)
        {
            // chase after player
            state = EnemyState.ChasePlayer;
            
            Vector3 distance = target.transform.position - transform.position;

            transform.position += distance.normalized * (speed * Time.deltaTime);

            if (distance.normalized.x > 0)
            {
                transform.localScale = new Vector3(
                    Vector3.left.x * Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z);
            }
            else 
            {
                transform.localScale = new Vector3(
                    Vector3.right.x * Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z);
            }
        }
        else if (state == EnemyState.ChasePlayer) // reach to the player
        {
            StartCoroutine(AttackAndWaitLoop());
        }
        
    }

    IEnumerator AttackAndWaitLoop()
    {
        while (true)
        {
            state = EnemyState.Attack;
            Debug.Log("Attacking the player");
            yield return new WaitForSeconds(2.0f);

            state = EnemyState.WaitForAttack;
            Debug.Log("Waiting....");
            yield return new WaitForSeconds(2.0f);
        }
    }
}
