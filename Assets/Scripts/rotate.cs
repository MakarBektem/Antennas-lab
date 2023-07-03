using System.Collections;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float smooth = 2.0f;
	public float RotateAngle = 70.0f;
	private Vector3 defaultRot;
	private Vector3 rotateRot;
	private bool open;
	private bool enter;
	public GameObject image;
	public GameObject text;

	// Use this for initialization
	void Start () {
		
			defaultRot = transform.eulerAngles;
			rotateRot = new Vector3 (defaultRot.x, defaultRot.y + RotateAngle, defaultRot.z);
		}
	
	// Update is called once per frame
	void Update () {
		if(enter){
      image.SetActive(true);
	  text.SetActive(true);
    }
    else 
    {
      image.SetActive(false);
	  text.SetActive(false);
    }
		if (open) 
		{	
			transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, rotateRot, Time.deltaTime * smooth);
		}
		 else 
		{	
			transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
		}

		if (Input.GetKeyDown (KeyCode.F) && enter) {
			open = !open;
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
