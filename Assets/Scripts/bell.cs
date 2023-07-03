using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bell : MonoBehaviour
{
  public AudioSource RingAudio;
	public GameObject image;
  private bool enter;
  public GameObject text;

  void Update()
  {
    if (enter) 
    {
      image.SetActive(true);
      text.SetActive(true);
    }
    else 
    {
      image.SetActive(false);
      text.SetActive(false);
    }
    if (Input.GetKeyDown (KeyCode.F) && enter) 
		  RingAudio.Play();      
  }
  
  void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") 
			enter = true;
	}

  void OnTriggerExit(Collider col)
  {
	  if (col.tag == "Player") 
		  enter = false;
  }
}
