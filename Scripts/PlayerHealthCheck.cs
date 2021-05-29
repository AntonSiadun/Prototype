using Photon.Pun;
using UnityEngine;

namespace NewScripts
{
    public class PlayerHealthCheck : MonoBehaviourPunCallbacks,IPunObservable
    {
        private int Health { get; set; }
        private readonly Player _player;

        PlayerHealthCheck(Player owner)
        {
            _player = owner;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine || other.GetComponent<PhotonView>().IsMine)
                return;
            if (!other.gameObject.CompareTag("Sword")) 
                return;
            
            Health -= 1;

            if (Health <= 0)
            {
                _player.Die();
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
