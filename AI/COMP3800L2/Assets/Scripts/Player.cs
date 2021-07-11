using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 15.0f;
    float rotSpeed = 20f;
    private float x, y, z;

    public float jumpSpeed = 5f;    // instantiate the jump force
    private Rigidbody rb;

    private CapsuleCollider col;
    public GameObject bullet;
    public float bulletSpeed = 50f;

    public LayerMask groundLM;
    public float h = 0.1f;
    private int health = 100;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // We are instanciating a rigib boyd component
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal") * Time.deltaTime * speed; // We are getting the position of the x-axis and multiplying it by time and initial speed so we can move right or left
        z = Input.GetAxis("Vertical") * Time.deltaTime * speed; // We are getting the position of the x-axis and multiplying it by time and initial speed so we can move up and down
        y = Input.GetAxis("Horizontal") * Time.deltaTime * (4 * rotSpeed); // We are getting the position of the x-axis and multiplying it by time and initial rotation speed so we can rotate

        transform.Translate(Vector3.forward * z);    // This is what let us move the object in the given direction depending on the value on the z-value   
        transform.Rotate(Vector3.up * y); // This allows us to move upward the object

        // Recover Health when spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            Debug.Log("Health Before: " + health);
            health = 100;
            Debug.Log("Health After: " + health);
        }


        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet,
                this.transform.position + new Vector3(1, 0, 0),
                this.transform.rotation) as GameObject;

            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();

            bulletRB.velocity = this.transform.forward * bulletSpeed;
        }
    }

    void FixedUpdate() //called before each internal physics update
    {
        Vector3 rotation = Vector3.up * y; //7
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime); // We are rotating y(rotation) degrees around y-axis
        rb.MovePosition(this.transform.position + this.transform.forward * x * Time.fixedDeltaTime); // We're moving the position of our rigid body given current + future position times x-axis and time
        rb.MoveRotation(rb.rotation * angleRot); // Similar to the idea above, but we're rotating our object given an angle
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(col.bounds.center, capsuleBottom, h, groundLM,
            QueryTriggerInteraction.Ignore);
        return grounded;
    }

}
