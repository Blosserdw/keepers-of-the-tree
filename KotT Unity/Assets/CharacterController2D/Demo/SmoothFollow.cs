using UnityEngine;
using System.Collections;



public class SmoothFollow : MonoBehaviour
{
	public Transform target;
    public Transform target2;
	public float smoothDampTime = 0.2f;
	[HideInInspector]
	public new Transform transform;
	public Vector3 cameraOffset = new Vector3(0,0,10f);
	public bool useFixedUpdate = false;
	
	private CharacterController2D _playerController;
	private Vector3 _smoothDampVelocity;

    float distanceX;
    float newPosX;
    float distanceY;
    float newPosY;
    Vector3 newTargetPos;
    Vector3 cameraTestVector;

	
	void Awake()
	{
		transform = gameObject.transform;
		_playerController = target.GetComponent<CharacterController2D>();
	}

    void Update()
    {
        // keep the camera centered between the two characters if we have two characters present
        if (target != null && target2 != null)
        {
            distanceX = target.transform.position.x - target2.transform.position.x;
            distanceY = target.transform.position.y - target2.transform.position.y;

            newPosX = distanceX / 2;
            newPosY = distanceY / 2;

            Vector3 newTargetVector = new Vector3(target.transform.position.x - newPosX, target.transform.position.y - newPosY, 0);

            newTargetPos = newTargetVector;

            Vector3 newTestVector = new Vector3(Mathf.Abs(distanceX), Mathf.Abs(distanceY), 0);

            //Debug.Log("Distance between players is: " + Vector3.Distance(target.transform.position, target2.transform.position));

            if (Vector3.Distance(target.transform.position, target2.transform.position) < 6.0f)
            {
                GameObject.Find("Main Camera").camera.orthographicSize = 5;
            }
            else if (Vector3.Distance(target.transform.position, target2.transform.position) > 14.0f)
            {
                GameObject.Find("Main Camera").camera.orthographicSize = 12;
            }
            else
            {
                GameObject.Find("Main Camera").camera.orthographicSize = 5 * (Mathf.Abs(Vector3.Distance(target.transform.position, target2.transform.position)) / 6.0f);
            }
        }
    }

	void LateUpdate()
	{
		if( !useFixedUpdate )
			updateCameraPosition();
	}


	void FixedUpdate()
	{
		if( useFixedUpdate )
			updateCameraPosition();
	}


	void updateCameraPosition()
	{
        if (target != null && target2 != null)
        {
            if (_playerController == null)
            {
                transform.position = Vector3.SmoothDamp(transform.position, newTargetPos - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
                return;
            }

            if (_playerController.velocity.x > 0)
            {
                transform.position = Vector3.SmoothDamp(transform.position, newTargetPos - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
            }
            else
            {
                var leftOffset = cameraOffset;
                leftOffset.x *= -1;
                transform.position = Vector3.SmoothDamp(transform.position, newTargetPos - leftOffset, ref _smoothDampVelocity, smoothDampTime);
            }
        }
        else
        {
            if (_playerController == null)
            {
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, 0) - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
                return;
            }

            if (_playerController.velocity.x > 0)
            {
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, 0) - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
            }
            else
            {
                var leftOffset = cameraOffset;
                leftOffset.x *= -1;
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, 0) - leftOffset, ref _smoothDampVelocity, smoothDampTime);
            }
        }
	}
	
}
