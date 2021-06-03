using Photon.Pun;
using UnityEngine;

namespace NewScripts
{
    public class SwordController : MonoBehaviourPunCallbacks,IPunObservable
    {
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(GetComponent<Collider>().enabled);
            }
            else
            { 
                GetComponent<Collider>().enabled = (bool)stream.ReceiveNext();
                
               
            }
        }
        
    }
}
