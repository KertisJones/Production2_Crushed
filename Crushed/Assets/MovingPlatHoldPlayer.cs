using UnityEngine;
using System.Collections;

public class MovingPlatHoldPlayer : MonoBehaviour {

    public bool isTouching = false;
  
    void OnTriggerEnter2D(Collider2D other)
    {
        //isTouching = true;
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
            isTouching = true;
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }

    }

}
