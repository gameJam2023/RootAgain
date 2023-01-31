using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

/*
! InputSystem注意
! perform 果下佢先會trigger, ctx 有3個state: start, perform, cancel. 而佢有得調教你點咁制 start係咁果下,perform係trigger, cancel係end個function
*/

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    private Vector2 moveVector;
    private PlayerInput playerInput;
    [SerializeField] InputActionReference jumpActionReference;
    [SerializeField] TMP_Text text_JumpAction;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.transform.GetComponent<Rigidbody>();
        playerInput = this.transform.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveVector != Vector2.zero)
        {
            this.transform.position += new Vector3(moveVector.x, 0, moveVector.y) * Time.deltaTime;
        }
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            rb.AddForce(Vector3.up * 10, ForceMode.Force);
            print("jump");
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        moveVector = ctx.ReadValue<Vector2>();
        Debug.Log(moveVector);

    }
    //! Rebinding 即係比player自己set key
    public void RebindingJumpInput(string rebindingMap) //! 比出面button 變字
    {
        print("rebinding");
        playerInput.SwitchCurrentActionMap(rebindingMap);
        text_JumpAction.text = "type...";
        jumpActionReference.action.PerformInteractiveRebinding()
        .OnMatchWaitForAnother(0.1f) //?等待時間
        .WithControlsExcluding("Mouse") //? 唔比人用某啲制
        .WithCancelingThrough("<Keyboard>/escape") //? 比佢 cancel 轉制
        .OnComplete(operation =>
        {
            //! if set mouse key ,要set active個game object , 可能係一個細bug
            var currentKey = operation.action.bindings[0].effectivePath;
            var readableKey = InputControlPath.ToHumanReadableString(currentKey, InputControlPath.HumanReadableStringOptions.OmitDevice); //? to human...轉番人類可讀string
            //? 其實operation 即係舊action map 之後就可以.入面啲action 同binding, 而bindings 入而應該係list,所以就要[],而effectivePath就係get key 啦 
            text_JumpAction.text = $"setJump:{(readableKey)}";
            print("done");
            operation.Dispose(); //?dispose 處理
            playerInput.SwitchCurrentActionMap("PlayerNormal");
        })
        .OnCancel(operation =>  //?for WithCancelingThrough , 比佢轉番之前果個制
        {
            //! if set mouse key ,要set active個game object , 可能係一個細bug
            var currentKey = operation.action.bindings[0].effectivePath;
            var readableKey = InputControlPath.ToHumanReadableString(currentKey, InputControlPath.HumanReadableStringOptions.OmitDevice); //? to human...轉番人類可讀string
            //? 其實operation 即係舊action map 之後就可以.入面啲action 同binding, 而bindings 入而應該係list,所以就要[],而effectivePath就係get key 啦 
            text_JumpAction.text = $"setJump:{(readableKey)}";
            print("done");
            operation.Dispose(); //?dispose 處理
            playerInput.SwitchCurrentActionMap("PlayerNormal");
        })
        .Start();
    }
}
