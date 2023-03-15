using ServerAdministratorSimulator.Interactable;
using ServerAdministratorSimulator.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace ServerAdministratorSimulator.Player
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
		[SerializeField] private GameObject objectInLeftHand;
		[SerializeField] private Transform rightHand;
		[SerializeField] private GameObject objectInRightHand;
		[SerializeField] private ObjectDetector detector;

		[Header("Cursor")]
		public Texture2D cursorTexture;
		public CursorMode cursorMode = CursorMode.Auto;
		public Vector2 hotSpot = Vector2.zero;

		private void Awake()
		{
			Instance = this;
		}
		private void Start()
		{

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
			
			
			if (objectInLeftHand) 
			{
				InteractOnHand interactOnHand = objectInLeftHand.GetComponent<InteractOnHand>();
				if (interactOnHand)
				{
					if (Input.GetKeyDown(interactOnHand.interactKey)) interactOnHand.Use();
					if (Input.GetKeyDown(KeyCode.Q)) ToRelease(Hands.Left);
				}
			}

			if (objectInRightHand)
			{
				InteractOnHand interactOnHand = objectInRightHand.GetComponent<InteractOnHand>();
				if (interactOnHand)
				{
					if (Input.GetKeyDown(interactOnHand.interactKey)) interactOnHand.Use();
					if (Input.GetKeyDown(KeyCode.Q)) ToRelease(Hands.Right);
				}
			}
		}

		private void FixedUpdate()
		{
			if (Input.GetKey(keyCodeWalkForward)) WalkForward();
			if (Input.GetKey(keyCodeWalkBack)) WalkBack();
			if (Input.GetKey(keyCodeWalkRight)) WalkRight();
			if (Input.GetKey(keyCodeWalkLeft)) WalkLeft();

			AnimationUpdate();
		}



		public bool IsObjectInHand(Hands typeHand) 
		{
			switch (typeHand)
			{
				case Hands.Left:
					if (objectInLeftHand == null) return false;
					else return true;
				case Hands.Right:
					if(objectInRightHand == null) return false;
					else return true;
			}
			return false;
		}
		public void ToGrab(GameObject obj, Hands typeHand)
		{
			switch (typeHand)
			{
				case Hands.Left:
					obj.transform.SetParent(leftHand);
					objectInLeftHand = obj;
					break;
				case Hands.Right:
					obj.transform.SetParent(rightHand);
					objectInLeftHand = obj;
					break;
				default:
					break;
			}
		}
		public void ToRelease(Hands typeHand)
		{

			switch (typeHand)
			{
				case Hands.Left:
					objectInLeftHand.GetComponent<Rigidbody>().isKinematic = false;
					objectInLeftHand.transform.SetParent(null);
					objectInLeftHand.transform.localScale = Vector3.one;
					objectInLeftHand = null;
					PlayerGUI.Instance.UpdateObjectInHandHelperText("");
					break;
				case Hands.Right:
					objectInLeftHand.GetComponent<Rigidbody>().isKinematic = false;
					objectInLeftHand.transform.SetParent(null);
					objectInLeftHand.transform.localScale = Vector3.one;
					objectInLeftHand = null;
					PlayerGUI.Instance.UpdateObjectInHandHelperText("");
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
			Vector3 direct = cameraTarget.transform.TransformDirection(Vector3.forward);
			if (Physics.Raycast(cameraTarget.transform.position, direct, out RaycastHit hit, 3f))
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