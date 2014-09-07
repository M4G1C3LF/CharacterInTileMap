using UnityEngine;
using System.Collections;

public class Light : MonoBehaviour {
	
	GameObject player;


	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Tile")
		{
			collider.GetComponent<Tile>().Illuminate ();
		}

	}

	void OnTriggerStay2D(Collider2D collider) 
	{
		//Si no se utiliza esta funcion el tile donde reside centro del circulo quedara oscuro.
		if (collider.tag == "Tile")
		{
			collider.GetComponent<Tile>().Illuminate ();
		}
		
	}
	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.tag == "Tile")
		{
			collider.GetComponent<Tile>().Obscure();
		}

	}

	public void FollowPlayer()
	{
		transform.position = player.transform.position;
	}

	// Use this for initialization
	void Start () {

		player = GameObject.Find ("LinkWrapper");
	}
	
	// Update is called once per frame
	void Update () {
		FollowPlayer ();
	}
}
