using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    List<string> CollideWithTheseTags = new List<string>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tag in CollideWithTheseTags)
        {
            if (collision.gameObject.CompareTag(tag) && !collision.isTrigger)
            {
               Destroy(gameObject);
            }
        }
    }
}