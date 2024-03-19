using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using FTPserver_form;

class FTPserver {

    public void start(string root_folder,string ipaddress) {
        IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 21);
        Socket ftpserver = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ftpserver.Bind(endpoint);
        Form1.WriteLog("running..");
        ftpserver.Listen(2);
        while (true) {
            Socket client = ftpserver.Accept();
            Form1.WriteLog("new client");
            SendMessage("200 ftp server connected\r\n", client);
            Task.Run(() => {
                HandleClient(client, root_folder,ipaddress);
            });
        }
    }

    void HandleClient(Socket command_socket, string root_folder, string ipaddress) {
        Socket dataTransfer_server_socket = null;
        Socket dataTransfer_socket = null;
        string root_path = root_folder;
        string pointer_path = root_folder;

        while (true) {
            string message = GetMessage(command_socket);

            string command = null;
            do {
                string[] splitted = message.Split(' ');
                if (splitted.Length == 1) {
                    command = message;
                }
                else {
                    command = splitted[0];
                }
            } while (false);

            if (command.ToUpper().Contains("USER")) {
                //allow all users
                SendMessage("230 accepted\r\n", command_socket);
            }
            else if (command.ToUpper().Contains("OPTS")) {
                //set options
                SendMessage("200 applied\r\n", command_socket);
            }
            else if (command.ToUpper().Contains("PWD")) {
                //print the current working directory
                SendMessage($"257 \"{pointer_path}\"\r\n", command_socket);
            }
            else if (command.ToUpper().Contains("CWD")) { //CWD /
                //change working directory
                string[] splitted = message.Split(' ');
                if (splitted[1].Contains("C:\\")) {
                    pointer_path = splitted[1];
                }
                else {
                    pointer_path = Path.Combine(pointer_path, splitted[1]);
                }
                pointer_path = pointer_path.Replace("\r\n", "");
                if (pointer_path[0] == '/') {
                    StringBuilder path_rebuilder = new StringBuilder();
                    for (int i = 1; i < pointer_path.Length; i++) {
                        if (pointer_path[i] == '/') {
                            path_rebuilder.Append("\\");
                        }
                        else {
                            path_rebuilder.Append(pointer_path[i]);
                        }
                    }
                    pointer_path = path_rebuilder.ToString();
                }
                Form1.WriteLog("pointerpath has changed:\r\n" + pointer_path);
                SendMessage("200 applied\r\n", command_socket);
            }
            else if (command.ToUpper().Contains("SYST")) { //change ftp format of os?
                //change working directory
                SendMessage("200 applied\r\n", command_socket);
            }
            else if (command.ToUpper().Contains("TYPE")) { //TYPE A
                //transfer charset?
                SendMessage("215 UNIX Type: L8\r\n", command_socket);
            }
            else if (command.ToUpper().Contains("PASV")) {
                //socket.Send(Encoding.ASCII.GetBytes(command));
                //Form1.WriteLog("sended: " + command);
                if (dataTransfer_server_socket != null) {
                    dataTransfer_server_socket.Dispose();
                }
                dataTransfer_server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                int port_num = 3328;
                int added = 0;
                while (true) {
                    try {
                        IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, port_num+added);
                        dataTransfer_server_socket.Bind(endpoint);
                        break;
                    }
                    catch (SocketException e) {
                        if (e.ToString().Contains("Only one usage")) {
                            added += 1;
                        }
                        else {
                            throw new Exception("unexpected error: " + e.ToString());
                        }
                    }
                }
                dataTransfer_server_socket.Listen();

                string[] splitted_ip = ipaddress.Split('.');
                if(splitted_ip.Length!= 4) {
                    throw new Exception($"unexpected ipaddress format: {ipaddress} (expected format: 0.0.0.0)");
                }

                SendMessage($"227 Entering Passive Mode ({splitted_ip[0]},{splitted_ip[1]},{splitted_ip[2]},{splitted_ip[3]},13,{added})\r\n", command_socket);

                if (dataTransfer_socket != null) {
                    dataTransfer_socket.Dispose();
                }
                dataTransfer_socket = dataTransfer_server_socket.Accept();
                Form1.WriteLog("client connected to data socket");
                SendMessage("226 Transfer complete.", command_socket);
            }
            else if (command.ToUpper().Contains("LIST")) {
                //request list of directory
                string[] files = Directory.GetFiles(pointer_path);
                string[] directories = Directory.GetDirectories(pointer_path);
                StringBuilder directoryListing = new StringBuilder();
                //directoryListing.AppendLine("Directory listing:");
                foreach (string directory in directories) {
                    DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                    directoryListing.AppendLine($"drwxrwxrwx 1 - - {0} {directoryInfo.LastWriteTime.ToString("MMM dd hh:mm", System.Globalization.CultureInfo.InvariantCulture)} {directoryInfo.Name}");
                }
                foreach (string file in files) {
                    FileInfo fileInfo = new FileInfo(file);

                    string file_type = "-";
                    //if ((fileInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory) {
                    //    file_type = "d";
                    //}
                    //else if ((fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) {
                    //    file_type = "r";
                    //}
                    //else if ((fileInfo.Attributes & FileAttributes.Archive) == FileAttributes.Archive) {
                    //    file_type = "a";
                    //}
                    directoryListing.AppendLine($"{file_type}rwxrwxrwx 1 - - {fileInfo.Length} {fileInfo.LastWriteTime.ToString("MMM dd hh:mm", System.Globalization.CultureInfo.InvariantCulture)} {fileInfo.Name}");
                }

                SendMessage($"150 Here comes the directory listing\r\n", command_socket);
                SendMessage($"{directoryListing.ToString()}\r\n", dataTransfer_socket);
                //Thread.Sleep(10);
                SendMessage("226 Directory send OK\r\n", command_socket);
                //socket.Close();
                break;
                //SendMessage("200", socket);
                //socket.Write(Encoding.ASCII.GetBytes(response), 0, response.Length);
            }
            else if (command.ToUpper().Contains("NOOP")) { //check server is alive
                SendMessage("200 I'm here\r\n", command_socket);
            }
            else if (command.ToUpper().Contains("SIZE")) {
                string[] splitted = message.Split(" ");
                StringBuilder filenameBuilder = new StringBuilder();
                for (int i = 1; i < splitted.Length; i++) {
                    if (i != 1) {
                        filenameBuilder.Append(" ");
                    }
                    filenameBuilder.Append(splitted[i]);
                }
                string filename = filenameBuilder.ToString().Replace("\r\n", "");
                string full_path = Path.Combine(pointer_path, filename);
                FileInfo info = new FileInfo(full_path);
                SendMessage($"213 {info.Length}\r\n", command_socket);
            }
            else if (command.ToUpper().Contains("RETR")) {
                SendMessage("150 opening..\r\n", command_socket);

                string[] splitted = message.Split(" ");
                StringBuilder filenameBuilder = new StringBuilder();
                for (int i = 1; i < splitted.Length; i++) {
                    if (i != 1) {
                        filenameBuilder.Append(" ");
                    }
                    filenameBuilder.Append(splitted[i]);
                }

                string filename = filenameBuilder.ToString().Replace("\r\n", "");
                string full_path = Path.Combine(pointer_path, filename);
                using (FileStream fileStream = File.OpenRead(full_path)) {
                    byte[] buffer = new byte[1024];
                    int bytesRead = -1;

                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0) {
                        dataTransfer_socket.Send(buffer, 0, bytesRead, SocketFlags.None);
                    }
                }
                //Thread.Sleep(10);
                SendMessage("226 done\r\n", command_socket);
                break;
            }
            else if (command.ToUpper().Contains("SITE")) {
                string param = message.Split(' ')[1].Replace("\r\n", "");
                if (param.ToUpper() == "HELP") {
                    string help = "SITE HELP\r\n" +
                        "214- Server-specific commands supported:\r\n" +
                        "214-   CUSTOMCMD <arguments> - Description of the custom command.\r\n" +
                        "214-   ANOTHERCMD <arguments> - Description of another custom command.\r\n" +
                        "214- Additional server features:\r\n" +
                        "214-   Feature 1 - Description of feature 1.\r\n" +
                        "214-   Feature 2 - Description of feature 2.\r\n" +
                        "214 End of help.\r\n";
                    SendMessage(help, command_socket);
                }
            }
        }

        command_socket.Dispose();
        dataTransfer_server_socket.Dispose();
        dataTransfer_socket.Dispose();
        Form1.WriteLog("closed..");
    }

    string GetMessage(Socket socket) {
        StringBuilder messagebuilder = new StringBuilder();
        byte[] buffer = new byte[1024];
        while (true) {
            int received = socket.Receive(buffer);
            if (received == 0) {
                Form1.WriteLog("break detected");
                break;
            }
            string mes = Encoding.ASCII.GetString(buffer, 0, received);
            messagebuilder.Append(mes);

            if (messagebuilder.ToString().Contains("\r\n")) {
                Form1.WriteLog("crlf detected");
                break;
            }
        }
        Form1.WriteLog(">> " + messagebuilder.ToString());


        return messagebuilder.ToString();
    }
    void SendMessage(string message, Socket socket) {
        socket.Send(Encoding.ASCII.GetBytes(message));
        Form1.WriteLog("sended: \r\n<< " + message);
    }
}