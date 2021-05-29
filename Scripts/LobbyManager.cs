using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NewScripts
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            PhotonNetwork.NickName = "Player" + Random.Range(0, 10000);
            PhotonNetwork.GameVersion = "0.0.1";


            PhotonNetwork.ConnectUsingSettings();
        
        }

        public void JoinGame()
        {
            if (!PhotonNetwork.IsConnected)
                return;
        
            if (PhotonNetwork.CountOfRooms == 0)
            {
                Debug.Log("Room is not created yet");
                return;
            }
        
            JoinRandomRoom();
        }
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master");
        }
        public void CreateRoom()
        {
            if (PhotonNetwork.IsConnectedAndReady)
                PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = 2});
        }
        private static void JoinRandomRoom()
        {
            PhotonNetwork.JoinRandomRoom();
        }
        public override void OnJoinedRoom()
        { 
            Debug.Log("Joined random room");
            PhotonNetwork.LoadLevel("Game");
        }
    }
}
