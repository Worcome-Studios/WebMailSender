Imports System.Net.Mail
Public Class HTMLCorreo

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBoxORIGEN.Text = My.Settings.MyCorreo 
        TextBoxCONTRASEÑA.Text = My.Settings.Contraseña
        TextBoxDESTINO.Text = My.Settings.Correos
        TextBoxDisplayName.Text = My.Settings.DisplayName
        TextBoxASUNTO.Text = My.Settings.Subject
        FastColoredTextBox1.Text = "<html>" & vbCrLf & vbCrLf & vbCrLf & vbCrLf & "</html>"
    End Sub

    Private Sub ButtonENVIAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonENVIAR.Click
        If MessageBox.Show("¿Seguro de que quieres Enviar este Mensaje?", "Worcome Security", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            SendEmail(TextBoxDESTINO.Text, True)
        Else
            Me.Show()
        End If
    End Sub

    Sub SendEmail(ByVal EmailAddress As String, ByVal ShowMSG As Boolean)
        Dim MIHTML As String = FastColoredTextBox1.Text 'TEXTO HTML QUE SE ENVIARA
        Dim VISTAHTML As AlternateView = AlternateView.CreateAlternateViewFromString(MIHTML, Nothing, System.Net.Mime.MediaTypeNames.Text.Html)
        Try
            Dim MENSAJE As MailMessage = New MailMessage 'DECLARA EL MENSAJE....
            MENSAJE.AlternateViews.Add(VISTAHTML) '... Y QUE VA EN FORMATO HTML
            MENSAJE.From = New MailAddress(TextBoxORIGEN.Text, TextBoxDisplayName.Text) 'DEFINE EL ORIGEN
            MENSAJE.To.Add(EmailAddress) 'DEFINE EL DESTINO
            MENSAJE.Subject = TextBoxASUNTO.Text 'DEFINE EL ASUNTO
            Dim MISMT As SmtpClient = New SmtpClient("smtp.gmail.com") 'CLIENTE MAIL QUE USAMOS (GMAIL)
            MISMT.EnableSsl = True 'SISTEMA DE SEGURIDAD
            MISMT.Port = "587"
            MISMT.Credentials = New Net.NetworkCredential(TextBoxORIGEN.Text, TextBoxCONTRASEÑA.Text) 'CREDENCIALES DEL ORIGEN
            MISMT.Send(MENSAJE) 'ENVIA
            If ShowMSG = True Then
                MsgBox("Correo Enviado Correctamente", MsgBoxStyle.Information, "Worcome Security")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Worcome Security")
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Try
            OpenFileDialog1.OpenFile()
            FastColoredTextBox1.Text = My.Computer.FileSystem.OpenTextFileReader(OpenFileDialog1.FileName).ReadToEnd
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Worcome Security")
        End Try
    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click
        Label5.ForeColor = Color.Green
        My.Settings.MyCorreo = TextBoxORIGEN.Text
        My.Settings.Contraseña = TextBoxCONTRASEÑA.Text
        My.Settings.DisplayName = TextBoxDisplayName.Text
        My.Settings.Save()
        My.Settings.Reload()
    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click
        Label7.ForeColor = Color.Green
        My.Settings.Correos = TextBoxDESTINO.Text
        My.Settings.Subject = TextBoxASUNTO.Text
        My.Settings.Save()
        My.Settings.Reload()
    End Sub

    Private Sub ViewerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewerToolStripMenuItem.Click
        Viewer.WebBrowser1.DocumentText = FastColoredTextBox1.Text
        Viewer.Show()
        Viewer.Focus()
        Viewer.WebBrowser1.DocumentText = FastColoredTextBox1.Text
    End Sub

    Private Sub NuevoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NuevoToolStripMenuItem.Click
        FastColoredTextBox1.Clear()
        FastColoredTextBox1.AppendText("<html>" & vbCrLf & vbCrLf & vbCrLf & vbCrLf & "</html>")
    End Sub

    Private Sub AbrirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AbrirToolStripMenuItem.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub GuardarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GuardarToolStripMenuItem.Click
        Try
            My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, FastColoredTextBox1.Text, False)
        Catch ex As Exception
            MsgBox("Primero debe Clickear en 'Guardar Como'", MsgBoxStyle.Critical, "Worcome Security")
        End Try
    End Sub

    Private Sub GuardarComoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GuardarComoToolStripMenuItem.Click
        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirToolStripMenuItem.Click
        End
    End Sub

    Private Sub SaveFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Try
            My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, FastColoredTextBox1.Text, False)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Worcome Security")
        End Try
    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click
        PerOneSender.Show()
        PerOneSender.Focus()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        If TextBoxCONTRASEÑA.PasswordChar = "●" Then
            TextBoxCONTRASEÑA.PasswordChar = Nothing
        Else
            TextBoxCONTRASEÑA.PasswordChar = "●"
        End If
    End Sub
End Class
