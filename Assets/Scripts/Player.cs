using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{

    public Camera playerCamera;
    public float walkSpeed = 1f;
    public float runSpeed = 3f;
    public float jumpPower = 4f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;

    public static FPSController Instance;

    public int MaxHealth = 30;

    public int CurrentHealth;

    public int MaxFood = 30;

    public int CurrentFood;

    public TMP_Text HealthText;

    public TMP_Text FoodText;

    public Slider Healthslider;

    public Slider Foodslider;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        CurrentHealth = MaxHealth;
        Healthslider.value = CurrentHealth;
        HealthText.text = $"HP:{CurrentHealth}";
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
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
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        //Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if(canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(3);
        }
    }

    void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Healthslider.value = CurrentHealth;
        HealthText.text = $"HP:{CurrentHealth}";
    }

    public void SetHealth()
    {
        Healthslider.value = CurrentHealth;
        HealthText.text = $"HP:{CurrentHealth}";
    }

    public void SetMaxHealth()
    {
        Healthslider.maxValue = MaxHealth;
        Healthslider.value = CurrentHealth;
        HealthText.text = $"HP:{CurrentHealth}";
    }

    public void IncreaseHealth(int value)
    {
        CurrentHealth += value;
        Healthslider.value = CurrentHealth;
        HealthText.text = $"HP:{CurrentHealth}";
    }

    public void SetFood()
    {
        Foodslider.value = CurrentFood;
        HealthText.text = $"HP:{CurrentHealth}";
    }

    public void SetMaxFood()
    {
        Healthslider.maxValue = MaxHealth;
        Healthslider.value = CurrentHealth;
        HealthText.text = $"HP:{CurrentHealth}";
    }

    public void IncreaseFood(int value)
    {
        CurrentFood += value;
        FoodText.text = $"Food:{CurrentFood}";
    }
}

