using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour {
	[RequireComponent(typeof (Rigidbody))]
	[RequireComponent(typeof (CapsuleCollider))]

	[Serializable]
	public class MovementSettings
	{
		public float ForwardSpeed = 8.0f;   // Speed when walking forward
		public float BackwardSpeed = 4.0f;  // Speed when walking backwards
		public float StrafeSpeed = 4.0f;    // Speed when walking sideways
		public float RunMultiplier = 2.0f;   // Speed when sprinting
		public KeyCode RunKey = KeyCode.LeftShift;
		public float JumpForce = 50f;
		public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
		[HideInInspector] public float CurrentTargetSpeed = 8f;
		
		#if !MOBILE_INPUT
		private bool m_Running;
		#endif
		
		public void UpdateDesiredTargetSpeed(Vector2 input)
		{
			if (input == Vector2.zero) return;
			if (input.x > 0 || input.x < 0)
			{
				//strafe
				CurrentTargetSpeed = StrafeSpeed;
			}
			if (input.y < 0)
			{
				//backwards
				CurrentTargetSpeed = BackwardSpeed;
			}
			if (input.y > 0)
			{
				//forwards
				//handled last as if strafing and moving forward at the same time forwards speed should take precedence
				CurrentTargetSpeed = ForwardSpeed;
			}
			#if !MOBILE_INPUT
			if (Input.GetKey(RunKey))
			{
				CurrentTargetSpeed *= RunMultiplier;
				m_Running = true;
			}
			else
			{
				m_Running = false;
			}
			#endif
		}
		
		#if !MOBILE_INPUT
		public bool Running
		{
			get { return m_Running; }
		}
		#endif
	}

	[Serializable]
	public class AdvancedSettings
	{
		public float groundCheckDistance = 0.01f; // distance for checking if the controller is grounded ( 0.01f seems to work best for this )
		public float stickToGroundHelperDistance = 0.5f; // stops the character
		public float slowDownRate = 20f; // rate at which the controller comes to a stop when there is no input
		public bool airControl; // can the user control the direction that is being moved in the air
	}
	
	public Camera cam;
	public MovementSettings movementSettings = new MovementSettings();
	public AdvancedSettings advancedSettings = new AdvancedSettings();
	public MouseLook mouseLook = new MouseLook();

	private Rigidbody rigidbody;
	private CapsuleCollider m_Capsule;
	private Vector3 m_GroundContactNormal;
	private bool m_Jump, m_PreviouslyGrounded, m_Jumping, m_IsGrounded;

	// Use this for initialization
	void Start () {
		mouseLook.Init (transform, cam.transform);
		rigidbody = GetComponent<Rigidbody> ();
		m_Capsule = GetComponent<CapsuleCollider>();
	}

	private void Update()
	{
		RotateView();
		
		if (CrossPlatformInputManager.GetButtonDown("Jump") && !m_Jump)
		{
			m_Jump = true;
		}
	}

	// Update is called once per frame
	private void FixedUpdate()
	{
		GroundCheck();
		Vector2 input = GetInput();
		
		if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && (advancedSettings.airControl || m_IsGrounded))
		{
			// always move along the camera forward as it is the direction that it being aimed at
			Vector3 desiredMove = cam.transform.forward*input.y + cam.transform.right*input.x;
			desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;
			
			desiredMove.x = desiredMove.x*movementSettings.CurrentTargetSpeed;
			desiredMove.z = desiredMove.z*movementSettings.CurrentTargetSpeed;
			desiredMove.y = desiredMove.y*movementSettings.CurrentTargetSpeed;
			if (rigidbody.velocity.sqrMagnitude <
			    (movementSettings.CurrentTargetSpeed*movementSettings.CurrentTargetSpeed))
			{
				rigidbody.AddForce(desiredMove*SlopeMultiplier(), ForceMode.Impulse);
			}
		}
		
		if (m_IsGrounded)
		{
			rigidbody.drag = 5f;
			
			if (m_Jump)
			{
				rigidbody.drag = 0f;
				rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
				rigidbody.AddForce(new Vector3(0f, movementSettings.JumpForce, 0f), ForceMode.Impulse);
				m_Jumping = true;
			}
			
			if (!m_Jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && rigidbody.velocity.magnitude < 1f)
			{
				rigidbody.Sleep();
			}
		}
		else
		{
			rigidbody.drag = 0f;
			if (m_PreviouslyGrounded && !m_Jumping)
			{
				StickToGroundHelper();
			}
		}
		m_Jump = false;
	}

	private float SlopeMultiplier()
	{
		float angle = Vector3.Angle(m_GroundContactNormal, Vector3.up);
		return movementSettings.SlopeCurveModifier.Evaluate(angle);
	}
	
	
	private void StickToGroundHelper()
	{
		RaycastHit hitInfo;
		if (Physics.SphereCast(transform.position, m_Capsule.radius, Vector3.down, out hitInfo,
		                       ((m_Capsule.height/2f) - m_Capsule.radius) +
		                       advancedSettings.stickToGroundHelperDistance))
		{
			if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
			{
				rigidbody.velocity = Vector3.ProjectOnPlane(rigidbody.velocity, hitInfo.normal);
			}
		}
	}
	
	private void RotateView()
	{
		//avoids the mouse looking if the game is effectively paused
		if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;
		
		// get the rotation before it's changed
		float oldYRotation = transform.eulerAngles.y;
		
		mouseLook.LookRotation (transform, cam.transform);
		
		if (m_IsGrounded || advancedSettings.airControl)
		{
			// Rotate the rigidbody velocity to match the new direction that the character is looking
			Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
			rigidbody.velocity = velRotation * rigidbody.velocity;
		}
	}

	private Vector2 GetInput()
	{
		
		Vector2 input = new Vector2
		{
			x = CrossPlatformInputManager.GetAxis("Horizontal"),
			y = CrossPlatformInputManager.GetAxis("Vertical")
		};
		movementSettings.UpdateDesiredTargetSpeed(input);
		return input;
	}

	/// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
	private void GroundCheck()
	{
		m_PreviouslyGrounded = m_IsGrounded;
		RaycastHit hitInfo;
		if (Physics.SphereCast(transform.position, m_Capsule.radius, Vector3.down, out hitInfo,
		                       ((m_Capsule.height/2f) - m_Capsule.radius) + advancedSettings.groundCheckDistance))
		{
			m_IsGrounded = true;
			m_GroundContactNormal = hitInfo.normal;
		}
		else
		{
			m_IsGrounded = false;
			m_GroundContactNormal = Vector3.up;
		}
		if (!m_PreviouslyGrounded && m_IsGrounded && m_Jumping)
		{
			m_Jumping = false;
		}
	}
}
