using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public int speed = 1;

    public bool inZone = false;
    private void Update()
    {
        if (inZone == false)
        {
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
        
       
    }
}
