using System;
using UnityEngine;

public class ZoneDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerZone"))
        {
            GetComponentInParent<Enemy>().inZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerZone"))
        {
            GetComponentInParent<Enemy>().inZone = false;
        }
    }
}
