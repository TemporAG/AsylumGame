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
    public float sprintSpeed = 5.0f;
    public float walkSpeed = 3.0f;
    public float crouchSpeed = 2.0f;
    public float gravity = -9.81f;
    public float height = 2.0f;
    float crouchHeight = 1.0f;
    float enlargementHeight = 4.0f;
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
    //key
    char key;
    Vector3 velocity;
    bool isGrounded;
    bool canGrow;

    bool yellowKey;
    bool blueKey;
    bool greenKey;
    bool redKey;
    bool orangeKey;
    bool purpleKey;

    public GameObject MHAl1;
    public GameObject MHAl2;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        ttransform = GetComponent<Transform>();
        currentStamina = maxStamina;
        //staminaBar.SetStamina(maxStamina);
        currentSanity = maxSanity;
        canGrow = false;

        yellowKey = false;
        blueKey = false;
        greenKey = false;
        redKey = false;
        orangeKey = false;
        purpleKey = false;
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

    /*private void Enlargement()
    {
        Debug.Log("enlar");
        controller.height = height*2;
        /*temp = transform.localScale;
        temp.y += aValue * Time.deltaTime;
        temp.y = Mathf.Clamp(temp.y, 0f, enlargementHeight);
        transform.localScale = temp;
        Invoke("DeEnlargement", 8);   
    }


    private void DeEnlargement()
    {
        controller.height = 2;
        //Debug.Log("works");
        //temp.y -= aValue * Time.deltaTime;
        //temp.y = Mathf.Clamp(temp.y, 0f, enlargementHeight/2);
        canGrow = false;
    }
*/

    // Update is called once per frame
    void Update()
    {
        /*if (currentSanity < 10)
        {
            canGrow = true;
        }
        else { canGrow = false; }*/

        //if(canGrow) { Enlargement(); }
        //if(!canGrow) { DeEnlargement(); }

        if(currentSanity < 30)
        {
            MHAl1.SetActive(true);
            MHAl2.SetActive(true);
        }
        else
        {
            MHAl1.SetActive(false);
            MHAl2.SetActive(false);
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
            controller.height = height/2;
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
}