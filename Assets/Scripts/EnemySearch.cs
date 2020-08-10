using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    public List<Transform> EnemyiesInRange = new List<Transform>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyiesInRange.Add(other.transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       EnemyiesInRange.Remove(other.transform);
    }
}
