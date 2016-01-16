//------------------------------------------------------------------------------------------------
// Author: Matthew Dean
// Date: 1/10/2016
// Credit: Unity Scripting API
// Credit: Full Sail University
// Purpose: Script used to define and create player movement, weapons firing,
// thrust movement, and camera shake.
//------------------------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class ShipPlayerController : MonoBehaviour
{
    #region Variables

    // The maximum lives the player starts with.
    [SerializeField]
    int MaxLives = 3;

    // The current lives remaining for the player.
    private int CurLives = 3;

    // The number of proectiles shot
    private int numShotsFired = 0;

    // The number of enemies hit
    private int numEnemyHits = 0;

    // The current game score (points earned)
    private int Score;

    // Determines the screen bounds in relation to the
    // player object. This camera object needs to be populated
    // in the editor (drag and drop the "Main Camera" on to
    // this attribute in the Inspector)
    public Camera CameraObject;

    // Control how quickly we can fire (delay between shots)
    [Tooltip("The speed at which the player fires projectiles")]
    public float FireRate;

    // The amount of time since we last fired out weapon
    private float FireTimer = 0.0f;

    // To throttle the "T" if from resetting "OriginalMoveSpeed",
    // because "OriginalMoveSpeed" needs to get set once at the 
    // beginning while holding "T", then get set back to "MoveSpeed".
    private bool firstTimeBoost = true;
    private float OriginalMoveSpeedX = 5;
    private float OriginalMoveSpeedY = 5;

    // Used to control how fast the ship moves X axis
    [SerializeField]
    [Tooltip("The speed at which the player moves on X axis")]
    [Range(0, 10)]
    public float MoveSpeedX;

    // Used to control how fast the ship moves Y axis
    [SerializeField]
    [Tooltip("The speed at which the player moves on Y axis")]
    [Range(0, 10)]
    public float MoveSpeedY;

    // Used to control how fast the ship rotates
    [SerializeField]
    [Tooltip("The speed at which the player rotates")]
    [Range(0, 500)]
    public float rotateSpeed;

    // Used to control how fast the ship rolls
    //[SerializeField]
    //[Tooltip("The speed at which the player rolls")]
    //[Range(0, 200)]
    //private float rollSpeed = 15.0f;

    // Helper vector for updating the ship's position
    // declared here since it is used every update/tick
    // and we do not want to waste CPU power recreating 
    // it constantly.
    private Vector3 movement = Vector3.zero;

    // Thrust Speed Variable
    [SerializeField]
    [Tooltip("The speed of turbo movement")]
    [Range(0, 8)]
    public float turboSpeed;

    // Thrust Boolean Variable
    private bool boostEnabled = false;

    // Thrust Cooldown Boolean Variable
    private bool ThrusterCooldown = false;

    // Projectile Game Object Variable
    [Tooltip("The Game Object used as the projectile")]
    public Projectile Bullet;

    // Projectile Game Object Variable
    [Tooltip("The Game Object used as the projectile")]
    public Projectile Bullet1;

    // Used to keep projectile time.
    private float _previousTime;

    // USed to defined original position for camera shake.
    Vector3 originPosition;
    Quaternion originRotation;

    // Camera Shake Float Variables
    float shake_decay;
    float shake_intensity;

    // 
    [SerializeField]
    HUD PlayerHUD;

    #endregion

    // Use this for initialization
    void Start()
    {
        ResetStats();
        _previousTime = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Called Functions
        UpdatePosition();
        UpdateRotation();
        //UpdateRoll();
        UpdateFiring();
        ActivateBoost();

        // Camera Shake (Work In Progress) Part of De-activated Camera Shake Function
        //if (shake_intensity > 0)
        //{
        //    transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
        //    transform.rotation = new Quaternion(
        //                    originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .2f,
        //                    originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .2f,
        //                    originRotation.z + Random.Range(-shake_intensity, shake_intensity) * .2f,
        //                    originRotation.w + Random.Range(-shake_intensity, shake_intensity) * .2f);
        //    shake_intensity -= shake_decay;
        //}
    }

    // Update the position of the ship based on "Horizontal" and "Vertical" input
    void UpdatePosition()
    {
        // Move the player laterally in the 'X' coordinate
        movement.x = Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeedX;
        // Move the player laterally in the 'Y' coordinate
        movement.y = Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeedY;
        // Apply the movement vector to the game object's position
        gameObject.transform.Translate(movement);
        // Transform the 3D world position to a screen pixel location
        Vector3 screenPosition = CameraObject.WorldToScreenPoint(
                                    gameObject.transform.position);
        // X Axis Player Movement
        // Off screen to the RIGHT?
        if (screenPosition.x > ((100.00 * Screen.width) / 100.00))
        {
            screenPosition.x = 0;
            // Transform clamped screen position to world space and
            // assign to player ship
            gameObject.transform.position =
                CameraObject.ScreenToWorldPoint(screenPosition);
        }
        //// Off screen o the LEFT?
        //double x = ((100.00 * Screen.width) / 100.00);
        //double y = ((100.00 * Screen.height) / 100.00);
        else if (screenPosition.x < 0)
        {
            // Clamp (reset) to the screen's left side
            screenPosition.x = (((100 * Screen.width) / 100));
            // Transform clamped screen position to world space and
            // assign to player ship
            gameObject.transform.position =
                CameraObject.ScreenToWorldPoint(screenPosition);
        }
        // Y Axis Player Movement
        // Off screen to the TOP?
        if (screenPosition.y > Screen.height)
        {
            // Clamp (reset) to the screen's top side
            screenPosition.y = Screen.height;
            // Transform clamped screen position to world space and
            // assign to player ship
            gameObject.transform.position =
                CameraObject.ScreenToWorldPoint(screenPosition);
        }
        // Off screen o the BOTTOM?
        else if (screenPosition.y < 0)
        {
            // Clamp (reset) to the screen's bottom side
            screenPosition.y = 0;
            // Transform clamped screen position to world space and
            // assign to player ship
            gameObject.transform.position =
                CameraObject.ScreenToWorldPoint(screenPosition);
        }
    }

    void OnBecameInvisible()
    {
        Debug.Log("Poof0");
        //var cam = CameraObject;
        var viewportPosition = CameraObject.WorldToViewportPoint(transform.position);
        var newPositon = transform.position;
        Debug.Log("Poof1");
        if (viewportPosition.x > 1 || viewportPosition.x < 0)
        {
            newPositon.x = -newPositon.x;
            Debug.Log("Poof");
        }
        Debug.Log("Poofend");
    }


    // Rotate the ship
    void UpdateRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(transform.forward, rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(transform.forward, -rotateSpeed * Time.deltaTime);
        }
    }

    #region De-activiated Barrel Roll Function
    // Roll the ship (Commented out due to it breaking the 2D game space. 
    // Uncomment to try it out, but be warned you may fly the ship through 
    // the starfield or behind the camera.
    // void UpdateRoll()
    //{
    //    if (Input.GetKey(KeyCode.Z))
    //    {
    //        transform.Rotate(transform.up, rotateSpeed * Time.deltaTime);
    //    }
    //    else if (Input.GetKey(KeyCode.X))
    //    {
    //        transform.Rotate(transform.up, -rotateSpeed * Time.deltaTime);
    //    }
    //}
    #endregion

    #region De-activated Camera Shake Function
    // De-Activate because it caused issues with camera position post-shake, 
    // but it semi works.
    //public void Shake()
    //{
    //    originPosition = transform.position;
    //    originRotation = transform.rotation;
    //    shake_intensity = .02f;
    //    shake_decay = 0.002f;
    //}
    #endregion

    // Update the firing of the ship based on "Fire1" input
    void UpdateFiring()
    {
        // Accumulate time each frame. When the fire key is
        // pressed, we check if enough time has passed.
        FireTimer += Time.deltaTime;
        // Detect if the fire button has been pressed.
        if (Input.GetButton("Fire1"))
        {
            if (FireTimer > FireRate)
            {
                FireTimer = 0;
                DoWeaponFire();
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
            }
        }
        // Multi-Shot Fire Mode
        else if (Input.GetKey(KeyCode.M))
        {
            if (FireTimer > FireRate)
            {
                FireTimer = 0;
                DoMultiWeaponFire();
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
            }
        }
        // Burst Fire Mode
        //else if (Input.GetKey(KeyCode.B))
        //{
        //    if (FireTimer > FireRate)
        //    {
        //        DoBurstWeaponFire();
        //    }
        //}
    }

    // Function that creates moving Bullet
    void DoWeaponFire()
    {
        Instantiate(Bullet, transform.position, transform.rotation);
        //print("The \"DowWeaponFire\" function has been called!");
    }

    // Function that creates multiple moving Bullet
    void DoMultiWeaponFire()
    {
        Vector3 _bullet1Vector = new Vector3(transform.position.x - 0.20f, transform.position.y);
        Vector3 _bullet2Vector = new Vector3(transform.position.x + 0.20f, transform.position.y);

        Instantiate(Bullet, _bullet1Vector, transform.rotation);

        Instantiate(Bullet, _bullet2Vector, transform.rotation);

        //print("The \"DowWeaponFire\" function has been called!");
    }

    // Function that creates Burst fire
    void DoBurstWeaponFire()
    {
        if (_previousTime == FireTimer / 2)
        {
            Vector3 _bullet1Vector = new Vector3(transform.position.x - 0.20f, transform.position.y);
            Vector3 _bullet2Vector = new Vector3(transform.position.x + 0.20f, transform.position.y);

            Instantiate(Bullet, _bullet1Vector, transform.rotation);

            Instantiate(Bullet, _bullet2Vector, transform.rotation);
            _previousTime = 10;
        }
        else if(_previousTime == 2)
        {
            _previousTime += FireTimer;
            Instantiate(Bullet, transform.position, transform.rotation);
        }
    }

    // Ship Thruster Activate Function
    void ActivateBoost()
    {
        // Is the player pressing the T key?
        if (Input.GetKey(KeyCode.T))
        {
            if (boostEnabled && ThrusterCooldown == false) { return; }

            if (firstTimeBoost)
            {
                OriginalMoveSpeedY = MoveSpeedY;
                OriginalMoveSpeedX = MoveSpeedX;
                firstTimeBoost = false;
            }

            BoostShip();
            boostEnabled = true;
            //ThrusterCooldown = true;
            //ThrustCool();
        }
        // Return to normal movement speed
        else
        {
            MoveSpeedY = OriginalMoveSpeedY;
            MoveSpeedX = OriginalMoveSpeedX;
            boostEnabled = false;
            firstTimeBoost = true;
        }
    }

    // Ship Thruster Move Speed Function
    void BoostShip()
    {
        MoveSpeedY += turboSpeed;
        MoveSpeedX += turboSpeed;
    }

    // Ship Thruster Cooldown
    void ThrustCool()
    {
        for (int x = 1; x < 600; x++)
        {
            ThrusterCooldown = false;
        }
    }

    // Resets player stats at initialization
    void ResetStats ()
    {
        numShotsFired = 0;
        numEnemyHits = 0;
        Score = 0;
        // MaxLives is public and set in the editor.
        // It is not reset; rather, used as the
        // default value for the current lives count.
        CurLives = MaxLives;

        PlayerHUD.CreateLifeIcons(MaxLives);

        PlayerHUD.updateLives(CurLives);
    }
    
    // Modifies the number of enemis hit
    public void ModEnemyHits (int _hits)
    {
        numEnemyHits += _hits;
    }

    // Modifies the player's score
    public void ModScore (int _value)
    {
        Score += _value;
        PlayerHUD.updateScore(Score);
    }

    // Modifies the number of lives the player has
    public void ModLives (int _value)
    {
        CurLives += _value;

        PlayerHUD.updateLives(CurLives);
    }

    void OnTriggerEnter (Collider other)
    {
        Debug.Log("Nope");
        if (other.tag == "Pickup")
        {
            if(other.name == "Score")
            {
                ModScore(5);

                Destroy(other.gameObject);
            }
        }
        else if (other.tag == "EnemyShip") //Top" || other.tag == "EnemyShipSide")
        {
            ModLives(-1);
            Debug.Log("Yo");
        }

    }
}
