using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	public Transform shotSpawn;
	public GameObject shot;
	public float fireRate;
	public float delay;

	private AudioSource audioSource;


	// Use this for initialization
	void Start () 
	{
		audioSource = GetComponent<AudioSource> ();
		//could introduce a level of randomisation in the fire rate
		InvokeRepeating ("Fire", delay, fireRate);
	}
	
	void Fire () 
	{
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		audioSource.Play ();
	}
}
