using UnityEngine;
using System.Collections;

public class HoleTile : Tile {

	void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.tag == "Player")
		{
			collider.GetComponentInChildren<Animator>().SetBool ("dead",true);
		}
		
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		

	}
}
