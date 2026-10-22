using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementHandler : MonoBehaviour
{
    private Vector2 aimDir = Vector2.zero;
    [Header("Movement Values")]
    public float moveSpeed = 5f;
    public Vector2 moveDir;

    //public AnimatorHandler animHandler;
    public enum Dir
    {
        North, South, East, West, NorthEast, SouthEast, NorthWest, SouthWest, Zero
    }
    private Dictionary<float, Dir> dirDict;
    private Dir facingDir;
    public int facingVal;
    private Rigidbody2D rb;
    private InputSystem_Actions inputSystem;
    public Vector2 facingdir = Vector2.zero;


    public Dir GetPlayerDirection()
    {
        return facingDir;

    }
    private void Awake()
    {


        dirDict = new Dictionary<float, Dir>  {
             {90.0f,Dir.North },    // when calculating within the range, it will be min inclusive, max exclusive 
             {135.0f,Dir.NorthWest },
             {180.0f,Dir.West},
             {-45.0f,Dir.SouthEast},
             {-90.0f,Dir.South},
             {-135.0f,Dir.SouthWest},
             {0.0f,Dir.East},
             {45.0f,Dir.NorthEast},
             //(30.0f, 60.0f, Dir.NorthEast),
        };
        inputSystem = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
        //check that the player has a rigibody component on the same object that this script is on
        Assert.IsTrue(rb != null, "Rigidbody 2D component needed to use this script!");
    }

    private void OnEnable()
    {
        inputSystem.Player.Move.performed += OnMove;
        inputSystem.Player.Move.canceled += OnMoveEnd;
        inputSystem.Enable();
    }

    private void OnDisable()
    {
        inputSystem.Player.Move.performed -= OnMove;
        inputSystem.Player.Move.canceled -= OnMoveEnd;
        inputSystem.Disable();
    }
    //public Vector2 GetAimDir()
    //{
    //    Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    mouseWorld.z = 0f;
    //    return ((Vector2)mouseWorld - (Vector2)transform.position).normalized;
    //}
    private void OnMove(InputAction.CallbackContext context)
    {

        moveDir = context.ReadValue<Vector2>().normalized;
    }

    public Vector2 GetFacingDir()
    {
        return facingdir;
    }


    private void OnMoveEnd(InputAction.CallbackContext context)
    {
        moveDir = Vector2.zero;
        //if (animHandler) // animation direction logic for now 
        //{

        //    animHandler.UpdateAnimation(moveDir,aimDir);


        //    //animHandler.TriggerRunning((int)MathF.Floor(moveDir.x), (int)MathF.Floor(moveDir.y)); // set trigger for moving 

        //    //animHandler.TriggerRunning((int)moveDir.x, (int)moveDir.y); // set trigger for moving


        //}
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDir * moveSpeed;
    }
    private void Update()
    {
        //aimDir = GetAimDir();
    }
}