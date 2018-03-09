using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeStateBasedOnPlayerDist : MonoBehaviour {

	public Transform Player;
	public Transform initialState;
	public Transform finalState;
	private BoxCollider bxCol;
	private MeshRenderer mRend;
	public float maxDist;
	public float minDist;
	public float distanceToPlayer;
	public bool finalActive;
	public bool initialActive;

	void Start() {
		bxCol = transform.GetComponent<BoxCollider> ();
		mRend = transform.GetComponent<MeshRenderer> ();
		setInitialState ();
	}

	// Update is called once per frame
	void Update () {
		distanceToPlayer = Vector3.Distance (finalState.position,Player.position);
		checkPlayerDist ();
	}

	void checkPlayerDist(){
		if (distanceToPlayer <= maxDist) {
			if (distanceToPlayer >= minDist) {
				mRend.enabled = (true);
				bxCol.enabled = (true);
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

		reSize(multiplier);
		rePosition (multiplier);
		//reRotation (multiplier);
	}

	void reSize(float multiplier) {
		Vector3 scaleDiff = finalState.localScale - initialState.localScale;
		Vector3 scaleAdjust = scaleDiff * multiplier;
		transform.localScale = initialState.localScale + scaleAdjust;
	}

	void rePosition(float multiplier) {
		Vector3 posDiff = finalState.position - initialState.position;
		Vector3 positionAdjust = posDiff * multiplier;
		transform.position = initialState.position + positionAdjust;
	}
	/* Rotation does not work this way, may need some work
	void reRotation(float multiplier) {
		Vector3 rotDiff = finalState.rotation - initialState.rotation;
		Vector3 rotationAdjust = rotDiff * multiplier;
		transform.rotation = Quaternion.Euler(initialState.rotation + rotationAdjust);
	}
*/
	void setFinalState() {
		mRend.enabled = (finalActive);
		bxCol.enabled = (finalActive);
		transform.localScale = finalState.localScale;
		transform.localRotation = finalState.localRotation;
		//transform.position = finalState.position;
	}
	void setInitialState() {
		mRend.enabled = (initialActive);
		bxCol.enabled = (initialActive);
		transform.localScale = initialState.localScale;
		transform.position = initialState.position;
		//transform.localRotation = initialState.localRotation;
	}
}
