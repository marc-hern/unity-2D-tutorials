﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter {

	private BoxCollider2D boxCollider;

	private Vector3 moveDelta;

	private RaycastHit2D hit;

	// Use this for initialization
	private void Start () {
		boxCollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	// void Update () {
		
	// }

	private void FixedUpdate(){
		// Reset MoveDelta
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");
		moveDelta = new Vector3(x, y, 0);

		// Swap the sprite direction when moving right or left
		if (moveDelta.x > 0){
			transform.localScale = Vector3.one;
		} 
		else if (moveDelta.x < 0){
			transform.localScale = new Vector3(-1, 1, 1);
		}

		// Make sure we can move in this direction, by casting a box there first
		//  if the box returns null we are free to move
		hit = Physics2D.BoxCast(
			transform.position,
			boxCollider.size,
			0,
			new Vector2(0, moveDelta.y),
			Mathf.Abs(moveDelta.y * Time.deltaTime),
			LayerMask.GetMask("Actor", "Blocking")
		);

		if (hit.collider == null){
			// Move the character move
			transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
		}

		hit = Physics2D.BoxCast(
			transform.position,
			boxCollider.size,
			0,
			new Vector2(moveDelta.x, 0),
			Mathf.Abs(moveDelta.x * Time.deltaTime),
			LayerMask.GetMask("Actor", "Blocking")
		);

		if (hit.collider == null){
			// Move the character move
			transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
		}
	}
}
