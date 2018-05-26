using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour {

	public Text autorName;
	public bool goLeft = false;
	public Vector2 canvasPosition;

	private Vector3 position;
	private Vector3 startPosition;
	private Vector3 finalPosition;
	private Vector3 startScale;

	private int direction = -1;
	private float status = 0.0f;

	// Use this for initialization
	void Start () {
		position = new Vector3(canvasPosition.x * Screen.width/10, canvasPosition.y * Screen.height/10, 1);

		if (!goLeft) {
			startPosition = position;
			finalPosition = position + new Vector3 (Screen.width * 7 / 10, 0, 0);
		} else {
			startPosition = position;
			finalPosition = position - new Vector3 (Screen.width * 7 / 10, 0, 0);
		}
		startScale = autorName.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		status += direction * Time.deltaTime / 5;
		status = Mathf.Clamp01(status);

		transform.position = Vector3.Lerp (startPosition, finalPosition, status);

        autorName.transform.localScale = new Vector3(Mathf.Lerp (0, startScale.x, status),startScale.y,1);
	}

	public void buttonClicked()
	{
		direction *= -1;
	}
}