using UnityEngine;
using System.Collections;

public class SpikeTile : Tile {
	private bool spikesOut = false;

	public int spikeFrequency = 50;
	public int actualFrequency = 0;


	private void ShowSpikes()
	{
		spikesOut = true;
		GetComponent<Animator>().SetBool ("spikesOut",spikesOut);

	}

	private void HideSpikes()
	{
		spikesOut = false;
		GetComponent<Animator>().SetBool ("spikesOut",spikesOut);
	}

	private void CheckSpikes()
	{
		if (spikeFrequency-actualFrequency == 0)
		{
			if (spikesOut)
			{
				HideSpikes();
			}
			else
			{
				ShowSpikes ();
			}
			actualFrequency=0;
		}
		actualFrequency++;
	}


	void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.tag == "Player" && spikesOut)
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

		CheckSpikes ();

	}
}
