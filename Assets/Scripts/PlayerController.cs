using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour 
{
	private Rigidbody rb;
	private AudioSource weaponAudio;
	private float myTime;
	private float nextFire;


	public float speed;
	public float tilt;
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		weaponAudio = GetComponent<AudioSource> ();
		myTime = 0.0f;
		nextFire = 0.5f;
	}

	void Update ()
	{
		myTime = myTime + Time.deltaTime;

		if (Input.GetButton ("Fire1") && myTime > nextFire) {
			nextFire = myTime + fireRate;

			//does not need to be stored as a gameobject as the shot will just fly off
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);

			//resets the time and makes nextfire equal to the fireRate
			nextFire = nextFire - myTime;
			myTime = 0.0f;

			//make weapon noise
			weaponAudio.Play (); 
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}