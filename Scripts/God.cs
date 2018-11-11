// I'm really sorry

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class God : MonoBehaviour {
    public GameObject ScreenStartGame;
    public GameObject ScreenChooseComposer;
    public GameObject[] Tracks = new GameObject[3];
    public GameObject[] Keys = new GameObject[3];
    public GameObject PosterHighlight;
    public GameObject TrackManager;
    public GameObject Finale;

    public int currentState = 0; // 0 - choose composer, 1 - game, 2 - final
    private GameObject highlight;
    private GameObject screenStart;
    private GameObject trackManager;
    private GameObject finale;

	void Awake () {
        this.InitScreenChooseGame();
        var vuforia = VuforiaARController.Instance;
        vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        vuforia.RegisterOnPauseCallback(OnPaused);
    }

    private void OnVuforiaStarted()
    {
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    private void OnPaused(bool paused)
    {
        if (!paused)
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform.tag == "poster")
                {
                    screenStart.transform.localPosition = new Vector3(
                        hit.transform.localPosition.x - 0.01f,
                        6.26f,
                        hit.transform.localPosition.z
                    );
                }

                if (hit.transform.tag == "playButton")
                {
                    this.DestroyScreenChooseGame();
                    this.InitGame();
                }

                if (hit.transform.tag == "closeButton")
                {
                    this.DestroyGame();
                    this.InitFinal();
                }

                if (hit.transform.tag == "buyButton")
                {
                    Application.OpenURL("https://www.meloman.ru/calendar/?hall=koncertnyj-zal-chajkovskogo");
                }

                if (hit.transform.tag == "exitButton")
                {
                    this.DestroyFinal();
                    this.InitScreenChooseGame();
                }

                AnotherKey key = hit.transform.GetComponent<AnotherKey>();

                if (key != null) {
                    if (key.readyToScore)
                    {
                        Debug.Log("Good hit");
                        key.GoodHit();
                    }
                    else
                    {
                        Debug.Log("Bad hit");
                        key.BadHit();
                    }
                }
            }
        }
    }

    void InitScreenChooseGame()
    {
        highlight = Instantiate(PosterHighlight, transform, false);
        highlight.transform.localPosition = new Vector3(1.2f, 0, -7.5f);
        screenStart = Instantiate(ScreenStartGame, highlight.transform, false);
        screenStart.transform.localPosition = new Vector3(-999f, -999f, -999f);
    }

    void DestroyScreenChooseGame()
    {
        Destroy(highlight);
        Destroy(screenStart);
    }

    void InitGame()
    {
        trackManager = Instantiate(TrackManager, transform, false);
        trackManager.transform.localPosition = new Vector3(0, 1f, 0);
    }

    void DestroyGame()
    {
        Destroy(trackManager);
    }

    void InitFinal()
    {
        finale = Instantiate(Finale, transform, false);
        finale.transform.localPosition = new Vector3(-0.43f, 0, -9.13f);
    }

    void DestroyFinal()
    {
        Destroy(finale);
    }
}
