using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour {
	public Transform horizontalTransform;
	public Transform verticalTransform;
	public float MaxVerticalAngle = 80;
	public float MinVerticalAngle = -80;
	
	private Vector2 _lookVector;
	private bool _rotateCamera = false;
	public float rotationSpeed;
	public float rotationDamping;
	private float _desiredRotationX;
	private float _desiredRotationY;

	void OnEnable () {
		_desiredRotationY = horizontalTransform.eulerAngles.y;
		_desiredRotationX = verticalTransform.eulerAngles.x;
	}

	void Update () {
		if (_rotateCamera) {
			_desiredRotationY += rotationSpeed * _lookVector.x * Time.deltaTime;
			_desiredRotationX += rotationSpeed * -_lookVector.y * Time.deltaTime;

			_desiredRotationX = Mathf.Clamp(_desiredRotationX, MinVerticalAngle, MaxVerticalAngle);
		}

		Quaternion rotY = Quaternion.Euler(horizontalTransform.eulerAngles.x, _desiredRotationY, horizontalTransform.eulerAngles.z);
		Quaternion rotX = Quaternion.Euler(_desiredRotationX, horizontalTransform.eulerAngles.y, horizontalTransform.eulerAngles.z);

		rotY = Quaternion.Lerp(horizontalTransform.rotation, rotY, Time.deltaTime * rotationDamping);
		rotX = Quaternion.Lerp(verticalTransform.rotation, rotX, Time.deltaTime * rotationDamping);

		horizontalTransform.rotation = rotY;
		verticalTransform.rotation = rotX;
	}

	public void GetLookVector (Vector2 v) {
		_lookVector = v;
		_rotateCamera = true;

	}
	public void StopRotation () {
		_rotateCamera = false;
	}
}
