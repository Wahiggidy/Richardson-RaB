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
    public float drag;             // This variable is used to reset the drag upon landing, allowing for less ridiculous air speeds as opposed to ground speed 
    public float airDrag;
    public UIController ui;
    public GameObject cam; 

    [SerializeField] private bool _isJumping;
    [SerializeField] private bool airJumping;
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
    private bool jumpy;
    private Vector3 cForceAmount;
    private Collider coll;
    private float airRes;           // Air resistance for the jump power
    private float horiMultiplier;
    private float vertiMultiplier;
    private float dampening;
    private float ffForce;
    private float maxBounceSpeed;
    private float dashForce;
    private Vector3 movement;
    private Vector3 moddedDash;
    public float maxSpeed;              // Public variable for speed clamp
    private bool invincible;
    private Color color;
    private Renderer ren; 

    private bool hasDashed;
    private bool hasDashedF;
    private bool finalDash;

    // Audio
    public AudioClip coinSFX;
    private AudioSource audioSource;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ren = GetComponent<Renderer>();
        count = 0;
        SetCountText();
        winText.text = "";
        startingTime = Time.time;
        gameOver = false;
        cForceAmount = new Vector3(0, -10, 0);
        airRes = 1f;
        horiMultiplier = 1f;
        vertiMultiplier = 1f; 
        dampening = .95f;
        ffForce = 30f;
        maxBounceSpeed = 20f;
        dashForce = 20f;
       // maxSpeed = 40f;
        //drag = 2f; 

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
       

        timeText.text = min + ":" + sec;     // update UI time text
        // New stuff below here: 
        if (Input.GetKeyDown(KeyCode.Space))        // Jump code
        {
            if (IsGroundedForJump())                               // Normal jump 
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                _isJumping = true;
                _startJumpTime = Time.time;
                _maxJumpTime = .25f;            //_startJumpTime + _airJumpTime;
            } 
            if (jumpy && !IsGrounded() && !airJumping)                  // Jump for when you have the jump power 
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce * 1.5f, rb.velocity.z);
                airJumping = true;
                _startJumpTime = Time.time;
                _maxJumpTime = .25f;            //_startJumpTime + _airJumpTime;
                DampenVelocity(.75f);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && jumpy && !IsGrounded())         // Fast fall input
        {
            
            rb.AddForce(Vector3.down * ffForce, ForceMode.VelocityChange);
            DampenVelocity(.5f);
            if (hasDashedF && !finalDash)
            {
                hasDashed = false;
                finalDash = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && jumpy && !hasDashed)               // Dash input
        {
            if (movement == Vector3.zero)                       // Dashes based on input, but if no input, dashes forward
            {
                Vector3 camForward = Camera.main.transform.forward;
                moddedDash = camForward;
                moddedDash = new Vector3(moddedDash.x, 0f, moddedDash.z);
            }
            else
            {
                moddedDash = movement;
            }
            if (finalDash)
            {
                moddedDash = new Vector3(moddedDash.x, -5f, moddedDash.z) * 1.5f;
            }
            rb.AddForce(moddedDash * dashForce, ForceMode.VelocityChange);
            hasDashed = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && jumpy && hasDashed && airJumping && !hasDashedF)               // Dash input
        {
            if (movement == Vector3.zero)                       //"" 
            {
                Vector3 camForward = Camera.main.transform.forward;
                moddedDash = camForward *5  ;
                moddedDash = new Vector3(moddedDash.x, 3.5f, moddedDash.z);
            }
            else
            {
                moddedDash = movement + new Vector3(0, 3.5f, 0);
            }            
            rb.AddForce(moddedDash * dashForce/2, ForceMode.VelocityChange);
            hasDashedF = true;
        }





        if (!IsGrounded())                          // Adds extra gravity when in the air
        {
            cForceDir = cForceAmount;
            cForce.force = cForceDir;
            rb.drag = airDrag;
            if (speedy)
            {
                speed = 30f;
            }
        }
        else
        {
            Invoke("DelayForceFix", .02f);          // Fixes a glitch where setting the drag immediately changes the landing direction of the sphere 
            cForceDir = new Vector3(0, 0, 0);
            cForce.force = cForceDir;
            if (speedy)
            {
                speed = 14 * 7.5f;
            }
            _isJumping = false;
            hasDashed = false;
            airJumping = false;
            hasDashedF = false;
            finalDash = false; 
        }

    }

    private void DampenVelocity(float dampen)
    {
        Vector3 currentVelocity = rb.velocity;       // Dampening effect when player is still or other calls

        // Damping for x and z components
        currentVelocity.x *= dampen;
        currentVelocity.z *= dampen;

        if (currentVelocity.x < .1f && currentVelocity.z < .1f)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

        rb.velocity = currentVelocity;
        
    }

    private void DelayForceFix()
    {
        
        rb.drag = drag;
        
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * horiMultiplier;
        float moveVertical = Input.GetAxis("Vertical") * vertiMultiplier;
        //float torqueStrength = 100f;

        //// Code for getting cam transform
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        movement = (camRight * moveHorizontal + camForward * moveVertical).normalized; // Changes movement input based on camera.. 

        movement = new Vector3(movement.x, 0f, movement.z);
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed * airRes);


        if (movement == Vector3.zero)
        {
            /*//rb.velocity *= dampening;                      
            Vector3 currentVelocity = rb.velocity;

            // Damping for x and z components
            currentVelocity.x *= dampening;
            currentVelocity.z *= dampening;

           
            rb.velocity = currentVelocity;*/

            DampenVelocity(dampening);
        }


        //rb.AddTorque(Vector3.up * moveHorizontal * torqueStrength);

        if (Input.GetKey(KeyCode.Space) && (_isJumping || airJumping) && (_startJumpTime + _maxJumpTime > Time.time))
        {
            rb.AddForce(Vector3.up * _jumpAcceleration, ForceMode.Acceleration);
        }


        if (rb.velocity.y > maxBounceSpeed && jumpy)                  // clamps max bounce speed 
        {
            Vector3 clampVelocity = rb.velocity;
            clampVelocity.y = maxBounceSpeed;
            rb.velocity = clampVelocity;
        }

        if (rb.velocity.x > maxSpeed || rb.velocity.z > maxSpeed)
        {
            Vector3 clampVelocity = rb.velocity;
            if (rb.velocity.x > maxSpeed)
            {
                clampVelocity.x = maxSpeed;
            }
            if (rb.velocity.z > maxSpeed)
            {
                clampVelocity.z = maxSpeed;
            }
            //rb.velocity = clampVelocity; 
        }

    }

    private bool IsGrounded()
    {

        if (!jumpy)
        {
            return GetComponent<Rigidbody>().velocity.y == 0;
        }
        else
        {
            float raycastDistance = 0.6f;
            return Physics.Raycast(transform.position, Vector3.down, raycastDistance);
        }
    }

    private bool IsGroundedForJump()
    {
        
            float raycastDistance = 0.6f;
            return Physics.Raycast(transform.position, Vector3.down, raycastDistance);
        
    }


    private void OnTriggerEnter(Collider other)
    {
        //This event/function handles trigger events (collsion between a game object with a rigid body)
   
        if (other.gameObject.tag == "PickUp")
        {
            //DestroyImmediate(other.transform.parent.gameObject);
            //other.gameObject.transform.parent.gameObject.SetActive(false);
            //other.gameObject.SetActive(false);
            //count++;
            //SetCountText();

            //PLAY SOUND EFFECT
            //audioSource.clip = coinSFX;
            //audioSource.Play();

        }

        if (other.gameObject.tag == "Security")
        {
            Destroy(other.gameObject);
            //other.gameObject.SetActive(false);
            //other.gameObject.SetActive(false);
            count++;
            SetCountText();

            //PLAY SOUND EFFECT
            audioSource.clip = coinSFX;
            audioSource.Play();

            //Play particle system



        }

        if (other.gameObject.CompareTag("DeathZone"))                   // Instant unconditional kill
        {
            
            
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(2);
        }

        if (other.gameObject.CompareTag("DamageZone") && !invincible)   // Damages player
        {
            ui.health--;
            invincible = true;
            Invoke("InvincibleReset", 2f);
            color = ren.material.color;
            color.a = .25f;
            ren.material.color = color;
            cam.GetComponent<CameraController>().InitiateCoroutine();
            Debug.Log(ui.health);

            if (ui.health <= 0)
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(2);
            }
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
            if (!jumpy)
            {
                coll = GetComponent<SphereCollider>();
                coll.material.bounciness = .965f;
                coll.material.staticFriction = 0f;
                coll.material.dynamicFriction = 0f;
                airDrag = 0f;
                drag = 0f;
                speed = 10f;                    // !! Need to change this to complement the speed powerup, or make a function happen that combines them... prolly the latter
                airRes = .5f;
                horiMultiplier = 15f;
                vertiMultiplier = 15f;
                dampening = .85f;
                cForceAmount = new Vector3(0,-25,0);

            }
            jumpy = true;
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

        if (other.gameObject.CompareTag("InvinciblePowah") && !invincible)
        {
            
            invincible = true;
            Invoke("InvincibleReset", 8f);

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

        if (other.gameObject.CompareTag("LightUp"))
        {
            Renderer otherRenderer = other.gameObject.GetComponent<Renderer>();
            if (otherRenderer != null)
            {
                other.GetComponent<LightUpPlats>().InitiateCoroutine();
            }


        }

    }

    private void InvincibleReset()
    {
        color.a = 1f;
        ren.material.color = color;

        invincible = false; 
    }

    void SetCountText()
    {
        countText.text = count.ToString();
        if(count >= 10)
        {
            gameOver = true; // returns true value to signal game is over
            timeText.color = Color.green;  // changes timer's color
            winText.text = "You win!";
            speed = 0;
        }
    }
}
