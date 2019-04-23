using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreBoundary : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }
    }
}
