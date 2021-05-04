using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class HandleJoin : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }


    void Update()
    {
 
    }

    private const int port = 3333;
    private const string server = "127.0.0.1";


    public static void TaskOnClick()
    {
        try {
            TcpClient client = new TcpClient();
            client.Connect(server, port);

            byte[] data = new byte[256];
            StringBuilder response = new StringBuilder();
            NetworkStream stream = client.GetStream();


            string respons1e = "01";
            byte[] data1 = System.Text.Encoding.UTF8.GetBytes(respons1e);
            stream.Write(data, 0, data1.Length);


            do {
                int bytes = stream.Read(data, 0, data.Length);
                response.Append(Encoding.UTF8.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable); // пока данные есть в потоке

            Debug.Log(response.ToString());

            // Закрываем потоки
            stream.Close();
            client.Close();
        }
        catch (SocketException e) {
            Debug.Log("SocketException: {0} " + e);
        }
        catch (Exception e) {
            Debug.Log("Exception: {0} "+ e.Message);
        }

        Debug.Log("Запрос завершен...");
    }

}
