using System.Collections;
using UnityEngine;
public class CursorController : MonoBehaviour
{
    CursorControls controls;
    public Texture2D cursorOriginal;
    public Texture2D cursorHover;
    Camera mainCamera;
    [SerializeField] Etana etana;
    public bool hideHead = false;
    public bool animationPlaying;
    Ray ray;
    RaycastHit2D hits2D;
    bool clickingCounter = false; // to prevent animation freezing
    public bool gameover;
    [SerializeField] AudioClip whish;
    private void Awake()
    {
        controls = new CursorControls();
        ChangeCursor(cursorOriginal);
        Cursor.lockState = CursorLockMode.Confined;
        mainCamera = Camera.main;
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    private void Start()
    {
        controls.Mouse.Click.started += _ => StartedClick();
        controls.Mouse.Click.performed += _ => EndedClick();
    }
    private void Update()
    {
        if(etana != null)
        {
            EnterInput();
            DetectObject();
        }
    }
    void StartedClick()
    {
    }
    void EndedClick()
    {
        DetectObject();
    }
    public void DetectObject()
    {
        ray = mainCamera.ScreenPointToRay(controls.Mouse.Position.ReadValue<Vector2>());
        hits2D = Physics2D.GetRayIntersection(ray);
        if(hits2D.collider != null && !gameover)
        {
            if (hits2D.collider.gameObject.CompareTag("Player"))
            {
                if (Input.GetMouseButtonDown(0) && hideHead == false && !clickingCounter)
                    HideHead();
                else if (Input.GetMouseButtonDown(0) && hideHead && !clickingCounter)
                    StopHiding();
                else
                    animationPlaying = false;
            }
        }
    }
    void EnterInput()
    {
        if (Input.GetKeyDown(KeyCode.Return) && hideHead == false && !clickingCounter)
            HideHead();
        else if (Input.GetKeyDown(KeyCode.Return) && hideHead && !clickingCounter)
            StopHiding();
        else
            animationPlaying = false;
    }
    void HideHead()
    {
        etana.GetComponent<Animator>().SetTrigger("HideHead");
        SoundManager.Instance.PlaySound(whish);
        hideHead = true;
        animationPlaying = true;
        StartCoroutine(ClickCounter());
    }
    void StopHiding()
    {
        etana.GetComponent<Animator>().SetTrigger("StopHiding");
        hideHead = false;
        animationPlaying = true;
        StartCoroutine(ClickCounter());
    }
    IEnumerator ClickCounter()
    {
        clickingCounter = true;
        yield return new WaitForSeconds(0.5f);
        clickingCounter = false;
    }

    public void ChangeCursor(Texture2D cursorType)
    {
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }
}