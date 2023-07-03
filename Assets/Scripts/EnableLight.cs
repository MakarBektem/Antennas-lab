
using UnityEngine;

public class EnableLight : MonoBehaviour
{
  public Light _mainLight;
  public GameObject image;
	private bool enter;
  public GameObject text;

  private void Update() 
  {
    if(enter)
    {
      image.SetActive(true);
      text.SetActive(true);
    }
    else 
    {
      image.SetActive(false);
      text.SetActive(false);
    }
    if(Input.GetKeyUp(KeyCode.F)&& enter)
    _mainLight.enabled = !_mainLight.enabled;
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
