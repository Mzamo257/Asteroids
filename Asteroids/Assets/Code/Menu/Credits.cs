using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour {
    #region Public Attributes
    public Text autorName;
	public bool goLeft = false;
	public Vector2 canvasPosition;
    #endregion

    #region Private Attributes
    private Vector3 position;
	private Vector3 startPosition;
	private Vector3 finalPosition;
	private Vector3 startScale;

	private int direction = -1;
	private float status = 0.0f;
    #endregion

    #region MonoDevelop Methods
    // Use this for initialization
    void Start ()
    {
        //Put the button in the correct place
		position = new Vector3(canvasPosition.x * Screen.width/10, canvasPosition.y * Screen.height/10, 1);

        //Assign the direction
		if (!goLeft)
        {
			startPosition = position;
			finalPosition = position + new Vector3 (Screen.width * 7 / 10, 0, 0);
		}
        else
        {
			startPosition = position;
			finalPosition = position - new Vector3 (Screen.width * 7 / 10, 0, 0);
		}

        //Keep the initial scale
		startScale = autorName.transform.localScale;
	}
	
	// Update is called once per frame
	void Update ()
    {
		status += direction * Time.deltaTime / 5;
		status = Mathf.Clamp01(status);

        //Move the image
		transform.position = Vector3.Lerp (startPosition, finalPosition, status);

        //Interpolate the name to extend or compress
        autorName.transform.localScale = new Vector3(Mathf.Lerp (0, startScale.x, status), startScale.y, 1);
	}
    #endregion

    #region User Methods
    /// <summary>
    /// Change the direction of the image
    /// </summary>
    public void buttonClicked()
	{
		direction *= -1;
	}
    #endregion
}