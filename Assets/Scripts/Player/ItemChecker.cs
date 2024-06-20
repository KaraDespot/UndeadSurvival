using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChecker : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D other)
    {
        //if item tagged as "Meat", the health +=10
        if (other.gameObject.tag == "Meat")
        {
            this.gameObject.GetComponent<PlayerVisual>().Meat();
            // Delete triggered item
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Hotwings")
        {
            this.gameObject.GetComponent<PlayerVisual>().Hotwings();
            // Delete triggered item
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Noodles")
        {
            this.gameObject.GetComponent<PlayerVisual>().Noodles();
            // Delete triggered item
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Pizza")
        {
            this.gameObject.GetComponent<PlayerVisual>().Pizza();
            // Delete triggered item
            Destroy(other.gameObject);
        }
    }
}
