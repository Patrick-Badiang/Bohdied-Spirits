using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference movementControl;
    [SerializeField]
    private InputActionReference jumpControl;
    [SerializeField]
    private float playerSpeed = 5.0f;

    [Header("Jump Mechanic")]
    public float jumpHeight;
    public float doubleJumpHeight;

    public float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 4f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraMainTransform;
    private float jumpCount;

    private void OnEnable(){
        movementControl.action.Enable();
        jumpControl.action.Enable();
    }

    private void OnDisable(){
        movementControl.action.Disable();
        jumpControl.action.Disable();

    }
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;        
        controller = gameObject.GetComponent<CharacterController>();
        cameraMainTransform = Camera.main.transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            jumpCount = 0;
        }

        Vector2 movement = movementControl.action.ReadValue<Vector2>();       
        Vector3 move = new Vector3(movement.x, 0, movement.y);

        //Moves the character in terms of the camera rotation
        move = cameraMainTransform.forward * move.z + cameraMainTransform.right * move.x;
        move.y = 0f;
        
        controller.Move(move * Time.deltaTime * playerSpeed);

        

        // Jumps
        if (jumpControl.action.triggered)
        {
            if(jumpCount == 0){
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                jumpCount = 1;
            }else if(jumpCount == 1){
                playerVelocity.y += Mathf.Sqrt(doubleJumpHeight * -3.0f * gravityValue);
                jumpCount = 2;
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //Gets the rotatin and moves according to the camera
        if(movement != Vector2.zero){
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f); //We only want to rotate on the y axis
            transform.rotation = Quaternion.Lerp(transform.rotation,rotation,Time.deltaTime * rotationSpeed);
        }
    }
}