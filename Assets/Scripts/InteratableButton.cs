using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteratableButton : Interactable {
	public Material triggerDownMat;
	public Material triggerUpMat;

	public override void Trigger_Down () {
		base.Trigger_Down();
		GetComponent<Renderer>().material = triggerDownMat;
	}

	public override void Trigger_Up () {
		base.Trigger_Up();
		GetComponent<Renderer>().material = triggerUpMat;
	}

	public override void Trigger_Hold () {
		base.Trigger_Hold();
	}

	public override void Gaze_In () {
		base.Gaze_In();
		transform.localScale *= 1.1f;
	}

	public override void Gaze_Out () {
		base.Gaze_Out();
		transform.localScale *= 0.9f;
	}
}
