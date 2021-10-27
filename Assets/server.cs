using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System;

namespace servers
{
    public class server : MonoBehaviour
    {
        private static readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> clientSockets = new List<Socket>();
        private const int BUFFER_SIZE = 2048;
        private const int PORT = 25565;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];
        public GameObject otherCar;
        static string mainPosition;
        static string mainRotation;
        static string totalCarData;
        public GameObject mainCar;
        public static string[] posi = new string[10];
        public static string[] oldPosi = new string[10];
        GameObject carcar;
        Rigidbody mainRB;
        Rigidbody otherRB;
        public GameObject rg;
        public static string mainVel;
        static string totalData;
        static string genData;
        public static bool sentSeed = false;
        void Start()
        {
            //CloseAllSockets();
                //Debug.Log("Client disconnected");
            mainRB = mainCar.GetComponent<Rigidbody>();
            rg.GetComponent<RandomGen>().setSeed();
            genData = rg.GetComponent<RandomGen>().getSeed().ToString();
            

            /*
            oldPosi[0] = "0";
            oldPosi[1] = "0";
            oldPosi[2] = "0";
            oldPosi[3] = "0";
            oldPosi[4] = "0";
            oldPosi[5] = "0";
            oldPosi[6] = "0";
            oldPosi[7] = "0";
            oldPosi[8] = "0";
            oldPosi[9] = "0";

            posi[0] = "0";
            posi[1] = "0";
            posi[2] = "0";
            posi[3] = "0";
            posi[4] = "0";
            posi[5] = "0";
            posi[6] = "0";
            posi[7] = "0";
            posi[8] = "0";
            posi[9] = "0";
            */


            Debug.Log("Main Running");
            carcar = GameObject.Instantiate(otherCar, new Vector3(0, 0, (float)13.9), Quaternion.identity);
            otherRB = carcar.GetComponent<Rigidbody>();

            //Console.Title = "Server";
            SetupServer();
            //Console.ReadLine(); // When we press enter close everything
            //CloseAllSockets();
        }

        /*void OnTriggerExit(Collider other) {
            if (other.tag == "ramp") {
                ramping = false;
            }
        }
        void OnTriggerEnter(Collider other) {
            if (other.tag == "ramp") {
                ramping = true;
            }
        }*/

        void Update() {
            //Debug.Log("Update running");
            //foreach (string s in posi) {
            //    Debug.Log(s);
            //}
            mainPosition = mainCar.transform.position.ToString("F3");
            mainRotation = mainCar.transform.rotation.ToString("F3");
            mainVel = mainRB.velocity.ToString("F3");
            
            totalCarData = mainPosition + "," + mainRotation + "," + mainVel;

            //Debug.Log("Running getSeed: " + rg.GetComponent<RandomGen>().getSeed());
            //was used to ensure packet sent, but tcp so should be fine
            if (sentSeed == true) {
                //rg.GetComponent<RandomGen>().setSeedFalse();
                //sentSeed = false;
                genData = "";
            }
            
            
            totalData = "{" + "|" + totalCarData + "|" + genData + "}";
            //Debug.Log("Total data: " + totalData);

            try {
                if ((posi[0] != oldPosi[0]) || (posi[1] != oldPosi[1]) || (posi[2] != oldPosi[2])) {
                    otherRB.constraints = RigidbodyConstraints.None;
                    carcar.transform.position = new Vector3(float.Parse(posi[0]), float.Parse(posi[1]), float.Parse(posi[2]));
                    oldPosi[0] = posi[0];
                    oldPosi[1] = posi[1];
                    oldPosi[2] = posi[2];
                    //Debug.Log("Old pos: " + oldPosi[0] + "," + oldPosi[1] + ","+ oldPosi[2]);
                    //Debug.Log("New pos: " + posi[0] + "," + posi[1] + ","+ posi[2]);
                } else {
                    //Debug.Log("No pos difference");
                    otherRB.constraints = RigidbodyConstraints.FreezePositionY;
                }
            } catch (Exception e) {

            }
            
        }

        void FixedUpdate() {
            try {

                /*if (Physics.Raycast(otherRB.transform.position, otherRB.transform.TransformDirection(Vector3.down), (float).2)
                && !(ramping)) {
                    grounded = true;
                    otherRB.transform.position = new Vector3(otherRB.transform.position.x, (float)-.85, otherRB.transform.position.z);
                    otherRB.constraints = RigidbodyConstraints.FreezePositionY;
                } else {
                    grounded = false;
                    otherRB.constraints = RigidbodyConstraints.None;
                }*/


                if ((posi[3] != oldPosi[3]) || (posi[4] != oldPosi[4]) || (posi[5] != oldPosi[5]) || (posi[6] != oldPosi[6])) {
                    carcar.transform.rotation = new Quaternion(float.Parse(posi[3]), float.Parse(posi[4]), float.Parse(posi[5]), float.Parse(posi[6]));
                    oldPosi[3] = posi[3];
                    oldPosi[4] = posi[4];
                    oldPosi[5] = posi[5];
                    oldPosi[6] = posi[6];
                } else {
                    //Debug.Log("No rot difference");
                }
                if ((posi[7] != oldPosi[7]) || (posi[8] != oldPosi[8]) || (posi[9] != oldPosi[9])) {
                    otherRB.velocity = new Vector3(float.Parse(posi[7]), float.Parse(posi[8]), float.Parse(posi[9]));
                    oldPosi[7] = posi[7];
                    oldPosi[8] = posi[8];
                    oldPosi[9] = posi[9];
                } else {
                    //Debug.Log("No vel difference");
                }

            } catch (Exception e) {

            }
        }

        private static void SetupServer()
        {
            Debug.Log("Setting up server...");
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            serverSocket.Listen(0);
            
            //begin accept here and at bottom of acceptcallback are replaced by an infinite loop
            serverSocket.BeginAccept(AcceptCallback, null);
            Debug.Log("Server setup complete");
        }


        /// <summary>
        /// Close all connected client (we do not need to shutdown the server socket as its connections
        /// are already closed with the clients).
        /// </summary>
        private static void CloseAllSockets()
        {
            foreach (Socket socket in clientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            Debug.Log("Server Sockets closed");
            serverSocket.Close();
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

           clientSockets.Add(socket);
            //make other car here
            //Debug.Log("Making Other Car");
            //GameObject.FindGameObjectWithTag("nonstatic").GetComponent<nonStaticNetcode>().log();
            //Debug.Log(GameObject.FindGameObjectWithTag("nonstatic").ToString());
            //GameObject.FindGameObjectWithTag("netcode").GetComponent<server>().log();

            //make send loop that wont progress until it receives data?
            //look into that, cant set stuff in receivecallback like I AM NOT
            //ABOVE IS IMPORTANT
            
           socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
           Debug.Log("Client connected, waiting for request...");
           serverSocket.BeginAccept(AcceptCallback, null);
        }
        public void log() {
            Debug.Log("log");
        }
        

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                Debug.Log("Client forcefully disconnected");
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current.Close(); 
                clientSockets.Remove(current);
                return;
            }

            //receive done to make sure message is complete?
            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            //Debug.Log("Received Main Pos: " + text);
            text = text.Replace("(", "");
            text = text.Replace(")", "");
            //posi = text.Substring (1, text.Length-2).Split (',');
            //removed parenthesis so start at start and end at end
            //Debug.Log("received");
            posi = text.Substring (0, text.Length).Split (',');

            /*if (text.ToLower() == "w") // Client requested time
            {
                //Debug.Log("Text is a w");
                byte[] data = Encoding.ASCII.GetBytes("w send");
                current.Send(data);
                //Debug.Log("Time sent to client");
            }*/
            /*else if (text.ToLower() == "e") // Client wants to exit gracefully
            {
                // Always Shutdown before closing
                current.Shutdown(SocketShutdown.Both);
                current.Close();
                clientSockets.Remove(current);
                Debug.Log("Client disconnected");
                return;
            }*/
            
            //Debug.Log(text.ToString());
            byte[] data = Encoding.ASCII.GetBytes(totalData);
            current.Send(data);
            sentSeed = true;
            //Debug.Log("Warning Sent");
            

            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }

        
            //Vector3fromString
//A useful function to convert back from a given Vector3.toString() output.  Passes back a Unity Vector3 object.
        /*public Vector3 stringToVec(string s) {
            string[] temp = s.Substring (1, s.Length-2).Split (',');
            return new Vector3 (float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]));
        }*/

        //TRY TO REMOVE COLLIDERS SOMETIME, DONT REALLY WANT THAT UNLESS WE MAKE THE TRACK LARGER, OR DO WE? ASK CALEB
        //MINIMAP, BACKGROUND THINGS AND A UI AND THAT SHOULD BE IT, SINGLE AND MULTIPLAYER?
    }

}
