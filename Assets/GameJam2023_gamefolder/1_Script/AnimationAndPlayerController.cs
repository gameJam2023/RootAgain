using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

public class AnimationAndPlayerController : MonoBehaviour
{
    //! declare reference variables
    PlayerControllerInputAction playerController;
    CharacterController characterController;
    Animator animator;

    //!variables to store optimized setter/getter parameter IDs
    int isWalkingHash;
    int isRunningHash;

    //!variables to store player input values
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    [SerializeField] float speed = 3f;
    [SerializeField] bool isMovementPressed;
    [SerializeField] bool isRunningPressed;
    [SerializeField] bool isJumpPressed;
    [SerializeField] float rotationFactorPerFrame; // faster if it closes to 1
    //!variable to confirm the state of character

    [SerializeField] bool isWalking;
    [SerializeField] bool isRunning;


    // Rigidbody rb;

    void Awake()
    {
        //! initially set reference variables
        playerController = new PlayerControllerInputAction();
        characterController = GetComponent<CharacterController>();
        // rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        //? For animation optimize 優化
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        //!InputSystem注意
        //!  context 有3個state: start, perform, cancel.而佢有得調教你點咁制 start係咁果下, 
        //! perform係running(通常都係蘑茹制), cancel係end個function(release button)

        //?Move set up
        playerController.PlayerNormal.Move.started += onMovementInput;
        playerController.PlayerNormal.Move.canceled += onMovementInput;
        playerController.PlayerNormal.Move.performed += onMovementInput;

        //?Run set up
        playerController.PlayerNormal.Run.started += onRun;
        playerController.PlayerNormal.Run.canceled += onRun;
        //playerController.PlayerNormal.Run.performed += onRun; //! button 

        //?Jump set up
        playerController.PlayerNormal.Jump.started += onJump;
        playerController.PlayerNormal.Jump.canceled += onJump;


    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity;
            currentRunMovement.y += gravity;
        }
    }


    // Update is called once per frame
    void Update()
    {
        handleRotation();
        handleAnimation();

        if (isRunningPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }

        // if (isRunningPressed)
        // {
        //     rb.velocity = currentRunMovement * Time.deltaTime;
        // }
        // else
        // {
        //     rb.velocity = currentMovement * Time.deltaTime;
        // }

    }

    void handleRotation()
    {
        Vector3 positionTolookAt;
        //? the change in position our character should point to 
        positionTolookAt.x = currentMovement.x;
        positionTolookAt.y = 0f;
        positionTolookAt.z = currentMovement.z;
        //? the current rotation of our character
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            //? create a new rotation based on where the player is currently pressing
            Quaternion targetRotation = Quaternion.LookRotation(positionTolookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame);
        }

    }

    //! Walking data for game Start
    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * speed;
        currentRunMovement.z = currentMovementInput.y * speed;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    //!Running data for game Start
    void onRun(InputAction.CallbackContext context)
    {
        isRunningPressed = context.ReadValueAsButton();
    }

    //!Jump data for game Start
    void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }


    void handleAnimation()
    {
        //? get parameter values from animator
        isWalking = animator.GetBool(isWalkingHash);
        isRunning = animator.GetBool(isRunningHash);

        //? start walking if movement pressed is true and not already walking
        if (isMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        //? stop walking if isMovementPressed is false and not already walking
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }

        if ((isMovementPressed && isRunningPressed) && !isRunning)
        {
            animator.SetBool("isRunning", true);
        }
        else if ((!isMovementPressed || !isRunningPressed) && isRunning)
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void OnEnable()
    {
        playerController.PlayerNormal.Enable();
    }

    private void OnDisable()
    {
        playerController.PlayerNormal.Disable();
    }
}
