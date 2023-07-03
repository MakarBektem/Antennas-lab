using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radar : MonoBehaviour
{
    public GameObject image;
	private bool enter;
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enter){
      image.SetActive(true);
      text.SetActive(true);
    }
    else 
    {
      image.SetActive(false);
      text.SetActive(false);
    }   
    }
    void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			enter = true;
			}
		}

    void OnTriggerExit(Collider col)
{
	if (col.tag == "Player") {
		enter = false;
	}
}
}
