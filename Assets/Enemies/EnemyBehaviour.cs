using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public AudioClip shot;
	public AudioClip destroyed;

	public float health = 150;
	public GameObject laser;
	public float laserspeed;
	public float firingRate = 2f;
	public float shotsPerSecond = 0.5f;

	public int scoreValue = 150;

	private ScoreKeeper scoreKeeper;
	private EnemySpawner enemySpawner;

	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();//returns score game object to be initilized.
		enemySpawner = GameObject.Find ("EnemyFormation").GetComponent<EnemySpawner>();
	}
	void OnTriggerEnter2D(Collider2D collider){
		Proectile missile = collider.gameObject.GetComponent<Proectile> (); //What component to use when component collides with this object.

		if(missile){
			health -= missile.GetDamage ();
			missile.Hit ();
			if(health <= 0){
				Die ();
			}
		}
	}

	void Die () {
		AudioSource.PlayClipAtPoint (destroyed,transform.position);
		Destroy (gameObject); //Destroys this game object. 
		scoreKeeper.Score(scoreValue);
	}

	void Update(){
		float probabality = Time.deltaTime * shotsPerSecond;
		if(Random.value < probabality && enemySpawner.enemiesOnScreen == enemySpawner.enemyNumbers){
			Fire ();

		}
	}

	void Fire(){
		AudioSource.PlayClipAtPoint (shot,transform.position);
		GameObject beam = Instantiate (laser,transform.position,Quaternion.identity) as GameObject;
		beam.transform.position =  new Vector3(transform.position.x,transform.position.y-0f+0); //the position of the beam
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0,-laserspeed,0); //velocity of the beam

	}
}
