using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpingSpeed;
    private Rigidbody2D rb2d;
    private bool grounded;

	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        grounded = true;
	}
	
	void Update () {
		
	}

    void FixedUpdate()
    {
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

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // This requires our objects that are on the ground to have a 'Ground' tag
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

}
