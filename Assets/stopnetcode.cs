using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System;

public class stopnetcode : MonoBehaviour
{
    // Start is called before the first frame update
    private static readonly Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    
    void Start()
    {
        ClientSocket.Shutdown(SocketShutdown.Both);
        ClientSocket.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
