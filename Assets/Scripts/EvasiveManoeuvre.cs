using UnityEngine;
using System.Collections;

public class EvasiveManoeuvre : MonoBehaviour {

	public float dodge;
	public float smoothing;
	public float tilt;

	// we can use this for generating a range
	public Vector2 startWait;
	public Vector2 manoeuvreTime;
	public Vector2 manoeuvreWait;

	public Boundary boundary;

	private float currentSpeed;
	private float targetManoeuvre;
	private Rigidbody rb;

	void Start () 
	{
		rb = GetComponent <Rigidbody> ();
		currentSpeed = rb.velocity.z;
		StartCoroutine (Evade ());
	}

	IEnumerator Evade ()
	{
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

		while (true) 
		{
			targetManoeuvre = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);
			yield return new WaitForSeconds (Random.Range (manoeuvreTime.x, manoeuvreTime.y));
			targetManoeuvre = 0;
			yield return new WaitForSeconds (Random.Range (manoeuvreWait.x, manoeuvreWait.y));
		}
	}

	void FixedUpdate () 
	{
		float newManoeuvre = Mathf.MoveTowards (rb.velocity.x, targetManoeuvre, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (newManoeuvre, 0.0f, currentSpeed);
		//clamps position
		rb.position = new Vector3
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
