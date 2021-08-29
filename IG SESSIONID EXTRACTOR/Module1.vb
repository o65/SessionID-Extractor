Imports System.Net, System.Console, System.IO, System.Threading, System.Security.Cryptography, System.Text
Module Module1
'Programmed by @30_x
    Public Accs, Proxies As List(Of String)
    Public Greq, Breq, NumOrSecure, Wrong As Integer
    Public Mode, Time, Uuid As String
    Sub Main()
starting:
        Clear() : SetWindowSize(80, 17) : Title = "IG Session Extracter"
        Try
            Accs = File.ReadAllLines("accounts.txt").ToList
        Catch ex As Exception
            MsgBox("[!] Please Add accounts.txt", MsgBoxStyle.Critical) : CompilerServices.ProjectData.EndApp()
        End Try
        Time = DateAndTime.Now.ToString("dd/MM/yyyy")
        Uuid = Guid.NewGuid.ToString
        ForegroundColor = ConsoleColor.White
        WriteLine("  
    __ ___  __  __ _  __  __  _   _____   _______ ___  __   ________ __  ___  
  /' _| __/' _/' _| |/__\|  \| | | __\ \_/ |_   _| _ \/  \ / _|_   _/__\| _ \ 
  `._`| _|`._``._`| | \/ | | ' | | _| > , <  | | | v | /\ | \__ | || \/ | v / 
  |___|___|___|___|_|\__/|_|\__| |___/_/ \_\ |_| |_|_|_||_|\__/ |_| \__/|_|_\ 

")
        ForegroundColor = ConsoleColor.Green
        Dim text As String = "[!] This Program Was Coded By LT. | IG : @30_x"
        For Each _char_ As Char In text
            Write(_char_)
            Thread.Sleep(30)
        Next
        Thread.Sleep(100)
        WriteLine()
        ForegroundColor = ConsoleColor.Green : Write("[") : ForegroundColor = ConsoleColor.White : Write("1") : ForegroundColor = ConsoleColor.Green : Write("] ") : ForegroundColor = ConsoleColor.White : WriteLine($"With Proxies")
        ForegroundColor = ConsoleColor.Green : Write("[") : ForegroundColor = ConsoleColor.White : Write("2") : ForegroundColor = ConsoleColor.Green : Write("] ") : ForegroundColor = ConsoleColor.White : WriteLine($"With OutProxies")
        ForegroundColor = ConsoleColor.Green : Write("[") : ForegroundColor = ConsoleColor.White : Write("+") : ForegroundColor = ConsoleColor.Green : Write("] ") : ForegroundColor = ConsoleColor.Green : Write("-") : ForegroundColor = ConsoleColor.White : Write($" Mode : ") : ForegroundColor = ConsoleColor.Green : Mode = ReadLine()
        If Mode = "1" Then
            Try
                Proxies = File.ReadAllLines("proxies.txt").ToList
            Catch ex As Exception
                MsgBox("Please Add ( proxies.txt )", MsgBoxStyle.Critical) : GoTo starting
            End Try
            ForegroundColor = ConsoleColor.Green : Write("[") : ForegroundColor = ConsoleColor.White : Write("+") : ForegroundColor = ConsoleColor.Green : Write("] ") : ForegroundColor = ConsoleColor.Green : Write("-") : ForegroundColor = ConsoleColor.White : Write($" Accounts : ") : ForegroundColor = ConsoleColor.Green : Write(Accs.Count) : ForegroundColor = ConsoleColor.White : Write($" | Proxies : ") : ForegroundColor = ConsoleColor.Green : WriteLine(Proxies.Count)
        Else
            ForegroundColor = ConsoleColor.Green : Write("[") : ForegroundColor = ConsoleColor.White : Write("+") : ForegroundColor = ConsoleColor.Green : Write("] ") : ForegroundColor = ConsoleColor.Green : Write("-") : ForegroundColor = ConsoleColor.White : Write($" Accounts : ") : ForegroundColor = ConsoleColor.Green : WriteLine(Accs.Count)
        End If
        ForegroundColor = ConsoleColor.Green : Write("[") : ForegroundColor = ConsoleColor.White : Write("+") : ForegroundColor = ConsoleColor.Green : Write("] ") : ForegroundColor = ConsoleColor.Green : Write("-") : ForegroundColor = ConsoleColor.White : Write($" Press Enter To") : ForegroundColor = ConsoleColor.Green : Write(" Start") : ReadLine()
        WriteLine()
        Dim c As New Thread(New ParameterizedThreadStart(AddressOf Counter)) With {.Priority = ThreadPriority.Lowest, .IsBackground = False} : c.Start()
        Dim t As New Thread(New ParameterizedThreadStart(AddressOf Login)) With {.Priority = ThreadPriority.Highest, .IsBackground = False} : t.Start()
        ReadLine()
    End Sub
    Function Ran(MaxSize As Integer) As String
        Try
            Dim LetNum As Char() = New Char(61) {}
            LetNum = "aqwszxcderfvbgtyhnmjuiklop1234567895".ToCharArray()
            Dim Bytes As Byte() = New Byte(MaxSize - 1 + 1 - 1) {}
            Dim RJ_1 As RNGCryptoServiceProvider = New RNGCryptoServiceProvider()
            RJ_1.GetNonZeroBytes(Bytes)
            Dim RL As StringBuilder = New StringBuilder(MaxSize)
            For nm As Integer = 0 To Bytes.Length - 1
                RL.Append(LetNum(CInt(Bytes(nm)) Mod LetNum.Length))
            Next
            Return RL.ToString
        Catch Ex As Exception
        End Try
        Return False
    End Function
    Sub Counter()
        While True
            Try
                Title = $"GOOD : {Greq} | BAD : {Breq} | Num/Sus : {NumOrSecure} | Wrong_Credentials : {Wrong}"
                Thread.Sleep(100)
            Catch ex As Exception
            End Try
        End While
    End Sub
    Sub Login()
        Dim proxy As String
        For Each acc In Accs
            Dim U As String = acc.Split(":")(0).Replace(" ", "").Replace(Environment.NewLine, Nothing)
            Dim P As String = acc.Split(":")(1)
            If Mode = "1" Then proxy = Proxies(New Random().Next(0, Proxies.Count - 1))
            Try
                Dim Bytes As Byte() = New System.Text.UTF8Encoding().GetBytes($"uuid={UUid}&password={P}&username={U}&device_id={UUid}&from_reg=false&_csrftoken=missing&login_attempt_count=-1")
                Dim Request As System.Net.HttpWebRequest = DirectCast(System.Net.WebRequest.Create($"https://i.instagram.com/api/v1/accounts/login/"), System.Net.HttpWebRequest)
                With Request
                    .Method = "POST"
                    .UserAgent = "Instagram 25.0.0 Android"
                    .Headers.Add("Cookie", "missing")
                    .Headers.Add("Accept-Language", "en-US")
                    .ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
                    .Headers.Add("X-IG-Connection-Type", "WIFI")
                    .Accept = "*\*"
                    .Headers.Add("X-IG-Capabilities", "3brTvw==")
                    .ContentLength = Bytes.Length
                    If Mode = "1" Then
                        .Proxy = New WebProxy(proxy)
                    Else
                        .Proxy = Nothing
                    End If
                    .Timeout = 10000
                End With
                Dim Stream As System.IO.Stream = Request.GetRequestStream() : Stream.Write(Bytes, 0, Bytes.Length) : Stream.Dispose() : Stream.Close()
                Dim HttpResponse As System.Net.HttpWebResponse
                Try
                    HttpResponse = DirectCast(Request.GetResponse, System.Net.HttpWebResponse)
                Catch ex As System.Net.WebException
                    HttpResponse = DirectCast(ex.Response, System.Net.HttpWebResponse)
                End Try
                Dim StreamReader As System.IO.StreamReader = New System.IO.StreamReader(HttpResponse.GetResponseStream())
                Dim Result As String = StreamReader.ReadToEnd().ToString : StreamReader.Dispose() : StreamReader.Close() : HttpResponse.Close()
                If Result.Contains("logged_in_user") Then
                    Greq += 1
                    Dim Set_Cookie As String = HttpResponse.GetResponseHeader("Set-Cookie")
                    Dim sessid = System.Text.RegularExpressions.Regex.Match(Set_Cookie, "sessionid=(.*?);").Groups(1).Value ': MsgBox(sessid)
                    My.Computer.FileSystem.WriteAllText($"sessions {Time}.txt", sessid & vbCrLf, True)
                    Console.ForegroundColor = ConsoleColor.Green : Write("[") : Console.ForegroundColor = ConsoleColor.White : Write("+") : Console.ForegroundColor = ConsoleColor.Green : Write("] ") : Console.ForegroundColor = ConsoleColor.Green : Write($"Done : ") : Console.ForegroundColor = ConsoleColor.White : WriteLine($"@{U}")
                ElseIf Result.Contains("block") Or Result.Contains("limit") Or Result.Contains("spam") Or Result.Contains("wait") Then
                    Breq += 1
                    Console.ForegroundColor = ConsoleColor.Red : Write("[") : Console.ForegroundColor = ConsoleColor.White : Write("-") : Console.ForegroundColor = ConsoleColor.Red : Write("] ") : Console.ForegroundColor = ConsoleColor.Red : Write($"Spam Error : ") : Console.ForegroundColor = ConsoleColor.White : WriteLine($"@{U}")
                    Uuid = Guid.NewGuid.ToString
                    Thread.Sleep(10000)
                ElseIf Result.Contains("challenge") Or Result.Contains("checkpoint") Then
                    NumOrSecure += 1
                    Console.ForegroundColor = ConsoleColor.Red : Write("[") : Console.ForegroundColor = ConsoleColor.White : Write("-") : Console.ForegroundColor = ConsoleColor.Red : Write("] ") : Console.ForegroundColor = ConsoleColor.Red : Write($"SecureOrNumLocked Error : ") : Console.ForegroundColor = ConsoleColor.White : WriteLine($"@{U}")
                ElseIf Result.Contains("bad") Or Result.Contains("pass") Or Result.Contains("appear") Then
                    Wrong += 1
                    My.Computer.FileSystem.WriteAllText("Wrong_Credentials.Txt", vbCrLf & $"{U}:{P}", True)
                    Console.ForegroundColor = ConsoleColor.Red : Write("[") : Console.ForegroundColor = ConsoleColor.White : Write("-") : Console.ForegroundColor = ConsoleColor.Red : Write("] ") : Console.ForegroundColor = ConsoleColor.Red : Write($"Wrong_Credentials Error : ") : Console.ForegroundColor = ConsoleColor.White : WriteLine($"@{U}")
                Else
                    Breq += 1
                    Console.ForegroundColor = ConsoleColor.Red : Write("[") : Console.ForegroundColor = ConsoleColor.White : Write("-") : Console.ForegroundColor = ConsoleColor.Red : Write("] ") : Console.ForegroundColor = ConsoleColor.Red : Write($"Unkown Error : ") : Console.ForegroundColor = ConsoleColor.White : Write($"@{U}") : Console.ForegroundColor = ConsoleColor.Red : WriteLine(" - " & Result)
                End If
            Catch ex As Exception
                Breq += 1
                Console.ForegroundColor = ConsoleColor.Red : Write("[") : Console.ForegroundColor = ConsoleColor.White : Write("-") : Console.ForegroundColor = ConsoleColor.Red : Write("] ") : Console.ForegroundColor = ConsoleColor.Red : Write($"Unkown Error : ") : Console.ForegroundColor = ConsoleColor.White : Write($"@{U}") : Console.ForegroundColor = ConsoleColor.Red : WriteLine(" - " & ex.Message)
                If Mode = "1" Then
                    Try
                        Proxies.Remove(proxy)
                    Catch ex2 As Exception
                    End Try
                End If
            End Try
        Next
        Console.ForegroundColor = ConsoleColor.Green : WriteLine($"{Environment.NewLine}[+] Finished !")
    End Sub
End Module
