using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0.2f;
    public int damage;
    private Transform target;

    public void StartMove(Transform target)
    {
        this.target = target;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance < 0.1f)
        {
            target.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
        Vector2 direction = target.position - transform.position;
        direction = direction.normalized;
        transform.Translate(direction * speed * Time.deltaTime);
        yield return new WaitForFixedUpdate();
        StartCoroutine(Move());
    }
}
