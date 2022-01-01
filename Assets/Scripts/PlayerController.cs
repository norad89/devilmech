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
    public ParticleSystem footstepsParticle;
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
        // left, right and walk player inputs
        horizontalInput = Input.GetAxis("Horizontal");
        walkingInput = Input.GetKey("left shift");
    
        if  (horizontalInput > 0) {
            // player moves and faces right
            transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * (walkingInput ? speed/2 : speed)); 
            transform.rotation = Quaternion.Euler(0, 90, 0);
            CheckFootsteps();
            MovementAnimationSwitch();
        } else if (horizontalInput < 0) {
            // player moves and faces left        
            transform.Translate(Vector3.back * horizontalInput * Time.deltaTime * (walkingInput ? speed/2 : speed)); 
            transform.rotation = Quaternion.Euler(0, -90, 0);
            CheckFootsteps();
            MovementAnimationSwitch();
        } else {
            playerAnim.SetBool("Walk", false);
            playerAnim.SetBool("Move", false);
            footstepsParticle.Pause();
            footstepsParticle.Clear();
        }
        
        // player jumps
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            playerRb.AddForce(jump * jumpForce, ForceMode.Impulse);
            playerAnim.SetBool("Jump", true);
            playerAnim.SetBool("isGrounded", false);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        // checks if player is standing on ground
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
            playerAnim.SetBool("isGrounded", true);
        }
    }

    private void MovementAnimationSwitch() {
        // switches player animation between walking and running
        // TO DO: sound effects
        playerAnim.SetBool("Move", true);
        if (walkingInput) {
            playerAnim.SetBool("Walk", true);
        } else {
            playerAnim.SetBool("Walk", false); 
        }
    }
    
    private void CheckFootsteps() {
        // if player is grounded plays footsteps particle and sound effects
        footstepsParticle.Play();
        if (!isGrounded) {
            footstepsParticle.Pause();
            footstepsParticle.Clear();
        }
    }
}
