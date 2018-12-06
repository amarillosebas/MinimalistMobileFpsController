using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReticle : MonoBehaviour {
	[Header("Dependencies")]
	public Transform cameraTransform;
	public ReticleSkin reticle;
	public FPSPlayerInput fpsPlayerInput;
	public FPSController fpsController;

	[Space(5f)]
	[Header("Variables")]
	public float reticleDistance;
	public LayerMask interactableLayers;

	private bool lookingAtSomething = false;
	private bool previousFrameLooking = false;
	private bool triggerState = false;
	private bool triggerBuffer = true;

	private Interactable _tempGazeInteractable = null;
	private Interactable _tempTriggerInteractable = null;
	public bool gazing = false;
	
	void Update () {
		RaycastHit hit;
		Interactable interactable = null;

		if (!fpsController.moving) {
			if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, reticleDistance, interactableLayers)) {
				lookingAtSomething = true;
				interactable = hit.transform.GetComponent<Interactable>();
			} else {
				lookingAtSomething = false;
			}
		}


		if (lookingAtSomething != previousFrameLooking) {
			previousFrameLooking = lookingAtSomething;

			if (lookingAtSomething) {
				reticle.Gaze_In();
				if (interactable) {
					interactable.Gaze_In();
					_tempGazeInteractable = interactable;
					gazing = true;
				}
			} else {
				reticle.Gaze_Out();
				if (_tempGazeInteractable) {
					_tempGazeInteractable.Gaze_Out();
					_tempGazeInteractable = null;
					gazing = false;
				}
			}
		}

		if (fpsPlayerInput) {
			if (fpsPlayerInput.trigger) {
				if (interactable) interactable.Trigger_Hold();
			}
		}
	}

	public void Trigger_Down () {
		if (_tempGazeInteractable) {
			_tempTriggerInteractable = _tempGazeInteractable;
			_tempTriggerInteractable.Trigger_Down();
		}
	}
	public void Trigger_Up () {
		if (_tempTriggerInteractable) {
			_tempTriggerInteractable.Trigger_Up();
			_tempTriggerInteractable = null;
		}
	}
}
