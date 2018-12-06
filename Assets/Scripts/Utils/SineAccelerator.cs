using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SineAccelerator {

	public static bool Accelerate (out float a, float t) {
		bool r = false;

		a = Mathf.Sin(t);
		if (a >= 0.9f) {
			r = true;
			a = 1f;
		}

		return r;
	}

	public static bool Decelerate (out float a, float t) {
		bool r = false;

		a = 1f - Mathf.Sin(t);
		if (a <= 0.1f) {
			r = true;
			a = 0f;
		}

		return r;
	}
}
