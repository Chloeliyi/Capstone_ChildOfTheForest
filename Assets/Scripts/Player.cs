using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{

    public Camera playerCamera;
    public GameObject playerSpawn;
    public GameObject pause;
    public bool paused = false;
    public float walkSpeed;
    public float runSpeed;
    public float jumpPower;
    public float gravity = 20f;
    public float ySpeed;

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

    Terrain terrain;

    //Animator animator;

    private void Awake()
    {
        Instance = this;
        playerSpawn = GameObject.Find("playerSpawn");
    }
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        terrain = Terrain.activeTerrain;
        //animator = GetComponent<Animator>();
        CurrentStamina = MaxStamina;
        Staminaslider.value = CurrentStamina;
        if (playerSpawn != null)
        {
            transform.position = playerSpawn.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        isRunning = Input.GetKey(KeyCode.LeftShift);
        if (!paused)
        {
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            //Jumping
            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }
            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            if (characterController.transform.position.y - 0.69f >= terrain.SampleHeight(transform.position))
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);

            if (canMove)
            {

                if (isRunning && Staminaslider.value > 0)
                {
                    runSpeed = runSpeed;
                    Staminaslider.value -= 3f * Time.deltaTime;
                    Debug.Log("Player is running");
                    //Debug.Log(Staminaslider.value);

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
                Staminaslider.value += 0.1f * Time.deltaTime;
                //Debug.Log(Staminaslider.value);
            }

                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                pause.SetActive(true);
                Time.timeScale = 0;
                paused = true;
            }
            else
            {
                pause.SetActive(false);
                Time.timeScale = 1;
                paused = false;
            }
        }
    }

    /*public void PlayerDeath()
    {
        animator.SetTrigger("isDead");
    }*/

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
        else if (other.gameObject.tag == "Spear")
        {
            Debug.Log("Near spear");
            GameManager.Instance.projectile = other.gameObject;
        }
        else if (other.gameObject.tag == "Water")
        {
            Debug.Log("Near water");
            GameManager.Instance.Nearwater = true;
        }

        else if (other.gameObject.tag == "Altar")
        {
            Debug.Log("Near altar");
            GameManager.Instance.Nearaltar = true;
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
        else if (other.gameObject.tag == "Spear")
        {
            Debug.Log("Near spear");
            GameManager.Instance.projectile = null;
        }
        else if (other.gameObject.tag == "Water")
        {
            Debug.Log("Not near water");
            GameManager.Instance.Nearwater = false;
        }

        else if (other.gameObject.tag == "Altar")
        {
            Debug.Log("Not near altar");
            GameManager.Instance.Nearaltar = false;
        }
    }
}

