using System;
using Photon.Pun;
using UnityEngine;

namespace NewScripts
{
    public class PlayerTrigger : MonoBehaviourPunCallbacks,IPunObservable
    {
        public int Health { get; private set; }
        private Player _player;

        private void Awake()
        {
            Health = 1;
        }

        public void SetPlayer(Player player)
        {
            _player = player;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            //Checking,that this is our object,and object intersect with enemy object
            if (!photonView.IsMine || other.GetComponent<PhotonView>().IsMine)
                return;
            //This must be only sword
            if (!other.gameObject.CompareTag("Sword")) 
                return;
            
            Health -= 1;
            
            if (Health <= 0)
            {
                _player.Kill();
            }
            else
            {
                _player.React();
            }
        }
        
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(Health);
            
            }
            else
            {
                Health = (int) stream.ReceiveNext();
            }
        }
    }
}
