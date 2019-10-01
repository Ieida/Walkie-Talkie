using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerControllerReferences
{
    public Rigidbody bodyRB;
    public Rigidbody camHolderRB;
    public Transform bodyT;
    public Transform camHolderT;
    public float mouseSensitivity;
    // public float clampX;
    // public float movementSpeed;
    public float movementForce;
    public float rbInterp;
    public float jumpForce;
    public float jumpInterp;
    // [System.NonSerialized] public float x;
    // [System.NonSerialized] public float y;
    // [System.NonSerialized] public float z;
    [System.NonSerialized] public float camX;
    [System.NonSerialized] public float camY;
    [System.NonSerialized] public float jump;
}

public class PlayerBehaviour : MonoBehaviour
{
    public PlayerControllerReferences cr;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        cr.jump = Mathf.Lerp(cr.jump, Input.GetKey(KeyCode.Space) ? 1.0f:0.0f, Time.deltaTime * cr.jumpInterp);
        Look();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Look()
    {
        //look behaviour
        float x = cr.camX;
        float y = cr.camY;
        x = x + -Input.GetAxis("Mouse Y") * cr.mouseSensitivity;
        y = y + Input.GetAxis("Mouse X") * cr.mouseSensitivity;
        cr.camHolderT.rotation = Quaternion.Euler(x, y, 0.0f);
        cr.camX = x;
        cr.camY = y;
    }

    void Move()
    {
        //movement behaviour
        Vector3 localDir = cr.camHolderT.rotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        cr.bodyRB.AddForce(localDir * cr.movementForce);
        cr.bodyRB.AddForce(cr.camHolderT.forward * (cr.jump * cr.jumpForce));
        //sync camera with body
        cr.camHolderRB.MovePosition(new Vector3(cr.bodyT.position.x, cr.bodyT.position.y + 0.85f, cr.bodyT.position.z));
    }
}
