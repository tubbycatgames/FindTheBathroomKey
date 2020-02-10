using UnityEngine;

public class Key : MonoBehaviour {

    private GameObject[] spawnPoints;
    private int rotationDegreesPerSecond = 45;

	private void Start() {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        transform.position = spawnPoint.transform.position;
    }

    public void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationDegreesPerSecond);
    }
}
