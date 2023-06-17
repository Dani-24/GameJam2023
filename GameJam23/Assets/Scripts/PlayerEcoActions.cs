using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class PlayerEcoActions : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
public class PlayerEcoActions 
{
    Transform playerTrans;
    Transform gunTrans;
    Quaternion gunRot;
    Gun gunAction;
    bool isShoot = false;
     public PlayerEcoActions (Transform _playerTrans, Transform _gunTrans, bool _isShoot)
    {
        this.playerTrans = _playerTrans;
       // this.gunAction = _gunAction;
        this.gunTrans = _gunTrans.transform;
        this.gunRot = _gunTrans.rotation;
        this.isShoot = _isShoot;
    }

}
