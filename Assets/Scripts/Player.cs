using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{

    public Camera playerCamera;
    public float walkSpeed;
    public float runSpeed;
    public float jumpPower;
    public float gravity;

    public int MaxStamina = 30;
    public int CurrentStamina;
    public Slider Staminaslider;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;

    public static FPSController Instance;

    public bool isRunning;

    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        CurrentStamina = MaxStamina;
        Staminaslider.value = CurrentStamina;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //Rotation
        characterController.Move(moveDirection * Time.deltaTime);
        
        if (canMove)
        {
            // Create a new Vector3
            /*Vector3 movementVector = Vector3.zero;

            // Add the forward direction of the player multiplied by the user's up/down input.
            movementVector += transform.forward * moveDirection.y;

            // Add the right direction of the player multiplied by the user's right/left input.
            movementVector += transform.right * moveDirection.x;

            // increases movement speed when sprint is held down
            if (isRunning && Staminaslider.value > 0)
            {
                Debug.Log("Player is running");
                GetComponent<Rigidbody>().MovePosition(transform.position + ((transform.right * moveDirection.x) + (transform.forward * moveDirection.y)) * runSpeed);
                Staminaslider.value -= 0.25f * Time.deltaTime;
            }

            // reduces movement speed to normal walking speed when stamina runs out
            else if (isRunning && Staminaslider.value <= 0)
            {
                Debug.Log("No stamina left");
                GetComponent<Rigidbody>().MovePosition(transform.position + ((transform.right * moveDirection.x) + (transform.forward * moveDirection.y)) * walkSpeed);
            }

            // reduces movement speed to normal walking speed when shift key is not held down
            else if (!isRunning && Staminaslider.value >= 0)
            {
                Debug.Log("Player is walking");
                GetComponent<Rigidbody>().MovePosition(transform.position + ((transform.right * moveDirection.x) + (transform.forward * moveDirection.y)) * walkSpeed);
                Staminaslider.value += 0.35f * Time.deltaTime;
            }*/

            if (isRunning && Staminaslider.value > 0)
            {
                //runSpeed = runSpeed;
                Staminaslider.value -= 1f * Time.deltaTime;
                Debug.Log("Player is running");
                Debug.Log(Staminaslider.value);

            }

            // reduces movement speed to normal walking speed when stamina runs out
            else if (isRunning && Staminaslider.value <= 0)
            {
                Debug.Log("No stamina left");
                isRunning = false;
                //runSpeed = walkSpeed;
            }

            // reduces movement speed to normal walking speed when shift key is not held down
            else if (!isRunning && Staminaslider.value >= 0)
            {
                Debug.Log("Player is walking");
                //runSpeed = runSpeed;
                Staminaslider.value += 1f * Time.deltaTime;
                Debug.Log(Staminaslider.value);
            }

            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        //Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Workbench")
        {
            GameManager.Instance.OpenCraftMenu();
        }
        else if(other.gameObject.tag == "Bed")
        {
            Debug.Log("Near Bed");
            GameManager.Instance.GoToBed();
        }
        else if (other.gameObject.tag == "Bonfire")
        {
            Debug.Log("Near bonfire");
            GameManager.Instance.LightTorch();
        }
        else if (other.gameObject.tag == "Axe")
        {
            Debug.Log("Near axe");
            GameManager.Instance.AxeGameObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Workbench")
        {
            GameManager.Instance.CloseCraftMenu();
        }
        else if(other.gameObject.tag == "Bed")
        {
            Debug.Log("Not Near Bed");
        }
        else if (other.gameObject.tag == "Bonfire")
        {
            Debug.Log("Not near bonfire");
        }
        else if (other.gameObject.tag == "Axe")
        {
            Debug.Log("Not near axe");
            GameManager.Instance.AxeGameObject = null;
        }
    }
}

