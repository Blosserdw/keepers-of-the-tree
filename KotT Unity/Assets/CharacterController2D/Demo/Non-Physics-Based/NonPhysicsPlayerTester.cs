using UnityEngine;
using System.Collections;


public class NonPhysicsPlayerTester : MonoBehaviour
{
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;

    // controls
    public KeyCode jumpKey = KeyCode.W;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public bool freezeInput = false;

    // trigger handling
    private bool level1InPosition = false;
    public KeyCode passKey = KeyCode.Q;
    public KeyCode failKey = KeyCode.E;

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
	}


	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent( Collider2D col )
	{
		Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );

        if (col.gameObject.name == "WinTrigger")
        {
            Debug.Log("Level 1 Pass Call!");
            //GameManager.Instance.LevelPassEvent();

            freezeInput = true;

            // change the camera follow to just the one player now
            if (this == GameManager.Instance.contestant1Object)
            {
                // First contestant has been frozen, make camera follow the first person now
                GameObject.Find("Main Camera").camera.GetComponent<SmoothFollow>().target = GameObject.Find("Main Camera").camera.GetComponent<SmoothFollow>().target2;
                GameObject.Find("Main Camera").camera.GetComponent<SmoothFollow>().target2 = null;
            }
            else
            {
                // When second contestant is frozen, we just set their target to null so it follows the first contestant now
                GameObject.Find("Main Camera").camera.GetComponent<SmoothFollow>().target2 = null;
            }
        }

        if (col.gameObject.name == "FailTrigger")
        {
            Debug.Log("Level 1 Fail Call!");
            //GameManager.Instance.LevelFailEvent();

            freezeInput = true;
        }

        if (col.gameObject.name == "ButtonTrigger")
        {
            level1InPosition = true;
        }
	}


	void onTriggerExitEvent( Collider2D col )
	{
		Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );

        if (col.gameObject.name == "ButtonTrigger")
        {
            level1InPosition = false;
        }
	}

	#endregion


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;

		if( _controller.isGrounded )
			_velocity.y = 0;

		if( Input.GetKey( rightKey ) && !freezeInput)
		{
			normalizedHorizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "Run" ) );
		}
		else if( Input.GetKey( leftKey ) && !freezeInput)
		{
			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "Run" ) );
		}
		else
		{
			normalizedHorizontalSpeed = 0;

			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "Idle" ) );
		}


		// we can only jump whilst grounded
        if (_controller.isGrounded && Input.GetKeyDown(jumpKey) && !freezeInput)
		{
			_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
			_animator.Play( Animator.StringToHash( "Jump" ) );
		}


		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		_controller.move( _velocity * Time.deltaTime );


        // Special input for trigger areas
        if (!freezeInput)
        {
            if (level1InPosition)
            {
                if (Input.GetKeyDown(passKey))
                {
                    Debug.Log("Player Passed!");
                    GameManager.Instance.LevelVoteEvent(true);
                    freezeInput = true;
                }

                if (Input.GetKeyDown(failKey))
                {
                    Debug.Log("Player Failed!");
                    GameManager.Instance.LevelVoteEvent(false);
                    freezeInput = true;
                }
            }
        }
	}

}
