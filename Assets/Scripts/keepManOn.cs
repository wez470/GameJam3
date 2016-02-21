using UnityEngine;
using System.Collections;

public class keepManOn : MonoBehaviour {
    public float yRotation = 1;
    
	
	// Update is called once per frame
	void Update () {
        
    }


    void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            // hit.transform.parent = this.transform;
            var emptyObject = new GameObject();
            emptyObject.transform.parent = this.transform;
            hit.transform.parent = emptyObject.transform;

        }
    }


      void OnCollisionExit2D(Collision2D hit)
      {
        if (hit.transform.tag == "Player")
        {
            hit.transform.parent = null;
            
        }
      }
}
