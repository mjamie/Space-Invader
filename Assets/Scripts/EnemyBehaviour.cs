using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public float health = 150;
	public GameObject laser;
	public float laserspeed;
	public float firingRate = 2f;
	public float shotsPerSecond = 0.5f;

	void OnTriggerEnter2D(Collider2D collider){
		Proectile missile = collider.gameObject.GetComponent<Proectile> ();

		if(missile){
			health -= missile.GetDamage ();
			missile.Hit ();
			if(health <= 0){
				Destroy (gameObject);
			}
		}
	}

	void Update(){
		float probabality = Time.deltaTime * shotsPerSecond;
		if(Random.value < probabality){
			Fire ();
		}
	}

	void Fire(){
		GameObject beam = Instantiate (laser,transform.position,Quaternion.identity) as GameObject;
		beam.transform.position =  new Vector3(transform.position.x,transform.position.y-0.8f+0);
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0,-laserspeed,0);

	}
}
