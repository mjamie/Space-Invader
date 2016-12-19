using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float speed;
	float xmin;
	float xmax;
	float health = 200f;

	public float padding = 1f;
	public float laserSpeed = 3f;
	public GameObject laser;
	public float firingRate = 2f;

	public AudioClip shot;

	void Start () {
	float distance = transform.position.z - Camera.main.transform.position.z;
	//	laser.transform.SetParent (transform);
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

		float newX = Mathf.Clamp (transform.position.x, xmin,xmax); // keeps the player in screen
		transform.position = new Vector3(newX,transform.position.y,transform.position.z);

		if(Input.GetKeyDown (KeyCode.Space)){
			InvokeRepeating ("Fire",0.000001f,firingRate);
		}
		if(Input.GetKeyUp (KeyCode.Space)){
			CancelInvoke ("Fire");
		}
	}

	void Fire(){
		AudioSource.PlayClipAtPoint (shot,transform.position);
		GameObject beam = Instantiate (laser,transform.position,Quaternion.identity) as GameObject;
		beam.transform.position =  new Vector3(transform.position.x,transform.position.y+0f+0);
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0,laserSpeed,0);

	}

	void OnTriggerEnter2D(Collider2D collider){
		Proectile missile = collider.gameObject.GetComponent<Proectile> ();//method to use when object collides with this object.

		if (missile) {
			
				health -= missile.GetDamage ();
				missile.Hit ();
				if (health <= 0) {
				Die ();
				}

		}
	}

	void Die () {
		LevelManager man = Object.FindObjectOfType <LevelManager>();
		man.LoadLevel ("Win Screen");
		Destroy (gameObject);
	}

}
