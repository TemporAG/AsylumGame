using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public CharacterController controller;
    public RigidbodyInterpolation interpolate;
    public Transform ttransform;
    //movement
    public float speed;
    public float sprintSpeed = 7.0f;
    public float walkSpeed = 4.5f;
    public float crouchSpeed = 2.0f;
    public float gravity = -9.81f;
    public float height = 2.0f;
    float crouchHeight = 1.0f;
    float enlargementHeight = 2.0f;
    Vector3 temp;
    //stamina
    public float maxStamina = 30f;
    public float currentStamina = 0f;
    //public UIScript staminaBar;
    float dValue = 10f;
    float sValue = 0.3f;
    float aValue = 0.09f;
    //sanity
    public float maxSanity = 40f;
    public float currentSanity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        ttransform = GetComponent<Transform>();
        currentStamina = maxStamina;
        //staminaBar.SetStamina(maxStamina);
        currentSanity = maxSanity;
    }


    private void DecreaseStamina()
    {
        if (currentStamina != 0)
        {
            currentStamina -= dValue * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }

    }
    private void IncreaseStamina()
    {
        if(Input.GetKey(KeyCode.LeftShift) == false)
        {
            currentStamina += dValue * Time.deltaTime;
        }
    }



    private void DeEnlargement()
    {
        Debug.Log("DEENLANRMENTEFASDFNFJAENF");
        temp = transform.localScale;
        temp.y = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSanity < 10)
        {
            StartCoroutine(Enlargement());
        }

        if (currentSanity > 0f)
        {
            currentSanity -= sValue * Time.deltaTime;
            currentSanity = Mathf.Clamp(currentSanity, 0f, maxSanity);
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        controller = GetComponent<CharacterController>();

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = crouchHeight;
            speed = crouchSpeed;
        }
        else
        {
            controller.height = 2.0f;
            speed = walkSpeed;
        }
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            DecreaseStamina();
            controller.height = 2.0f;
            speed = sprintSpeed;
            //staminaBar.SetStamina(currentStamina);
        }
        else if (currentStamina < maxStamina)
        {
            Invoke("IncreaseStamina", 3);
        }
    }

    private IEnumerator Enlargement()
    {
        Debug.Log("enlar");
        temp = transform.localScale;
        temp.y += aValue * Time.deltaTime;
        temp.y = Mathf.Clamp(temp.y, 0f, enlargementHeight);
        transform.localScale = temp;
        yield return new WaitForSeconds(3);
        DeEnlargement();
    }
}