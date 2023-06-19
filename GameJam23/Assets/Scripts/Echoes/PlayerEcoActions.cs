using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEcoActions 
{
    public Vector3 playerTrans;
    public Quaternion gunRot;
    public bool isShoot = false;
   // public Quaternion gunTrans;
   // public Gun gunAction;
     public PlayerEcoActions (Vector3 _playerPos, Quaternion _gunRot, bool _isShoot)
    {
        this.playerTrans = _playerPos;
        this.gunRot = _gunRot;
        this.isShoot = _isShoot;
       // this.gunAction = _gunAction;
       // this.gunTrans = _gunTrans;
    }

}
