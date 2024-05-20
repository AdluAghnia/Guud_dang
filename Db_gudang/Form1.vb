Imports MySql.Data.MySqlClient
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Viewdata()
    End Sub

    Sub Viewdata()
        Call Koneksi()
        Dim query As String = "SELECT * FROM barang"
        Dim cmd As New MySqlCommand(query, Conn)
        Dim dataTable As New DataTable
        Dim adapter As New MySqlDataAdapter(cmd)

        Try
            adapter.Fill(dataTable)
            DataGridView1.DataSource = dataTable
        Catch ex As Exception
            MessageBox.Show("Error : " + ex.Message)
        Finally
            Conn.Close()
        End Try

    End Sub

    Private Sub InputButton_Click(sender As Object, e As EventArgs) Handles InputButton.Click
        Call Koneksi()

        Dim query As String = "INSERT INTO barang VALUES(@id, @nama, @jumlah)"
        Dim cmd As New MySqlCommand(query, Conn)
        cmd.Parameters.AddWithValue("@id", TextId.Text)
        cmd.Parameters.AddWithValue("@nama", TextNama.Text)
        cmd.Parameters.AddWithValue("@jumlah", TextJumlah.Text)

        Try
            cmd.ExecuteNonQuery()
            MsgBox("Berhasil Di Simpan")
        Catch ex As Exception
            MessageBox.Show("Error : " + ex.Message)
        Finally
            Conn.Close()
        End Try
        TextId.Text = ""
        TextNama.Text = ""
        TextJumlah.Text = ""
        Viewdata()

    End Sub

    Private Sub EditButton_Click(sender As Object, e As EventArgs) Handles EditButton.Click
        Dim oForm As New Form3

        oForm.ShowDialog()
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        Dim oForm As New Form2

        oForm.ShowDialog()
    End Sub

    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        Call Koneksi()

        Dim query As String = "DELETE FROM barang"
        Dim cmd As New MySqlCommand(query, Conn)

        Try
            cmd.ExecuteNonQuery()
            MsgBox("Tabel berhasil di bersihkan")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Viewdata()
    End Sub

End Class
