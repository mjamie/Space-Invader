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
	public float spawnDelay = 0.5f;

		void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;

		transform.position += Vector3.right * speed * Time.deltaTime;

		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3(1,0,distance));
		xmax = rightMost.x - padding;

		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3(0,0,distance));
		xmin = leftMost.x + padding;

		SpawnUntilFull ();
	}

	void SpawnEnemies () {
		foreach (Transform child in transform) { //the transform attached to this object.
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.SetParent (child);
		}
	}

	void SpawnUntilFull () {
		Transform freePosition = NextFreePosition ();
		if(freePosition){
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;	
		}
		if(NextFreePosition ()){
		Invoke ("SpawnUntilFull",spawnDelay);
		}
	}

	public void OnDrawGizmos () {//Drawing of gizmos.(Better refernce to see the object on the scene)
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height,0));
	}

	void Update () {

		//transform.position += Vector3.right * speed * Time.deltaTime;

		if(movingRight){ //if true
			transform.position += Vector3.right * speed * Time.deltaTime; //moving enemies right.

				if(transform.position.x>=xmax){
					movingRight = !movingRight;//changes to else.
				}
		}
		else{
		 transform.position += Vector3.left * speed * Time.deltaTime;//moves the enemy to the left.

			if(transform.position.x<=xmin){
				movingRight = !movingRight;
			}
		}

		float newX = Mathf.Clamp (transform.position.x, xmin,xmax); //keeps the enemies in the screen
		transform.position = new Vector3(newX,transform.position.y,transform.position.z);

		if(AllMembersDead()){
			SpawnUntilFull ();
		}
	}

	Transform NextFreePosition () {
		foreach (Transform childPositionGameObject in transform) {//tranfrom that is attached to this object.
			if (childPositionGameObject.childCount == 0) { //looks at the number of childern the transform has.
				return childPositionGameObject;
			}
		}
		return null;
	}

	bool AllMembersDead(){
		
		foreach (Transform childPositionGameObject in transform) {//tranform attached to this object.
			if (childPositionGameObject.childCount > 0) { //looks at the number of childern the transform has.
				print(childPositionGameObject.childCount);
				return false;
			}
		}

		return true;
	}
}
