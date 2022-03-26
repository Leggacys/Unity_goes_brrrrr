using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	#region Singleton

	public static PlayerMovement instance;
    
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		} else if(instance!=this)
		{
			Destroy(gameObject);
		}
	}
	

	#endregion
	
	public float gravity = -9.18f;
	public bool isGrounded { get; set; }
	public LayerMask ground;
	public float raycastDistance;

	private Vector3 velocity = new Vector3(2 , 0 ,2);
	private Rigidbody rb;
	private Vector3 direction;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	private void FixedUpdate()
	{
		rb.MovePosition(  rb.position + new Vector3(0,velocity.y,
			velocity.z* PlayerManager.instace.speed) * Time.fixedDeltaTime);
		Gravity();
		TakeGroundAngle();
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		PlayerAnimations.instance.FallAnimation();
		isGrounded = true;
	}
	
	private void Gravity()
	{
		if (isGrounded && velocity.y < 0)
		{
			velocity.y = -2f;
		}

		velocity.y += gravity * Time.deltaTime;
	}

	public void Jump()
	{
		isGrounded = false;	
		velocity.y = Mathf.Sqrt(PlayerManager.instace.jumpAmount * -2f * gravity);
		PlayerAnimations.instance.JumpEffect(PlayerManager.instace.playerBody);
		
	}

	void TakeGroundAngle()
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, -transform.up, out hit,raycastDistance, ground))
		{
			Quaternion angle = Quaternion.FromToRotation(transform.up, hit.normal);
			transform.rotation = Quaternion.Slerp(transform.rotation,angle * transform.rotation,10);
		}
	}
	
}
