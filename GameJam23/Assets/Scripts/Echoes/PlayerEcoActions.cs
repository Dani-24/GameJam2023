using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEcoActions 
{
    public Vector3 playerTrans;
    public Quaternion gunRot;
    public bool isShoot = false;
    public Vector2 mousePos;
    public Vector2 input;
    // public Quaternion gunTrans;
    // public Gun gunAction;
    public PlayerEcoActions (Vector3 _playerPos, Quaternion _gunRot, bool _isShoot, Vector2 _mousePos, Vector2 _input)
    {
        this.playerTrans = _playerPos;
        this.gunRot = _gunRot;
        this.isShoot = _isShoot;
        this.mousePos = _mousePos;
        this.input = _input;
       // this.gunAction = _gunAction;
       // this.gunTrans = _gunTrans;
    }

}
