using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 250.0f;
    private float horizontalInput;
    private bool walkingInput;
    public Animator playerAnim;
    private Vector3 jump;
    public bool isGrounded;

    Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }  

    // Update is called once per frame
    void Update()
    {
        // left and right player movements
        horizontalInput = Input.GetAxis("Horizontal");
        walkingInput = Input.GetKey("left shift");
    
        if  (horizontalInput > 0) {
            // player moves and faces right
            transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * (walkingInput ? speed/2 : speed)); 
            transform.rotation = Quaternion.Euler(0, 90, 0);
            StartMovementAnim();
        } else if (horizontalInput < 0) {
            // player moves and faces left        
            transform.Translate(Vector3.back * horizontalInput * Time.deltaTime * (walkingInput ? speed/2 : speed)); 
            transform.rotation = Quaternion.Euler(0, -90, 0);
            StartMovementAnim();
        } else {
            playerAnim.SetBool("Walk", false);
            playerAnim.SetBool("Move", false);
        }
        
        // player jumps
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            playerRb.AddForce(jump * jumpForce, ForceMode.Impulse);
            playerAnim.SetBool("Jump", true);
            playerAnim.SetBool("isGrounded", false);
            isGrounded = false;
        }
    }

    private void StartMovementAnim() {
        playerAnim.SetBool("Move", true);
        if (walkingInput) {
            playerAnim.SetBool("Walk", true);
        } else {
            playerAnim.SetBool("Walk", false); 
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        // checks if player is standing on ground
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
            playerAnim.SetBool("isGrounded", true);
        }
    }
}
