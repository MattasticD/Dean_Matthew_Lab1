using UnityEngine;
using System.Collections;

public class EnemyControllerBasic : MonoBehaviour {

    [SerializeField]
    float MoveSpeedX;

    private ShipPlayerController PlayerShipCtrl;

    [SerializeField]
    GameObject[] PickupTypes;
    //[SerializeField]
    private float Frequency = 3.0f;
    //[SerializeField]
    private float Magnitude = 0.1f;
    private Vector3 axis;

    // Projectile Game Object Variable
    [Tooltip("The Game Object used as the projectile")]
    public EnemyProjectile Bullet;

    // Use this for initialization
    void Start ()
    {
        axis = transform.right;
        // Set the life-span until our object is destroyed
        Destroy(gameObject, 10.0f);
        // Find the player ship game object by name
        GameObject playerShip = GameObject.Find("PlayerShip");
        // Access the player ship's script component by type
        PlayerShipCtrl = playerShip.GetComponent<ShipPlayerController>();
	}

    Vector3 position = new Vector3();
    float t = 10f;

    // Update is called once per frame
    void Update ()
    {
        //transform.position +=
        //    transform.up * Time.deltaTime * MoveSpeedX;

        //transform.position += transform.up * Time.deltaTime * MoveSpeedX;
        //transform.position = transform.position + axis * Mathf.Cos(Time.time * Frequency) * Magnitude;

        transform.position += transform.up * MoveSpeedX * Time.deltaTime;
        transform.position = transform.position + axis * Magnitude * Mathf.Cos(t);
        

        t += Time.deltaTime;

        DoWeaponFire();
    }

    int iCounter = 0;
    void DoWeaponFire()
    {
        //yield return new WaitForSeconds(3);
        if (iCounter == 100)
        {
            Instantiate(Bullet, transform.position, transform.rotation);
            iCounter = 0;
        }
        iCounter++;
    }

    void OnTriggerEnter (Collider other)
    {
        // Hit the player's projectile?
        if(other.tag == "PlayerProjectile" || other.tag == "PlayerProjectileInstaKill")
        {
            //A projectile instant kills us
            Destroy(gameObject);

            SpawnPickup();
        }
        else if(other.tag == "PlayerShip")
        {
            // Wrecking with the player kills us
            Destroy(gameObject);
            // Remove points from the player for hitting this enemy
            PlayerShipCtrl.ModScore(-1);
        }
    }
    void SpawnPickup()
    {
        int randIndex = Random.Range(0, PickupTypes.Length);

        Instantiate(PickupTypes[randIndex],
                    transform.position, transform.rotation);
    }
}
