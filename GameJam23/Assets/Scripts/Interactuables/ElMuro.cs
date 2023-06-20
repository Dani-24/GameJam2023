using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElMuro : MonoBehaviour
{
    [Header("If Activate Things selected")]
    [SerializeField]
    List<GameObject> thingsToActivate;
    private int activablesCount = 0;
    private bool activatedWall = true;
    //private bool isDesactived = false;

    //[SerializeField] private float disapearTime = 2f;
    //private float _disapearDt = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        activablesCount = 0;
        transform.GetChild(0).gameObject.SetActive(activatedWall);
        for (int i = 0; i < thingsToActivate.Count; i++)
        {
            if (thingsToActivate[i].GetComponent<InteractuableItem>().activated)
            {
                activablesCount++;
            }
        }
        if (activablesCount >= thingsToActivate.Count)
        {
            activatedWall = false;
           //isDesactived = true;

        }
        else
        {
            activatedWall = true;
            
        }

        //if(isDesactived)
        //{
        //    if(_disapearDt>=disapearTime)
        //    {

        //    }
        //}
    }
}
