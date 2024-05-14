Imports MySql.Data.MySqlClient

Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IsiCheckedListBox()
    End Sub

    Sub IsiCheckedListBox()
        Call Koneksi()
        Dim query As String = "SELECT id, nama, jumlah FROM barang"
        Dim cmd As New MySqlCommand(query, Conn)
        Dim reader As MySqlDataReader

        Try
            reader = cmd.ExecuteReader()
            CheckedListBox1.Items.Clear()
            While reader.Read()
                Dim displayString As String = String.Format("{0} - {1} - {2}", reader("id").ToString(), reader("nama").ToString(), reader("jumlah").ToString())
                CheckedListBox1.Items.Add(displayString)
            End While
        Catch ex As Exception
            MessageBox.Show("Error : " + ex.Message)
        Finally
            Conn.Close()
        End Try
    End Sub


    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        Call Koneksi()
        Dim list_id As New List(Of String)

        For Each barang In CheckedListBox1.CheckedItems
            Dim id As String = barang.Split(" "c)(0)
            list_id.Add(id)
        Next

        Dim query As String = "DELETE FROM barang WHERE id=@id_barang"
        Dim cmd As New MySqlCommand(query, Conn)
        For Each id In list_id
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@id_barang", id)

            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show("Error : " + ex.Message)
            End Try
        Next

        Conn.Close()

        Application.OpenForms.OfType(Of Form1)().FirstOrDefault()?.Viewdata()

        Me.Close()
    End Sub
End Class