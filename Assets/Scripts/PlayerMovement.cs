using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement player;

    public Transform pCamera;
    public Transform cameraPOS;
    private Rigidbody rb;

    private float horiINPUT;
    private float vertINPUT;
    private Vector3 moveDirection;


    private float speedRotate;
    public float speedMove;

    private float xRotate;
    private float yRotate;

    public float groundDrag;
    private float playerHeight;
    public LayerMask theGround;
    bool grounded;

    public float flightStamina;
    public float flightForce;
    public float airDrag;
    public float flightCost;
    public Transform broomBar;
    bool isFlying;

    public GameObject interactWithTree;
    public LayerMask magmenos;
    bool lookAtMagmenos;

    bool bookIsOpen;
    public GameObject spellBook;

    // Start is called before the first frame update
    void Start()
    {
        interactWithTree.SetActive(false);
        playerHeight = 2;
        speedRotate = 2;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        spellBook.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        pCamera.transform.position = cameraPOS.position;
        xRotate += speedRotate * Input.GetAxis("Mouse X");
        yRotate -= speedRotate * Input.GetAxis("Mouse Y");
        flightStamina = broomBar.GetComponent<Slider>().value;

        pCamera.eulerAngles = new Vector3(Mathf.Clamp(yRotate, -90f, 90f), xRotate, 0.0f);
        transform.eulerAngles = new Vector3(0, xRotate, 0.0f);

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, theGround);

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        //Debug.DrawRay(transform.position, forward, Color.green);
        lookAtMagmenos = Physics.Raycast(transform.position,forward, 10f, magmenos);
        if (lookAtMagmenos)
            interactWithTree.SetActive(true);
        else
            interactWithTree.SetActive(false);

        myInput();
        if (grounded)
        {
            rb.drag = groundDrag;
            speedMove = 5;
        }
        else if (isFlying)
        {
            rb.drag = airDrag;
            speedMove = 2;
        }
        else
        {
            rb.drag = 0.0f;
            speedMove = 0.5f;
        }
            
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    private void myInput()
    {
        horiINPUT = Input.GetAxisRaw("Horizontal");
        vertINPUT = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.Space) && flightStamina !=0)
        {
            isFlying = true;
            fly();
        }
        else
            isFlying= false;

        if (Input.GetKey(KeyCode.Q) && lookAtMagmenos)
        {
            broomBar.GetComponent<Slider>().value = 10f;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!bookIsOpen)
            {
                spellBook.SetActive(true);
                bookIsOpen = true;
            }
            else
            {
                spellBook.SetActive(false);
                bookIsOpen = false;
            }
        } 
            


    }

    private void movePlayer()
    {
        moveDirection = transform.forward * vertINPUT + transform.right * horiINPUT;
        rb.AddForce(moveDirection.normalized * speedMove * 10.0f, ForceMode.Impulse);

    }

    private void fly()
    {
        rb.AddForce(transform.up * flightForce, ForceMode.Impulse);
        flightStamina -= flightCost *Time.deltaTime;
        broomBar.GetComponent<Slider>().value = flightStamina;

        if (flightStamina < 0)
            flightStamina = 0;
    }
}
