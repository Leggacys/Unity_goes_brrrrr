using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem.Controls;


public class PlayerManager : MonoBehaviour
{
    #region Singletone

    public static PlayerManager instace;
    
    private void Awake()
    {
        if (instace == null)
        {
            instace = this;
        }else if (instace != this)
        {
            Destroy(gameObject);
        }

        playerInput = new PlayerInput();
    }
    #endregion


    public PlayerInput playerInput;
    public float twinningTime;
    public float jumpAmount = 1f;
    public float speed;
    public float rotatePerJump;
    public GameObject playerBody;
    
    
    public void JumpAnimation()
    {
        if (PlayerMovement.instance.isGrounded)
        {
            Debug.LogWarning("Jump Animation!!!!");
            PlayerMovement.instance.Jump();
        }
    }

    private void Start()
    {
        playerInput.Movement.Jump.performed += context => JumpAnimation();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}
