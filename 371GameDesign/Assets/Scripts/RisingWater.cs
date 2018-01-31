using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : MonoBehaviour {

	private float waterSpeed = 0.2f;
	private Rigidbody2D rb2d;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.velocity = new Vector2 (0,waterSpeed);
	}

	// Update is called once per frame
	void Update () {

	}
}
