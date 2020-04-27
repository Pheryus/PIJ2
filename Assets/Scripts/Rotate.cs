using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    private bool rotatingClockwise = false;

    public SpecialGround specialGround;

    private void Start() {
        specialGround.touchGround += ApplyRotation;
    }


    public void ApplyRotation() {
        rotatingClockwise = !rotatingClockwise;
    }
	
	// Update is called once per frame
	void Update () {
	    if (rotatingClockwise && (transform.eulerAngles.z > 270 || transform.eulerAngles.z <= 0))
	    {
            transform.Rotate(0, 0, -1);
	    }
	    else if (!rotatingClockwise && transform.eulerAngles.z > 0)
	    {
            transform.Rotate(0, 0, 1);
        
        }


	}
}
