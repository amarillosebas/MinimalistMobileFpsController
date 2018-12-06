using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {
	public CharacterController Controller;
	public Camera CharacterCamera;
	public float Gravity = 1f;
	public float MoveSpeed = 6;
	private Vector3 TheVector;
	public FPSPlayerInput fpsPlayerInput;

	private float acceleration;
	[Range(0f, 0.1f)]
	public float accelerationValue;
	private float accelerationFactor;

	private bool _canCalculateAcceleration = true;
	private bool _canCalculateDeceleration = false;

	public bool moving = false;

	void Start () {
		Controller = GetComponent<CharacterController> ();
	}
	
	void FixedUpdate () {
		if (moving) {
			if (_canCalculateAcceleration) {
				accelerationFactor += Time.fixedDeltaTime + accelerationValue;
				if (SineAccelerator.Accelerate(out acceleration, accelerationFactor)) {
					_canCalculateAcceleration = false;
					_canCalculateDeceleration = true;
				}
			} else accelerationFactor = 0f;

			Vector3 tV;
			tV = CharacterCamera.transform.forward;
			tV.y = 0f;
			tV = tV.normalized;
			tV = tV * MoveSpeed * Time.fixedDeltaTime;

			tV *= acceleration;

			TheVector = tV;
		} else {
			if (_canCalculateDeceleration) {
				accelerationFactor += Time.fixedDeltaTime;
				if (SineAccelerator.Decelerate(out acceleration, accelerationFactor)) {
					_canCalculateAcceleration = true;
					_canCalculateDeceleration = false;
				}
			} else accelerationFactor = 0f;

			TheVector = TheVector * acceleration;
		}

		if (!Controller.isGrounded) {
			TheVector.y = -Gravity * Time.fixedDeltaTime;
		}

		Controller.Move(TheVector);
	}

	public void Trigger_Down () {
		if (!fpsPlayerInput.playerReticle.gazing) {
			moving = true;
			
			_canCalculateAcceleration = true;
			_canCalculateDeceleration = false;
			accelerationFactor = 0f;
		}
	}
	public void Trigger_Up () {
		moving = false;

		_canCalculateAcceleration = false;
		_canCalculateDeceleration = true;
		accelerationFactor = 0f;
	}
}
