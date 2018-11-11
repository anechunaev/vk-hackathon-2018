using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustAnotherTrackManager : MonoBehaviour {
    public TextAsset trackSource;
    public GameObject[] trackBasePrefab = new GameObject[3];
    public GameObject[] keyPrefab = new GameObject[3];
    public float speed = 64f;
    public GameObject[] hitArea = new GameObject[3];
    public int numOfTracks = 3;
    public AudioSource audioTrack;
    public float keySpeed = 1f;
    public float timeShift = 0f;

    private string[] noteTimeCodes = new string[165];
    private string[] noteTypes = new string[165];
    private int lastIndex = 0;
    private float lastTime, deltaTime, startTime;

    void Start()
    {
        string[] textTracks = trackSource.ToString().Split('\n');

        for (int i = 0; i < textTracks.Length; i++)
        {
            string[] s = textTracks[i].Split(' ');
            noteTimeCodes[i] = s[0];
            noteTypes[i] = s[1];
        }

        lastTime = 0f;
        startTime = Time.fixedTime;
        audioTrack.Play();
    }

    private void FixedUpdate()
    {
        if (lastIndex >= noteTimeCodes.Length) return;
        deltaTime = audioTrack.time - lastTime;
        if (Time.fixedTime - startTime + deltaTime >= float.Parse(noteTimeCodes[lastIndex]) - timeShift)
        {
            this.AddKey(noteTypes[lastIndex]);
            lastIndex++;
        }

        lastTime = audioTrack.time;
    }

    void AddKey(string type)
    {
        string[] types = type.Split(',');

        for (int i = 0; i < types.Length; i++)
        {
            int arrayKey = int.Parse(types[i]);
            GameObject currentKey = Instantiate(
                keyPrefab[arrayKey],
                transform
            );
            currentKey.transform.localPosition = new Vector3(trackBasePrefab[arrayKey].transform.localPosition.x, 14.05f, trackBasePrefab[arrayKey].transform.localPosition.z);
            currentKey.transform.localRotation = trackBasePrefab[arrayKey].transform.localRotation;
            currentKey.GetComponent<AnotherKey>().type = arrayKey;
            currentKey.GetComponent<AnotherKey>().speed = keySpeed;
        }
    }
}
