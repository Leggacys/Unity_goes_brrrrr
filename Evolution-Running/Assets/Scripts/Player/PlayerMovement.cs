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

	[Header("Attack")]
	public GameObject projectile;
	public float throwPower;
	public bool canAttack;
	public float timeBetweenAttacks;
	public float homingProjectiles;
	public float attackRate;

    [Header("Dash")] 
    public bool canDash;
    public float dashCooldown;
    public float dashForce;

    [Header("GroundPound")] 
    public float groundForce;
    public float slamForce;
    private bool isSlamming;
    private float initialY;
    
    private Vector3 direction;
	public LayerMask ground;
	public float raycastDistance;


	[HideInInspector]
	public  bool takeHit;
	
	private Vector3 velocity = new Vector3(0 , 0 ,2);
	private Rigidbody rb;
	private float time;
	
	public void OnEnable()
	{
		rb = GetComponent<Rigidbody>();
		//StartCoroutine(Unstuck());
	}
	
	private void FixedUpdate()
	{
		if (!takeHit)
		{
			rb.MovePosition(  rb.position + new Vector3(0,velocity.y,
				velocity.z* PlayerManager.instace.speed) * Time.fixedDeltaTime);
			Gravity();
			//TakeGroundAngle();
		}
		
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		PlayerAnimations.instance.FallAnimation();
		isGrounded = true;
		if (isSlamming)
		{
			SlamAttack();
		}
		
	}

	private void SlamAttack()
	{
		isSlamming = false;
		float radius = Mathf.Abs(initialY - transform.position.y);
		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
		Debug.Log("here");
		Debug.Log(radius);
		foreach (Collider hit in colliders)
		{
			if (hit.gameObject.transform.root != transform && hit.gameObject.tag != "NoMove")
			{
				if (hit.gameObject.name.Contains("Platform"))
				{
					PlatformRegeneration regen = hit.GetComponent<PlatformRegeneration>();
					if (regen)
					{
						regen.onDestruction();
					}
				}
				Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();
				if (rb)
				{
					Debug.Log("Applying force");
					rb.constraints = RigidbodyConstraints.None;
					rb.isKinematic = false;
					//hit.gameObject.transform.parent = null;
					rb.AddExplosionForce(slamForce * radius, transform.position, radius);
					
				}
			}
		}

		
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
		//PlayerAnimations.instance.JumpEffect(PlayerManager.instace.playerBody);
		
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


	public void Attack()
    {
	    
	    
        GameObject newProjectile = Instantiate(projectile,transform.position + transform.forward + transform.up, Quaternion.identity);
        if (homingProjectiles > 0)
        {
	        newProjectile.GetComponent<HomingProjectile>().enabled = true;
	        homingProjectiles--;
        }
        else
			newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward*throwPower + transform.up/2,ForceMode.Impulse);
        StartCoroutine(attackCooldown());
        
    }

    IEnumerator attackCooldown(){
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
    }

    public void Dash()
    {
	    StartCoroutine(updateSpeed());
    }

    IEnumerator updateSpeed()
    {
	    //PlayerManager.instace.speed *= 2;
	    rb.AddForce(transform.forward*dashForce,ForceMode.Impulse);
	    yield return new WaitForSeconds(dashCooldown);
	    //PlayerManager.instace.speed /= 2;
	    canDash = true;
    }

    public void Land()
    {
	    Debug.Log("LANDING");
	    StopCoroutine(HitGround());
	    StartCoroutine(HitGround());
    }

    IEnumerator HitGround()
    {
	    isSlamming = true;
	    initialY = transform.position.y;
	    while (!isGrounded)
	    {
		    rb.AddForce(Vector3.down * groundForce,ForceMode.Impulse);
		    yield return new WaitForSeconds(0.05f);
	    }
    }



    public void TakeHitEffect()
    {
	    rb.velocity=Vector3.zero;
	    rb.AddForce(new Vector3(0f,20,-20f),ForceMode.Impulse);
    }

    
}
