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
	
	private Vector3 velocity = new Vector3(0 , 0 ,1);
	private Rigidbody rb;
	

	[Header("Attack")]
	public GameObject projectile;
	public float throwPower;
	public bool canAttack;
	public float timeBetweenAttacks;
	public float homingProjectiles;

    [Header("Dash")] 
    public bool canDash;
    public float dashCooldown;
    public float dashForce;
    
    
	private Vector3 direction;
	public LayerMask ground;
	public float raycastDistance;

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
}
