using MyScripts.Interfaces;
using UnityEngine;

namespace MyScripts.Player
{
    /// <summary>
    /// Класс отвечающий за передвижение игрока, назначение клавиш управления, анимация игрока, вызов паузы, управление курсором
    /// </summary>
    public class PlayerController : MonoBehaviour
    {

        public enum PlayerState
        {
            Idle, Walk
        }
        public enum Hands
        {
            Left, Right
        }

        private static PlayerController instance;
        public static PlayerController Instance
        {
            get { return instance; }
            private set { instance = value; }
        }

        [SerializeField] private bool isPause;
        public bool IsPause
        {
            get { return isPause; }
            private set
            {
                isPause = value;
                if (isPause)
                {
                    if (detector) detector.ToDisable();
                    Time.timeScale = 0;
                    IsFixedLook = true;
                }
                else
                {
                    if (detector) detector.ToEnable();
                    Time.timeScale = 1;
                    IsFixedLook = false;
                }
            }
        }

        [SerializeField] private bool isFixedLook;
        public bool IsFixedLook
        {
            get { return isFixedLook; }
            private set
            {
                isFixedLook = value;
            }
        }
        private Rigidbody rb;

        public GameObject cameraTarget;

        public float movementIntensity;
        public float mouseSensitivity;


        [SerializeField] private GameObject pauseMenu;

        [Header("Keys")]
        [SerializeField] private KeyCode keyCodeWalkForward = KeyCode.W;
        [SerializeField] private KeyCode keyCodeWalkBack = KeyCode.S;
        [SerializeField] private KeyCode keyCodeWalkRight = KeyCode.D;
        [SerializeField] private KeyCode keyCodeWalkLeft = KeyCode.A;
        [SerializeField] private KeyCode keyCodePauseMenu = KeyCode.Escape;
        [SerializeField] private KeyCode keyCodeInteract = KeyCode.Mouse0;

        [Header("Animations")]
        [SerializeField] private PlayerState playerState;
        [SerializeField] private Animator animator;

        [Header("Interact")]
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;
        [SerializeField] private ObjectDetector detector;

        [Header("Cursor")]
        public Texture2D cursorTexture;
        public CursorMode cursorMode = CursorMode.Auto;
        public Vector2 hotSpot = Vector2.zero;

        private void Start()
        {
            Instance = this;

            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

            rb = GetComponent<Rigidbody>();

            SetMouseVisable(false);

            playerState = PlayerState.Idle;
        }

        private void OnApplicationFocus(bool focus)
        {
            SetMouseVisable(false);
        }

        private void Update()
        {
            playerState = PlayerState.Idle;

            if (!isFixedLook)
            {
                transform.localRotation = Quaternion.AngleAxis(transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity, Vector3.up);
                Camera.main.transform.localRotation = Quaternion.AngleAxis(Camera.main.transform.localEulerAngles.x + (-Input.GetAxis("Mouse Y")) * mouseSensitivity, Vector3.right);
            }

            if (Input.GetKeyDown(keyCodePauseMenu)) PauseMenu();
            if (Input.GetKeyDown(keyCodeInteract)) InteractWithObject();

        }

        private void FixedUpdate()
        {
            if (Input.GetKey(keyCodeWalkForward)) WalkForward();
            if (Input.GetKey(keyCodeWalkBack)) WalkBack();
            if (Input.GetKey(keyCodeWalkRight)) WalkRight();
            if (Input.GetKey(keyCodeWalkLeft)) WalkLeft();

            AnimationUpdate();
        }




        public void ToGrab(GameObject obj, Hands typeHand)
        {
            switch (typeHand)
            {
                case Hands.Left:
                    obj.transform.SetParent(leftHand);
                    break;
                case Hands.Right:
                    obj.transform.SetParent(rightHand);
                    break;
                default:
                    break;
            }
        }

        public void SetMouseVisable(bool value)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = value;
        }

        public void SetFixedLook(bool value)
        {
            if (value) IsFixedLook = true;
            else IsFixedLook = false;
        }
        public void SetPause(bool value)
        {
            if (value) IsPause = true;
            else IsPause = false;
        }



        private void PauseMenu()
        {

            if (pauseMenu.activeSelf)
            {
                IsPause = false;
                pauseMenu.SetActive(false);
                SetMouseVisable(false);

            }
            else
            {
                IsPause = true;
                pauseMenu.SetActive(true);
                SetMouseVisable(true);
            }

        }


        private void AnimationUpdate()
        {

            switch (playerState)
            {
                case PlayerState.Idle:
                    animator.SetBool("IsWalk", false);
                    break;
                case PlayerState.Walk:
                    animator.SetBool("IsWalk", true);
                    break;
                default:
                    break;
            }
        }

        public void InteractWithObject()
        {
            if (IsPause) return;
            RaycastHit hit;
            Vector3 direct = cameraTarget.transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(cameraTarget.transform.position, direct, out hit, 5f))
            {

                IInteractable interactObject = hit.collider.GetComponent<IInteractable>();

                if (interactObject == null) return;

                interactObject.Interact();
            }
        }

        private void WalkForward()
        {
            ToWalk();
            rb.AddForce(transform.forward * movementIntensity);
        }
        private void WalkBack()
        {
            ToWalk();
            rb.AddForce(-(transform.forward) * movementIntensity);
        }
        private void WalkRight()
        {
            ToWalk();
            rb.AddForce(transform.right * movementIntensity);
        }
        private void WalkLeft()
        {
            ToWalk();
            rb.AddForce(-(transform.right) * movementIntensity);
        }
        private void ToWalk()
        {
            playerState = PlayerState.Walk;
        }
    }
}