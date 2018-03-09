using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeBasedOnDistance : MonoBehaviour {

	public Transform Player;
	public Transform initialState;
	public Transform finalState;
	public float maxDist;
	public float minDist;
	private float distanceToPlayer;
	public bool finalActive;
	public bool initialActive;

	// Update is called once per frame
	void Update () {
		distanceToPlayer = Vector3.Distance (finalState.position,Player.position);
	}

	void checkPlayerDist(){
		if (distanceToPlayer <= maxDist) {
			if (distanceToPlayer >= minDist) {
				transform.gameObject.SetActive (false);
				reState ();
			}
			else {
				setFinalState ();
			}
		}
		else {
			setInitialState ();
		}
	}

	void reState() {
		float progression = distanceToPlayer - minDist;
		float total = maxDist - minDist;
		float multiplier = (total - progression) / (total);

		Vector3 scaleDiff = finalState.localScale - initialState.localScale;
		Vector3 posDiff = finalState.position - initialState.position;
		Vector3 scaleAdjust = scaleDiff * multiplier;
		Vector2 positionAdjust = posDiff * multiplier;

		reSize(scaleAdjust);
		rePosition (positionAdjust);
	}

	void reSize(Vector3 scaleAdjust) {
		transform.localScale = initialState.localScale + scaleAdjust;
	}

	void rePosition(Vector3 positionAdjust) {
		transform.position = initialState.position + positionAdjust;
	}

	void setFinalState() {
		transform.gameObject.SetActive (finalActive);
		transform.localScale = finalState.localScale;
		transform.position = finalState.position;
	}
	void setInitialState() {
		transform.gameObject.SetActive (initialActive);	
		transform.localScale = initialState.localScale;
		transform.position = initialState.position;
	}
}
