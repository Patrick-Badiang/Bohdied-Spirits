using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipment;
    public LayerMask whatIsItem;

    [Header("Inputs")]
    
    [SerializeField]
    private InputActionReference movementControl;
    [SerializeField]
    private InputActionReference jumpControl;

    [SerializeField]
    private float playerSpeed = 5.0f;

    [Header("Animation Duration")]
    public float attackAnimation;
    

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

    bool changed;
    
    Combat combat;
    Animator animator;

    private void OnEnable(){
        movementControl.action.Enable();
        jumpControl.action.Enable();   
    }

    private void OnDisable(){
        movementControl.action.Disable();
        jumpControl.action.Disable();
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        combat = GetComponent<Combat>();
        animator = GetComponent<Animator>();
        cameraMainTransform = Camera.main.transform;
    }

    
    public void PickUpItem(Collider other){
        var groundItem = other.GetComponent<GroundItem>();
        if(groundItem){
            Item _item = new Item(groundItem.item);
            
            if(inventory.AddItem(_item, 1)){
                Destroy(other.gameObject);
            }
        }
    }

    void CheckForItem()
    {
        Collider[] itemsToPickUp = Physics.OverlapSphere(transform.position, 1.5f, whatIsItem);

        foreach (Collider item in itemsToPickUp)
        {
            PickUpItem(item);
        }
        
         
    }
    void Update()
    {
        CheckForItem();
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
        if((movement.x != 0f) || (movement.y != 0f)){
            animator.SetBool("Move", true);
        }else{
            animator.SetBool("Move", false);
        }

        //State Conditions
        bool jumping = jumpControl.action.triggered;
        
        //State actions

        // Jumps
        if (jumping)
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

    public void StartAttack(){
        StartCoroutine(AttackAnim());
    }
    IEnumerator AttackAnim(){
          animator.SetBool("Attack", true);

          yield return new WaitForSeconds(attackAnimation);

          animator.SetBool("Attack", false);  
    }

    private void OnApplicationQuit(){
        inventory.Clear();
        equipment.Clear();
    }
}

