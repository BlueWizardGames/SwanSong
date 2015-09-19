using UnityEngine;
using System.Collections;
using InControl;
using weaponsSystem;
using SControls;

[System.Serializable]
public class Done_Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public GameObject shot0;
	public GameObject shot1;
	public GameObject shot2;
	public GameObject shot3;
	public GameObject shot4;
	public Transform shotSpawn0;
	public Transform shotSpawn1;
	public Transform shotSpawn2;
	public float previousInputVM = 0;
	public float previousInputHM = 0;
	private float nextFire;
	private bool archetypeState = false;
	private ComposedWeapon weaponRT;
	private ComposedWeapon weaponLT;
	private ComposedWeapon weaponLB;
	private ComposedWeapon weaponRB;
	private ComposedWeapon archetyp;
	private ShipActions bcn;

	void Start ()
	{
		// instantiate composed weapons here.
		weaponRT = WCore.composeWeapon (WCore.burstFire(4, 0.1f, 0.4f),
		                                WCore.defaultFire(),
		                                WCore.smoothedActuation(.07f,
		                    		    .2f,
		                        		.3f),
		                                shot0);
		weaponLT = WCore.composeWeapon (WCore.burstFire(2, 0.05f, 0.5f),
		                                WCore.defaultFire(),
		                                WCore.smoothedActuation(.07f,
		                        		.2f,
		                        		.3f),
		                                shot1);
		weaponRB = WCore.composeWeapon (WCore.defaultCooldown(0.02f),
		                                WCore.angleFire(45),
		                                WCore.defaultActuation(),
		                                shot2);
		weaponLB = WCore.composeWeapon (WCore.defaultCooldown(0.02f),
		                                WCore.angleFire(-45),
		                                WCore.defaultActuation(),
		                                shot3);
		archetyp = WCore.composeWeapon (WCore.burstFire (40, 0.001f, 10f),
		                                WCore.triFire (20),
		                                WCore.smoothedActuation (.1f,
		                         		.2f,
		                       			.5f),
		                                shot4);
		bcn = new ShipActions ();
		bcn.bL.AddDefaultBinding (InputControlType.LeftBumper);
		bcn.bR.AddDefaultBinding (InputControlType.RightBumper);
		bcn.tL.AddDefaultBinding (InputControlType.LeftTrigger);
		bcn.tR.AddDefaultBinding (InputControlType.RightTrigger);
		//MOVE STICK MOVE STICK MOVE MOVE MOVE
		bcn.Down.AddDefaultBinding (InputControlType.LeftStickDown);
		bcn.Up.AddDefaultBinding (InputControlType.LeftStickUp);
		bcn.Left.AddDefaultBinding (InputControlType.LeftStickLeft);
		bcn.Right.AddDefaultBinding (InputControlType.LeftStickRight);
		//LOOK STICK LOOK
		bcn.lookDown.AddDefaultBinding (InputControlType.RightStickDown);
		bcn.lookUp.AddDefaultBinding (InputControlType.RightStickUp);
		bcn.lookLeft.AddDefaultBinding (InputControlType.RightStickLeft);
		bcn.lookRight.AddDefaultBinding (InputControlType.RightStickRight);
	}
	
	void Update ()
	{
		bool[] m = new bool[4]{false, false, false, false}; 
		bool weaponsLocked = false;

		if (bcn.bL.IsPressed) {
			m [0] = true;
		}

		if (bcn.bR.IsPressed) {
			m [1] = true;
		}

		if (bcn.tL.RawValue > 0) 
		{
			m [2] = true;
		}

		if (bcn.tR.RawValue > 0) 
		{
			m [3] = true;
		}
		
		archetypeState = ( m [0] && m [1] && m [2] && m [3] );
		if (archetypeState) {
			m[0] = false; m[1] = false;
			m[2] = false; m[3] = false;
			weaponsLocked = true;
		}

		if (m[0]
		    && !weaponsLocked) {
			weaponLB.shootFrom(shotSpawn0);	
		}

		if (m[1]
		    && !weaponsLocked) {
			weaponRB.shootFrom(shotSpawn1);
		}

		if ( m[2] && !m[0]
		    && !weaponsLocked) {
			weaponLT.shootFrom(shotSpawn0);
		}

		if (m[3] && !m[1]
		    && !weaponsLocked) {
			weaponRT.shootFrom(shotSpawn1);
		}
				
		if (weaponsLocked) {
			archetyp.shootFrom (shotSpawn2);
		}

		
	}
	
	void FixedUpdate ()
	{

		float t = 0;
		float moveHorizontal = bcn.mAxs.X;
		float moveVertical = bcn.mAxs.Y;
		t = moveHorizontal;
		moveHorizontal = moveHorizontal * previousInputHM;
		previousInputHM = Mathf.Abs (t) * speed * 0.5f;
		t = moveVertical;
		moveVertical = moveVertical * previousInputVM;
		previousInputVM = Mathf.Abs (t) * speed * 0.5f;
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody> ().velocity = (movement * speed);

		float rotY = bcn.aAxs.Y;
		float rotX = bcn.aAxs.X;
		if ((Mathf.Abs (rotX) + Mathf.Abs (rotY)) > .9) {
			
			float dir = Mathf.Atan2 (rotX, rotY) * (180 / Mathf.PI);
			if (Mathf.Abs (GetComponent<Rigidbody> ().rotation.eulerAngles.y - dir) > 1) {
				GetComponent<Rigidbody> ().MoveRotation (Quaternion.Euler (0, dir, 0));
				GetComponent<Rigidbody> ().maxAngularVelocity = 0;
			}
		}
	}
}
