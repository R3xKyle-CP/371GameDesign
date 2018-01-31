using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public bool alive = true;
    public float maxSpeed = 3;
    private float speed = 50f;
    private float jumpingSpeed = 300f;
    public bool grounded;
    private Rigidbody2D rb2d;
    //private bool grounded;
    private Animator anim;
	public GameObject gameOverText;

	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //grounded = true;
	}
	
	void Update () {

        if (alive)
        {
            anim.SetBool("grounded", grounded);
            anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));

            if (Input.GetAxis("Horizontal") < -0.1f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (Input.GetKeyDown("space") && grounded)
            {
                rb2d.AddForce(Vector2.up * jumpingSpeed);
                anim.SetTrigger("Jump");
            }
        }
        else if (alive == false && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    void FixedUpdate()
    {

        if (alive)
        {
            // Friction
            if (grounded)
            {
                Vector3 easeVelocity = rb2d.velocity;
                easeVelocity.y = rb2d.velocity.y;
                easeVelocity.z = 0.0f;
                easeVelocity.x *= 0.75f;
                rb2d.velocity = easeVelocity;
            }

            float horizontalSpeed = Input.GetAxis("Horizontal");

            rb2d.AddForce((Vector2.right * speed) * horizontalSpeed);


            // Limits speed of player
            if (rb2d.velocity.x > maxSpeed)
            {
                rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
            }

            if (rb2d.velocity.x < -maxSpeed)
            {
                rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
            }

            /*
            float horizontalSpeed = Input.GetAxis("Horizontal");

            // Player must be grounded in order to be able to jump
            if (Input.GetKeyDown("space") && grounded)
            {
                rb2d.AddForce(new Vector2(horizontalSpeed, jumpingSpeed) * speed);
            }
            else
            {
                rb2d.AddForce(new Vector2(horizontalSpeed, 0f) * speed);
            }
            */
        }

    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        // This requires our objects that are on the ground to have a 'Ground' tag
        if (col.gameObject.tag == "Ground" && alive)
        {
            grounded = true;
        }
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		// This requires our objects that are on the ground to have a 'Ground' tag
		if (col.gameObject.tag == "Water" && alive)
		{
			alive = false;
			gameOverText.SetActive (true);
		}
	}
    
}
