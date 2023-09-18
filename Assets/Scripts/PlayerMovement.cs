using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement player;

    public Transform pCamera;
    public Transform cameraPOS;

    public float horiINPUT;
    public float vertINPUT;
    public Vector3 moveDirection;

    public Rigidbody rb;
    public float dragF;

    public float speedRotate;
    public float speedMove;

    public float xRotate;
    public float yRotate;

    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        pCamera.transform.position = cameraPOS.position;
        xRotate += speedRotate * Input.GetAxis("Mouse X");
        yRotate -= speedRotate * Input.GetAxis("Mouse Y");

        pCamera.eulerAngles = new Vector3(Mathf.Clamp(yRotate, -90f, 90f), xRotate, 0.0f);
        transform.eulerAngles = new Vector3(0, xRotate, 0.0f);

        myInput();
        rb.drag = dragF;
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    private void myInput()
    {
        horiINPUT = Input.GetAxisRaw("Horizontal");
        vertINPUT = Input.GetAxisRaw("Vertical");

    }

    private void movePlayer()
    {
        moveDirection = transform.forward * vertINPUT + transform.right * horiINPUT;
        rb.AddForce(moveDirection.normalized * speedMove, ForceMode.Force);

    }
}
