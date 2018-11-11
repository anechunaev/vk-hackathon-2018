using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour {

    public TextAsset trackSource;
    public Material[] trackMaterials;
    public GameObject trackBasePrefab;
    public GameObject keyPrefab;
    public float tracksSpread = 1.5f;
    public float speed = 140f;
    public float trackLength = 10f;
    public int startIndex = 8;
    public float tempo = 1f;
    public GameObject hitArea;
    public AudioClip audioTrack;

    private string[] textTracks;
    private int lastIndex = 0;
    private readonly GameObject[] tracks = new GameObject [4];

	void Start ()
    {
        textTracks = trackSource.ToString().Replace(" ", "").Split('\n');

        for (int i = 0; i < textTracks.Length; i++) 
        {
            tracks[i] = Instantiate(trackBasePrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
            tracks[i].transform.localPosition = new Vector3(tracksSpread * i, 0, 0);
            TrackBase baseTrack = tracks[i].GetComponent<TrackBase>();
            baseTrack.mesh.GetComponent<MeshRenderer>().material = trackMaterials[i];
            baseTrack.length = trackLength;

        }

        lastIndex = startIndex;

        InvokeRepeating("AddKey", 60f / speed, 60f / speed);

        hitArea.transform.localScale = new Vector3(tracksSpread * textTracks.Length, 1, 1);
        hitArea.transform.localPosition = new Vector3((textTracks.Length - 1) * tracksSpread / 2, 0, 0);
    }

    void Update()
    {
        if (lastIndex >= textTracks[0].Length)
        {
            CancelInvoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                KeyBase key = hit.transform.GetComponent<KeyBase>();

                if (key == null) return;

                if (key.readyToScore)
                {
                    key.GoodHit();
                }
                else
                {
                    key.BadHit();
                }
            }
        }
    }

    void AddKey()
    {
        for (int i = 0; i < textTracks.Length; i++)
        {
            if (textTracks[i][lastIndex] == 'x') {
                GameObject currentKey = Instantiate(
                    keyPrefab,
                    new Vector3(tracks[i].transform.position.x, trackLength, tracks[i].transform.position.z),
                    Quaternion.identity,
                    transform
                );
                currentKey.GetComponent<KeyBase>().type = i;
                currentKey.GetComponent<KeyBase>().speed = (60f / speed) * (trackLength / tempo);
            }
        }

        lastIndex++;
    }
}
