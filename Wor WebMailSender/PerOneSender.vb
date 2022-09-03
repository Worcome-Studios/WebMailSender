Public Class PerOneSender
    Dim DIRCommons As String = "C:\Users\" & Environment.UserName & "\WebEmailSender"

    Private Sub PerOneSender_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If My.Computer.FileSystem.DirectoryExists(DIRCommons) = False Then
                My.Computer.FileSystem.CreateDirectory(DIRCommons)
            End If
            LoadEmailList()
        Catch ex As Exception
            Console.WriteLine("[PerOneSender@PerOneSender_Load]Error: " & ex.Message)
        End Try
    End Sub

    Private Sub PerOneSender_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            If My.Computer.FileSystem.DirectoryExists(DIRCommons) = False Then
                My.Computer.FileSystem.CreateDirectory(DIRCommons)
            End If
            SaveEmailList()
        Catch ex As Exception
            Console.WriteLine("[PerOneSender@PerOneSender_FormClosing]Error: " & ex.Message)
        End Try
    End Sub

#Region "Controles"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim TextBoxVirtual = InputBox("Ingrese una direccion de correo", "Worcome Security")
        If TextBoxVirtual = Nothing Then
        Else
            ListBox1.Items.Add(TextBoxVirtual)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SaveEmailList()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button4.Text = "Sending"
        Button4.Enabled = False
        SenderPerOne()
    End Sub
#End Region

#Region "Subs"
    Sub LoadEmailList()
        Try
            'Carga los ficheros (Correos inscritos)
            For Each Correo As String In System.IO.File.ReadLines(DIRCommons & "\EmailList.ini")
                ListBox1.Items.Add(Correo)
            Next
        Catch ex As Exception
            Console.WriteLine("[PerOneSender@LoadEmailList]Error: " & ex.Message)
        End Try
    End Sub

    Sub SaveEmailList()
        If My.Computer.FileSystem.FileExists(DIRCommons & "\EmailList.ini") = True Then
            My.Computer.FileSystem.DeleteFile(DIRCommons & "\EmailList.ini")
        End If
        Try
            'Crea un fichero con todos los correos inscritos
            Dim StringPassed As String = Nothing
            For Each Correo As Object In ListBox1.Items
                StringPassed = StringPassed & Correo & vbCrLf
            Next
            My.Computer.FileSystem.WriteAllText(DIRCommons & "\EmailList.ini", StringPassed, False)
        Catch ex As Exception
            Console.WriteLine("[PerOneSender@SaveEmailList]Error: " & ex.Message)
        End Try
    End Sub

    Sub SenderPerOne()
        Try
            'Pasa cada correo y envia
            For Each Correo As Object In ListBox1.Items
                HTMLCorreo.SendEmail(Correo, False)
                Threading.Thread.Sleep(15)
            Next
            Button4.Text = "Start Sender"
            Button4.Enabled = True
            MsgBox("Correos enviados correctamente", MsgBoxStyle.Information, "Worcome Security")
        Catch ex As Exception
            Console.WriteLine("[PerOneSender@SenderPerOne]Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Dim TXBVR = InputBox("Ingrese la direccion de correo para buscarla", "Worcome Security")
            If TXBVR = Nothing Then
            Else
                ListBox1.SelectedItem = TXBVR
            End If
        Catch ex As Exception
            Console.WriteLine("[PerOneSender@btnSearch_Click]Error: " & ex.Message)
        End Try
    End Sub
#End Region
End Class