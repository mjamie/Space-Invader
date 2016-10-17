using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float speed;
	float xmin;
	float xmax;

	public float padding = 1f;
	public float laserSpeed = 3f;
	public GameObject laser;
	public float firingRate = 2f;

	void Start () {
	float distance = transform.position.z - Camera.main.transform.position.z;
		laser.transform.SetParent (transform);
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3(1,0,distance));
		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding;
	}
	
	void Update () {

		float input = this.transform.position.x;

		if(Input.GetKey (KeyCode.RightArrow)){
			
			this.transform.position += new Vector3(speed*Time.deltaTime,0f,0f);
			//this.transform.position += Vector3.right * speed * Time.deltaTime ;
		}

		else if(Input.GetKey (KeyCode.LeftArrow)) {

			this.transform.position += new Vector3(-speed*Time.deltaTime,0f,0f);
			//this.transform.position += Vector3.left * speed * Time.deltaTime;
		}

		float newX = Mathf.Clamp (transform.position.x, xmin,xmax);
		transform.position = new Vector3(newX,transform.position.y,transform.position.z);

		if(Input.GetKeyDown (KeyCode.Space)){
			InvokeRepeating ("Fire",0.000001f,firingRate);
		}
		if(Input.GetKeyUp (KeyCode.Space)){
			CancelInvoke ("Fire");
		}
	}

	void Fire(){
		GameObject beam = Instantiate (laser,transform.position,Quaternion.identity) as GameObject;
			beam.transform.position =  new Vector3(transform.position.x,transform.position.y+0.6f+0);
			beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0,laserSpeed,0);

	}
}
