using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    public float movespeed;

    public float raySize;

    public LayerMask objectLayer;

    public float maxDistance;

    public Transform groundChecker;

    public enum direction { up, down, left, right, };

    public direction playerDirection = direction.up;

    bool rightRotation = false;

    float startTimer = 0;

    bool isRotating = false;

    float diffAngle;


    public float rotationSpeed;

    Vector3 previousAngle, targetAngle;

    private void Update() {

        RaycastHit wallHit;
        Physics.Raycast(transform.position, transform.forward, out wallHit, maxDistance, objectLayer);


        RaycastHit groundHit;
        Physics.Raycast(groundChecker.position, Vector3.down, out groundHit, maxDistance, objectLayer);
        
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        if (!isRotating) { 

            if (playerDirection != direction.down && verticalAxis < -0.5f) {

                targetAngle = new Vector3(0, 180, 0);
                playerDirection = direction.down;
                SetRotation();
                return;
            }
            else if (playerDirection != direction.up && verticalAxis > 0.5f) {
                targetAngle = new Vector3(0, 0, 0);
                playerDirection = direction.up;
                SetRotation();
                return;
            }

            else if (playerDirection != direction.right && horizontalAxis < -0.5f) {
                targetAngle = new Vector3(0, -90, 0);
                playerDirection = direction.right;
                SetRotation();
                return;
            }
            else if (playerDirection != direction.left && horizontalAxis > 0.5f) {
                targetAngle = new Vector3(0, 90, 0);
                playerDirection = direction.left;
                SetRotation();
                return;
            }



        }
        else {

            float distCovered = (Time.time - startTimer) * rotationSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / diffAngle;

            transform.localRotation = Quaternion.Lerp(Quaternion.Euler(previousAngle), Quaternion.Euler(targetAngle), fractionOfJourney);
            if (fractionOfJourney >= 1) {
                isRotating = false;
            }
            return;

        }

        if (wallHit.collider == null && groundHit.collider != null) {
            if (playerDirection == direction.up || playerDirection == direction.down) {

                transform.position += new Vector3(0, 0, verticalAxis * movespeed * Time.deltaTime);
            }
            else if (playerDirection == direction.right || playerDirection == direction.left) {
                transform.position += new Vector3(horizontalAxis * movespeed * Time.deltaTime, 0, 0);
            }
        }
    }


    void SetRotation() {
        startTimer = Time.time;
        
        previousAngle = transform.localEulerAngles;
        isRotating = true;
        diffAngle = Quaternion.Angle(Quaternion.Euler(previousAngle), Quaternion.Euler(targetAngle));

    }


    private void OnDrawGizmos() {
        Debug.DrawRay(transform.position, transform.forward * raySize, Color.red);
        Debug.DrawRay(groundChecker.position, Vector3.down * raySize, Color.blue);

    }
}
