using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleSkin : MonoBehaviour {
	public Animator skinAnimator;

	public void Gaze_In () {
		skinAnimator.SetBool("look", true);
	}

	public void Gaze_Out () {
		skinAnimator.SetBool("look", false);
	}
}
