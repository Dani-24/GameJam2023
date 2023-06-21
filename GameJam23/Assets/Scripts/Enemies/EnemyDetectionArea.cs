using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionArea : MonoBehaviour
{
    Enemy1 enemyScript;

    [SerializeField]CircleCollider2D circleCollider;

    private void Start()
    {
        enemyScript = transform.parent.GetComponentInChildren<Enemy1>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tag in enemyScript.chaseTheseTags)
        {
            if (collision.CompareTag(tag))
            {
                enemyScript.SetTarget(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemyScript.currentlyTargeting != null)
        {
            if (collision.tag == enemyScript.currentlyTargeting.tag)
            {
               enemyScript.SetTarget(null);

                GameObject[] gameObjects = null;
                foreach (var tag in enemyScript.chaseTheseTags)
                {
                    gameObjects = GameObject.FindGameObjectsWithTag(tag);
                }

                for(int i = 0; i < gameObjects.Length; i++)
                {
                    if ((gameObjects[i].transform.position - enemyScript.transform.position).magnitude < circleCollider.radius)
                    {
                        enemyScript.SetTarget(gameObjects[i]);
                    }
                }
            }
        }
    }
}
