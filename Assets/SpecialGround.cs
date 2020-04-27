using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGround : MonoBehaviour {

    public Action touchGround;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            touchGround?.Invoke();
        }
    }

}
