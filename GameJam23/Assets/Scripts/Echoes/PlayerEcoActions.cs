using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEcoActions 
{
    public Transform playerTrans;
    public Transform gunTrans;
    public Quaternion gunRot;
    public Gun gunAction;
    public bool isShoot = false;
     public PlayerEcoActions (Transform _playerTrans, Transform _gunTrans, bool _isShoot)
    {
        this.playerTrans = _playerTrans;
       // this.gunAction = _gunAction;
        this.gunTrans = _gunTrans;
        this.gunRot = _gunTrans.rotation;
        this.isShoot = _isShoot;
    }

}
