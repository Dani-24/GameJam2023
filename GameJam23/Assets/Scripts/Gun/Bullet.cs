using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    List<string> CollideWithTheseTags = new List<string>();

    private void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tag in CollideWithTheseTags)
        {
            if (collision.CompareTag(tag))
            {
               Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var tag in CollideWithTheseTags)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                Destroy(gameObject);
            }
        }
    }
}