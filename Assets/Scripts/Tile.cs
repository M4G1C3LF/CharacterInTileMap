using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	SpriteRenderer spriteRenderer;

	GameObject light;

	bool illuminated;


	public static int lightOffIdleState;

	public void Illuminate()
	{
		illuminated = true;
	}

	public void Obscure()
	{
		illuminated = false;
	}

	public void CheckIllumination()
	{
		if (light.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).nameHash == lightOffIdleState)
		{
			Obscure ();
		}
		if (!illuminated)
		{
			spriteRenderer.color = Color.black;
		}
		else
		{
			spriteRenderer.color = Color.white;
		}


	}



	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();

		illuminated = false;

		light = GameObject.Find ("Darkness");

		lightOffIdleState = Animator.StringToHash("Base Layer.LightOffIdle");
	}
	
	// Update is called once per frame
	void Update () {
	
		CheckIllumination ();
	}
}
