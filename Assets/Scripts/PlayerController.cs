using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //needed to restart the game when the player enters the death zone (trigger event)
using TMPro;

public class PlayerController : MonoBehaviour
{

    //These public variables are initialized in the Inspector
    public float speed;
    public TMP_Text countText;
    public TMP_Text winText;
    public TMP_Text timeText;  //  variable to display the timer text in Unity
    public float startingTime;  // variable to hold the game's starting time
    public string min;
    public string sec;
    public float jumpForce;
    public Rigidbody rb;

    [SerializeField] private bool _isJumping;
    [SerializeField] private float _startJumpTime;
    [SerializeField] private float _airJumpTime;
    [SerializeField] private float _maxJumpTime;
    [SerializeField] private float _jumpAcceleration;



    //These private variables are initialized in the Start
    private int count;
    private bool gameOver; //  bool to define game state on or off.
    private ConstantForce cForce;
    private Vector3 cForceDir;
    private bool speedy;
    private Vector3 cForceAmount;
    private float drag; 

    // Audio
    public AudioClip coinSFX;
    private AudioSource audioSource;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        startingTime = Time.time;
        gameOver = false;
        cForceAmount = new Vector3(0, -10, 0);
        drag = 2f; 

        audioSource = GetComponent<AudioSource>();  // access the audio source component of player
        // New stuff below: 
        cForce = GetComponent <ConstantForce>();
        rb.maxAngularVelocity = Mathf.Infinity;



    }
    private void Update()
    {
        if (gameOver) // condition that the game is NOT over; returns the false value
            return;
        float timer = Time.time - startingTime;     // local variable to updated time
        min = ((int)timer / 60).ToString();     // calculates minutes
        sec = (timer % 60).ToString("f0");      // calculates seconds
       

        timeText.text = "Elapsed Time: " + min + ":" + sec;     // update UI time text
        // New stuff below here: 
        if (Input.GetKeyDown(KeyCode.Space))        // Jump code
        {
            if (IsGrounded())
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                _isJumping = true;
                _startJumpTime = Time.time;
                _maxJumpTime = .25f;            //_startJumpTime + _airJumpTime;
            } 
        }
        





        if (!IsGrounded())                          // Adds extra gravity when in the air
        {
            cForceDir = cForceAmount;
            cForce.force = cForceDir;
            rb.drag = 1f;
            if (speedy)
            {
                speed = 30f;
            }
        }
        else
        {
            cForceDir = new Vector3(0, 0, 0);
            cForce.force = cForceDir;
            rb.drag = drag;
            if (speedy)
            {
                speed = 14*7.5f;
            }
            _isJumping = false;
        }

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        //float torqueStrength = 100f;

        //// Code for getting cam transform
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        Vector3 movement = (camRight * moveHorizontal + camForward * moveVertical).normalized; // Changes movement input based on camera.. 

        movement = new Vector3(movement.x, 0f, movement.z);
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);


        //rb.AddTorque(Vector3.up * moveHorizontal * torqueStrength);

        if (Input.GetKey(KeyCode.Space) && _isJumping && (_startJumpTime + _maxJumpTime > Time.time))
        {
            rb.AddForce(Vector3.up * _jumpAcceleration, ForceMode.Acceleration);
            Debug.Log("WorkWork");
        }


    }

    private bool IsGrounded()
    {
         return GetComponent<Rigidbody>().velocity.y == 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        //This event/function handles trigger events (collsion between a game object with a rigid body)
   
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();

            //PLAY SOUND EFFECT
            audioSource.clip = coinSFX;
            audioSource.Play();

        }

        if (other.gameObject.CompareTag("DeathZone"))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }

        if (other.gameObject.CompareTag("Grow"))
        {
            if (transform.localScale.x <= 2.0f)
            {
                transform.localScale *= 1.25f;    // increase scale by 25%
            }
        }

        if (other.gameObject.CompareTag("JumpPowah"))
        {
            if (transform.localScale.x <= 2.0f)
            {
                transform.localScale *= 1.25f;    // increase scale by 25%
            }
        }

        if (other.gameObject.CompareTag("SpeedPowah"))
        {
            if (!speedy)
            {
                speed = 105f;
                drag = 4f;
                rb.drag = 4f;
                //rb.mass = .5f;
                //jumpForce = jumpForce * 2.8f;
                //cForceAmount = new Vector3(0,-50,0);
            }
            speedy = true;
        }

        if (other.gameObject.CompareTag("InvinciblePowah"))
        {
            if (transform.localScale.x <= 2.0f)
            {
                transform.localScale *= 1.25f;    // increase scale by 25%
            }
        }

        if (other.gameObject.CompareTag("Shrink"))
        {
            if (transform.localScale.x >= 0.5f)
            {
                transform.localScale *= 0.75f;     // decreases scale by 25%
            }
        }

        if (other.gameObject.CompareTag("Jump"))
        {
            rb.AddForce(new Vector3(0.0f, 300.0f, 0.0f));
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 2)
        {
            gameOver = true; // returns true value to signal game is over
            timeText.color = Color.green;  // changes timer's color
            winText.text = "You win!";
            speed = 0;
        }
    }
}
