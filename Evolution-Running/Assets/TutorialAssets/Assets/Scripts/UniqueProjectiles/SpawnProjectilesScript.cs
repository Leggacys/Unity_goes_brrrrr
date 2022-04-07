using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnProjectilesScript : MonoBehaviour {
	public List<GameObject> VFXs = new List<GameObject> ();
	private float timeToFire = 0f;
	private GameObject effectToSpawn;

	void Start ()
	{
		effectToSpawn = VFXs[0];
	}

	void Update () {
		
		//if (Input.GetKey (KeyCode.Space) && Time.time >= timeToFire || Input.GetMouseButton (0) && Time.time >= timeToFire) {
			//timeToFire = Time.time + 1f / effectToSpawn.GetComponent<ProjectileMoveScript>().fireRate;
			//SpawnVFX ();	
		//}
		
	}

	[ContextMenu("TestMeteor")]
	public void SpawnVFX () {
		GameObject vfx;
		vfx = Instantiate (effectToSpawn);
	}
}
