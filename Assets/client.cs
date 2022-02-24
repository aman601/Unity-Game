using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System;

namespace client
{
    public class client : MonoBehaviour
    {
        private static readonly Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private const int PORT = 25565;
        //int attempts = 0;

        public GameObject otherCar;
        public GameObject Caruwu;
        Boolean booly = true;
        public static string pos;
        static string mainRotation;
        static string mainVel;
        static string totalCarData;
        public static string[] posi2 = new string[10];
        public static string[] oldPosi = new string[10];
        public static string[] seedReceive = new string[1];
        public static string[] seed = new string[1];
        public static string[] parts = new string[2];
        public static bool ss = false;
        public GameObject rg;
        GameObject carcar2;
        Rigidbody mainRB;
        Rigidbody otherRB;

        //https://blog.stephencleary.com/2009/04/message-framing.html

        void Start()
        {
            Debug.Log("Client start function reached");
            //Console.Title = "Client";
            //try {
            //    ConnectToServer();
            //} catch (Exception e) {
            //    Debug.Log(e);
            //    Debug.Log("Client no worky worky");
            //}
            //RequestLoop();
            //Exit();

            carcar2 = GameObject.Instantiate(otherCar, new Vector3(0, 0, (float)15.9), Quaternion.identity);
            otherRB = carcar2.GetComponent<Rigidbody>();
            mainRB = Caruwu.GetComponent<Rigidbody>();
            try
            {
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Close();
            }
            catch (SocketException)
            {
                Debug.Log("No socket to close");
            }



        }

        //private static void ConnectToServer()
        void Update()
        {
            Debug.Log((String.IsNullOrEmpty(seed[0])));
            Debug.Log(ss);
            if (!(String.IsNullOrEmpty(seed[0])) && !ss)
            {
                try
                {
                    Debug.Log("Setting seed as " + seed[0]);
                    UnityEngine.Random.InitState(int.Parse(seed[0]));
                    Debug.Log("Seed is set");
                    ss = true;
                }
                catch (Exception e)
                {

                }

            }

            if (ss)
            {
                rg.GetComponent<RandomGen>().seedSetTrue();
            }
            //Debug.Log("Started Connect Attempts");

            if (Connect(Caruwu, booly))
            {
                //Debug.Log(Caruwu);
                booly = false;
            }


            //Debug.Log("car" + carcar2);
            //Debug.Log("position" + posi2);

            pos = Caruwu.transform.position.ToString("F3");
            mainRotation = Caruwu.transform.rotation.ToString("F3");
            mainVel = mainRB.velocity.ToString("F3");
            totalCarData = pos + "," + mainRotation + "," + mainVel;

            try
            {
                //foreach (string s in posi2) {
                //    Debug.Log(s);
                //}
                if ((posi2[0] != oldPosi[0]) || (posi2[1] != oldPosi[1]) || (posi2[2] != oldPosi[2]))
                {
                    otherRB.constraints = RigidbodyConstraints.None;
                    carcar2.transform.position = new Vector3(float.Parse(posi2[0]), float.Parse(posi2[1]), float.Parse(posi2[2]));
                    oldPosi[0] = posi2[0];
                    oldPosi[1] = posi2[1];
                    oldPosi[2] = posi2[2];
                    //Debug.Log("Old pos: " + oldPosi[0] + "," + oldPosi[1] + ","+ oldPosi[2]);
                    //Debug.Log("New pos: " + posi[0] + "," + posi[1] + ","+ posi[2]);
                }
                else
                {
                    otherRB.constraints = RigidbodyConstraints.FreezePositionY;
                }

            }
            catch (Exception e)
            {
            }

        }

        void FixedUpdate()
        {
            try
            {
                if ((posi2[3] != oldPosi[3]) || (posi2[4] != oldPosi[4]) || (posi2[5] != oldPosi[5]) || (posi2[6] != oldPosi[6]))
                {
                    carcar2.transform.rotation = new Quaternion(float.Parse(posi2[3]), float.Parse(posi2[4]), float.Parse(posi2[5]), float.Parse(posi2[6]));
                    oldPosi[3] = posi2[3];
                    oldPosi[4] = posi2[4];
                    oldPosi[5] = posi2[5];
                    oldPosi[6] = posi2[6];
                }
                else
                {
                    //Debug.Log("No rot difference");
                }
                if ((posi2[7] != oldPosi[7]) || (posi2[8] != oldPosi[8]) || (posi2[9] != oldPosi[9]))
                {
                    otherRB.velocity = new Vector3(float.Parse(posi2[7]), float.Parse(posi2[8]), float.Parse(posi2[9]));
                    oldPosi[7] = posi2[7];
                    oldPosi[8] = posi2[8];
                    oldPosi[9] = posi2[9];
                }
                else
                {
                    //Debug.Log("No vel difference");
                }
            }
            catch (Exception e)
            {

            }
        }
        private static Boolean Connect(GameObject car, Boolean boofy)
        {

            try
            {
                //attempts++;
                //Debug.Log("Connection attempt " + attempts);
                // Change IPAddress.Loopback to a remote IP to connect to a remote host.
                // IPAddress.Parse("10.10.25.221")
                //Debug.Log("Connection Attempt");
                ClientSocket.Connect(IPAddress.Parse(ClientMenu.ipaddr), PORT);
            }
            catch (SocketException)
            {
                //Debug.Log("No Connection");
            }
            if (ClientSocket.Connected && boofy)
            {
                //Debug.Log(boofy);

                Debug.Log("Connected! Entering SendLoop");
                //Debug.Log(car);

                var t = Task.Run(() => SendLoop(car));
                return boofy;
            }
            else
            {
                return false;
            }

        }

        private static void SendLoop(GameObject mainCar)
        {
            //Debug.Log("In SendLoop");
            while (true)
            {
                //Debug.Log("In while loop");
                try
                {
                    Debug.Log(mainCar);
                }
                catch (Exception E)
                {
                    //Debug.Log(E);
                }

                //Debug.Log("Own car pos sending: " + mainCar.transform.position.ToString());
                string request = totalCarData;
                byte[] bufferr = Encoding.ASCII.GetBytes(request);
                ClientSocket.Send(bufferr, 0, bufferr.Length, SocketFlags.None);



                //if (request.ToLower() == "e")
                //{
                //    Exit();
                //}
                ReceiveResponse();
                //Debug.Log("Past Receive Response");
                /*var buffer = new byte[2048];
                int received = ClientSocket.Receive(buffer, SocketFlags.None);
                if (received == 0) return;
                var data = new byte[received];
                Array.Copy(buffer, data, received);
                string text = Encoding.ASCII.GetString(data);
                Debug.Log(text);*/
            }

        }
        /*

        private static void RequestLoop()
        {
            //Console.WriteLine(@"<Type ""exit"" to properly disconnect client>");

            while (true)
            {
                SendRequest();
                ReceiveResponse();
            }
        }

        /// <summary>
        /// Close socket and exit program.
        /// </summary>
        private static void Exit()
        {
            SendString("exit"); // Tell the server we are exiting
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            Environment.Exit(0);
        }

        private static void SendRequest()
        {
            Console.Write("Send a request: ");
            string request = Console.ReadLine();
            SendString(request);

            if (request.ToLower() == "exit")
            {
                Exit();
            }
        }

        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        private static void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }
*/
        private static void ReceiveResponse()
        {
            //Debug.Log("receiving");
            var buffer = new byte[2048];
            int received = ClientSocket.Receive(buffer, SocketFlags.None);
            if (received == 0) return;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);


            //put like _ in between things and then split like this into dataCollection
            //and then select data and parse from there
            //Debug.Log("Removing (){}");
            text = text.Replace("(", "");
            text = text.Replace(")", "");
            text = text.Replace("{", "");
            text = text.Replace("}", "");
            //posi2 = text.Substring (1, text.Length-2).Split (',');
            //removed parenthesis so start at start and end at end
            parts = text.Substring(0, text.Length).Split('|');
            posi2 = parts[1].Split(',');
            seedReceive = parts[2].Split(',');
            try
            {
                if (!(String.IsNullOrEmpty(seedReceive[0])))
                {
                    seed[0] = seedReceive[0];
                }
                Debug.Log("parts: " + parts[1] + "///////" + parts[2]);
                Debug.Log("Seed: " + seed[0]);
                //Debug.Log("posi2: " + posi2[0]);
                //for (int i=0; i < posi2.Length; i++) {
                //    Debug.Log(posi2[i]);
                //}
                //Debug.Log("seed: " + seed[0]);

            }
            catch (Exception e)
            {
                //Debug.Log("Logging failed");
            }

            //Console.WriteLine(text);
            //Debug.Log("Received: " + text);
        }

    }
}