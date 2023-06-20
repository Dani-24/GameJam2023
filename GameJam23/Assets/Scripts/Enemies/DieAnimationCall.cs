using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAnimationCall : MonoBehaviour
{
    public Enemy1 Enemy1;

    public void DeactivateCollider()
    {
        Enemy1.DeactivateCollider();
    }

    public void Die()
    {
        Enemy1.DeactivateEnemy();
    }
}
