using UnityEngine;
using System.Collections;

public class FPSPlayerInput : MonoBehaviour {
	[Header("Dependencies")]
	public FPSCamera playerCamera;
	public FPSController playerController;
	public PlayerReticle playerReticle;

	[Space(5f)]
	[Header("Variables")]
	public bool trigger = false;

	Vector2 touchDirection;
	private Vector2 touchOrigin = -Vector2.one;
	
	void Start () {
		_directionTouch = int.MaxValue;
		_triggerTouch = int.MaxValue;
	}

	private int _directionTouch;
	private int _triggerTouch;

	private bool triggerBuffer = true;

	private void Update () {
		if (Input.touchCount > 0) {
			foreach (Touch touch in Input.touches) {

				switch (touch.phase) {
					case TouchPhase.Began:
						if (touch.position.x < Screen.width / 2) {
							_directionTouch = touch.fingerId;
							touchOrigin = touch.position;
						} else if (touch.position.x > Screen.width / 2) {
							_triggerTouch = touch.fingerId;
							trigger = true;
						}
					break;
					case TouchPhase.Moved:
						if (touch.fingerId == _directionTouch) {
							touchDirection = touch.position - touchOrigin;
							touchDirection = new Vector2(Mathf.Clamp(touchDirection.x, -300f, 300f) / 300f, Mathf.Clamp(touchDirection.y, -300f, 300f) / 300f);
							playerCamera.GetLookVector(touchDirection);
						}
					break;
					case TouchPhase.Ended:
						if (touch.fingerId == _directionTouch) {
							playerCamera.StopRotation();
							touchOrigin.x = -1;
							touchDirection = Vector2.zero;
							_directionTouch = int.MaxValue;
						} else if (touch.fingerId == _triggerTouch) {
							trigger = false;
							_triggerTouch = int.MaxValue;
						}
					break;
				}
			}
		}

		/*if (Input.GetKeyDown(KeyCode.V)) {
			trigger = true;
		}
		if (Input.GetKeyUp(KeyCode.V)) {
			trigger = false;
		}*/

		if (trigger != triggerBuffer) {
			triggerBuffer = trigger;

			if (trigger) {
				if (playerReticle) playerReticle.Trigger_Down();
				if (playerController) playerController.Trigger_Down();
			} else {
				if (playerReticle) playerReticle.Trigger_Up();
				if (playerController) playerController.Trigger_Up();
			}
		}
	}
}