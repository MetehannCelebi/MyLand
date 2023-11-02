using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private JoystickController _joystickController;
    private CharacterController _characterController;
    [SerializeField] private PlayerAnimator PlayerAnimator;
    private Vector3 moveVector;
    [Header("Settings")]
    [SerializeField] private int moveSpeed;

    private float gravity = -9.81f;

    private float gravityMultipler = 3f;

    private float gravityVelocity;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        moveVector = _joystickController.GetMovePosision() * (moveSpeed * Time.deltaTime) / Screen.width;
        moveVector.z = moveVector.y;
        moveVector.y = 0;
        PlayerAnimator.ManageAnimations(moveVector);
        ApplyGravity();
        _characterController.Move(moveVector);
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded && gravityVelocity < 0.0f)
        {
            gravityVelocity = -1f;
        }
        else
        {
            gravityVelocity += gravity * gravityMultipler * Time.deltaTime;
        }

        moveVector.y = gravityVelocity;
    }
}
