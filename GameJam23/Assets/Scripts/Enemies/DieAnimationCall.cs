using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAnimationCall : MonoBehaviour
{
    public Enemy1 Enemy1;

    void Die()
    {
        Enemy1.DeactivateEnemy();
    }
}
