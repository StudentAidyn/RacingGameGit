using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnpoint;
    public int selectedCharacter = 0;
    public GameObject CLONE;

    public CameraFollow cameraFl;
    [SerializeField] Camera cam; //serializefield is another way to see a variable in the editor without making it public
    //https://www.youtube.com/watch?v=Y7pp2gzCzUI&ab_channel=DaniKrossing

    // Start is called before the first frame update
    void Awake()
    {
        cameraFl = cam.GetComponent<CameraFollow>();
    }

    void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        GameObject prefab = characterPrefabs[selectedCharacter];
        CLONE = Instantiate(prefab, spawnpoint.position, Quaternion.identity);
        cameraFl.target = CLONE.transform; //assign Clone's Transform to the camera's target transform

        StartCoroutine(WaitToStart());
    }

    private IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(2);

        if (cam.GetComponent<CameraFollow>())
        {
            cam.GetComponent<CameraFollow>().enabled = true; //CameraFollow Script needs to be set initially to disabled otherwise the script will crash.

        }
    }

}
