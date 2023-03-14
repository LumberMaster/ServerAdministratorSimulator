using MyScripts.Player;
using UnityEngine;

namespace MyScripts.NPC {

    /// <summary>
    ///  Класс отвечающий за поведение NPC
    /// </summary>
    public class NPCLook : MonoBehaviour
    {
        private void FixedUpdate()
        {
            Vector3 tempDirection = (PlayerController.Instance.gameObject.transform.position - gameObject.transform.position).normalized;
            Quaternion tempLookRotation = Quaternion.LookRotation(new Vector3(tempDirection.x, 0.0f, tempDirection.z));

            transform.rotation = Quaternion.Lerp(transform.rotation, tempLookRotation, Time.deltaTime);
            //transform.LookAt(PlayerController.Instance.gameObject.transform.position);
        }
    }


}
