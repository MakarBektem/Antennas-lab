using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour 
{
	public float smooth = 2.0f;
	public float DoorOpenAngle = 90.0f;

	/*public AudioClip OpenAudio;
	public AudioClip CloseAudio;
	private bool AudioS;*/

	private Vector3 defaultRot;
	private Vector3 openRot;
	private bool open;
	private bool enter;
	public GameObject image;
	public GameObject text;

	void Start () 
	{
		defaultRot = transform.eulerAngles;
		openRot = new Vector3 (defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
	}
	
	void Update () 
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
		if (open) 
			transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, openRot, Time.deltaTime * smooth);
		 else 
			transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, defaultRot, Time.deltaTime * smooth);

		if (Input.GetKeyDown (KeyCode.F) && enter) 
			open = !open;
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
