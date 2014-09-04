using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	GameObject link;
	Animator linkAnimator;

	GameObject darkness;
	Animator darknessAnimator;

	float verticalAxis; 
	float horizontalAxis;
	Vector3 localScale;


	int moveSideState;

	int lighterWarmLevel = 0;
	int lighterMaxWarmLevel = 1000;
	bool lighterIsOverheated = false;

	public float translationSpeed = 0.01f;	//USE THIS VAR TO CONFIG THE SPEED OF THE PLAYER




	public void checkAnimation()
	{

		linkAnimator.SetFloat ("verticalAxis",verticalAxis);

		if (verticalAxis != 0)
		{
			linkAnimator.SetBool("isMoving",true);

			link.transform.localScale = localScale;
		
		}


		if (horizontalAxis != 0)
		{
			linkAnimator.SetBool ("isMoving",true);
			
			
			if(horizontalAxis > 0)
			{
				
				linkAnimator.SetBool ("isMoving",true);
				linkAnimator.SetBool ("movingAtSide",true);
				
				
				link.transform.localScale = localScale;
				
			}
			else if (horizontalAxis < 0)
			{
				linkAnimator.SetBool ("movingAtSide",true);

				
				if (linkAnimator.GetCurrentAnimatorStateInfo(0).nameHash == moveSideState)
				{
					link.transform.localScale = new Vector3 (-localScale.x,localScale.y,localScale.z);
				}
				
			}
		}
		else
		{
			
			linkAnimator.SetBool ("movingAtSide",false);
		}

		
		if (horizontalAxis == 0 && verticalAxis == 0)
		{
			linkAnimator.SetBool ("isMoving",false);

		}

	}
	public void getAxis()
	{
		verticalAxis = Input.GetAxis ("Vertical");
		horizontalAxis = Input.GetAxis ("Horizontal");
	}


	public void checkMovement()
	{
		float x = horizontalAxis*translationSpeed;
		float y = verticalAxis*translationSpeed;

		this.transform.position = new Vector3 (this.transform.position.x + x, this.transform.position.y + y, this.transform.position.z);
	}

	public void checkLighterOverheat()
	{
		if (this.lighterIsOverheated)
		{
			if (this.lighterWarmLevel == 0)
			{
				this.lighterIsOverheated = false;
			}
		}

		if (this.lighterWarmLevel >= this.lighterMaxWarmLevel)
		{
			this.lighterIsOverheated = true;
		}
	}

	public void freezeLighter(int unit)
	{
		if (lighterWarmLevel > 0)
		{
			this.lighterWarmLevel -= unit;
		}

		if (this.lighterWarmLevel < 0)
		{
			this.lighterWarmLevel = 0;
		}

	}

	public void warmLighter(int unit)
	{
		if (!lighterIsOverheated)
		{
			this.lighterWarmLevel += unit;
		}

	}
	public void checkLighter()
	{
		this.checkLighterOverheat ();

		if (Input.GetButton("Fire1") && !lighterIsOverheated)
		{
			darknessAnimator.SetBool ("lightEnabled",true);
			warmLighter(1);
		}
		else
		{
			darknessAnimator.SetBool ("lightEnabled",false);
			freezeLighter(1);
		}

		Debug.Log (this.lighterWarmLevel);
	}
	// Use this for initialization
	void Start () {
	
		link = GameObject.Find ("LinkSprite");
		linkAnimator = link.GetComponent<Animator> ();
		localScale =link.transform.localScale;
		moveSideState = Animator.StringToHash("Moves.MoveSide");

		darkness = GameObject.Find ("Darkness");
		darknessAnimator = darkness.GetComponent<Animator> ();


	}

	// Update is called once per frame
	void Update () {
		getAxis ();
		checkAnimation ();
		checkMovement ();
		checkLighter();


	}
}
