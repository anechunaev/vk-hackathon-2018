using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager2 : MonoBehaviour
{

    public TextAsset trackSource;
    public Material[] trackMaterials;
    public GameObject trackBasePrefab;
    public GameObject keyPrefab;
    public float tracksSpread = 1.5f;
    public float speed = 160f;
    public float trackLength = 10f;
    public float tempo = 1f;
    public GameObject hitArea;
    public int numOfTracks = 4;
    public AudioSource audioTrack;
    public float keySpeed = 1f;
    public float timeShift = 0f;

    private readonly GameObject[] tracks = new GameObject[4];
    private string[] noteTimeCodes = new string[92];
    private string[] noteTypes = new string[92];
    private int lastIndex = 0;
    private float lastTime, deltaTime, startTime;

    void Start()
    {
        string[] textTracks = trackSource.ToString().Split('\n');

        for (int i = 0; i < numOfTracks; i++)
        {
            tracks[i] = Instantiate(trackBasePrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
            tracks[i].transform.localPosition = new Vector3(tracksSpread * i, 0, 0);
            TrackBase baseTrack = tracks[i].GetComponent<TrackBase>();
            baseTrack.mesh.GetComponent<MeshRenderer>().material = trackMaterials[i];
            baseTrack.length = trackLength;

        }

        for (int i = 0; i < textTracks.Length; i++) {
            string[] s = textTracks[i].Split(' ');
            noteTimeCodes[i] = s[0];
            noteTypes[i] = s[1];
        }

        hitArea.transform.localScale = new Vector3(tracksSpread * textTracks.Length, 1, 1);
        hitArea.transform.localPosition = new Vector3((textTracks.Length - 1) * tracksSpread / 2, 0, 0);

        lastTime = 0f;
        startTime = Time.fixedTime;
        audioTrack.Play();
    }

    private void FixedUpdate()
    {
        if (lastIndex >= noteTimeCodes.Length) return;
        deltaTime = audioTrack.time - lastTime;
        if (Time.fixedTime - startTime + deltaTime >= float.Parse(noteTimeCodes[lastIndex]) - timeShift) {
            this.AddKey(noteTypes[lastIndex]);
            lastIndex++;
        }

        lastTime = audioTrack.time;
    }

    void AddKey(string type)
    {
        string[] types = type.Split(',');

        for (int i = 0; i < types.Length; i++) {
            GameObject currentKey = Instantiate(
                keyPrefab,
                new Vector3(tracks[int.Parse(types[i])].transform.position.x, trackLength, tracks[i].transform.position.z),
                Quaternion.identity,
                transform
            );
            currentKey.GetComponent<KeyBase>().type = int.Parse(types[i]);
            currentKey.GetComponent<KeyBase>().speed = keySpeed;
        }
    }
}
