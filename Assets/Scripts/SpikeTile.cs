using UnityEngine;
using System.Collections;

public class SpikeTile : Tile {
	private bool spikesOut = false;

	public int spikeFrequency = 50;
	public int actualFrequency = 0;

	private void CheckSpikeFrequency()
	{
		if (spikeFrequency-actualFrequency == 0)
		{
			if (spikesOut)
			{
				spikesOut = false;
			}
			else
			{
				spikesOut = true;
			}
			actualFrequency=0;
		}
		actualFrequency++;
	}

	private void ShowSpikes()
	{
		spikesOut = true;
	}

	private void HideSpikes()
	{
		spikesOut = false;
	}
	
	private void CheckSpikes()
	{

		if (spikesOut && (actualFrequency == spikeFrequency) )
		{
			//AQUI HAY QUE LLAMAR AL ANIMATOR PARA QUE SE VEA
			Debug.Log("Mostrando Pinchos");
		}
		else if ( !spikesOut && (actualFrequency == spikeFrequency) )
		{
			//AQUI HAY QUE LLAMAR AL ANIMATOR QUE SE VEA
			Debug.Log("Escondiendo Pinchos");
		}
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		CheckSpikeFrequency ();
		CheckSpikes ();


	}
}
