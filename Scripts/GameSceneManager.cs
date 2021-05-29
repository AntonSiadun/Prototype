using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NewScripts
{
    public class GameSceneManager : MonoBehaviourPunCallbacks
    {
        public GameObject prefabPlayer;
        public static GameSceneManager Instance;
        private void Awake()
        {
            Instance = this;

            if (prefabPlayer != null)
                PhotonNetwork.Instantiate(this.prefabPlayer.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
            base.OnLeftRoom();
        }
    }
}
