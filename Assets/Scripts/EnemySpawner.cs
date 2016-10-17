using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;

	float xmin;
	float xmax;

	public float padding = 1f;

	private bool movingRight = true;

	public float speed;
		void Start ()
	{
		float distance = transform.position.z - Camera.main.transform.position.z;

		transform.position += Vector3.right * speed * Time.deltaTime;

		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3(1,0,distance));
		xmax = rightMost.x - padding;

		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3(0,0,distance));
		xmin = leftMost.x + padding;

		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.SetParent (child);

		}
	}

	public void OnDrawGizmos ()
	{
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height,0));
	}

	void Update () {

		//transform.position += Vector3.right * speed * Time.deltaTime;

		if(movingRight){
			transform.position += Vector3.right * speed * Time.deltaTime;

				if(transform.position.x>=xmax){
					movingRight = !movingRight;
				}
		}
		else{
		 transform.position += Vector3.left * speed * Time.deltaTime;

			if(transform.position.x<=xmin){
				movingRight = !movingRight;
			}
		}
		print (transform.position+"    "+xmax);

		float newX = Mathf.Clamp (transform.position.x, xmin,xmax);
		transform.position = new Vector3(newX,transform.position.y,transform.position.z);
	}
}
