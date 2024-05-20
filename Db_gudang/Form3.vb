Imports MySql.Data.MySqlClient
Imports Org.BouncyCastle.Asn1.Misc

Public Class Form3

    Private id As String
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Dim barisTerpilih As DataGridViewRow
        If e.RowIndex >= 0 Then
            barisTerpilih = DataGridView1.Rows(e.RowIndex)
            id = barisTerpilih.Cells(0).Value.ToString()
            TextNama.Text = barisTerpilih.Cells(1).Value.ToString()
            TextJumlah.Text = barisTerpilih.Cells(2).Value.ToString()
        End If

    End Sub

    Private Sub ButtonEdit_Click(sender As Object, e As EventArgs) Handles ButtonEdit.Click
        Call Koneksi()

        Dim query As String = "UPDATE barang SET nama=@newNama, jumlah=@newJumlah WHERE id=@id"
        Dim cmd As New MySqlCommand(query, Conn)
        cmd.Parameters.AddWithValue("@id", id)
        cmd.Parameters.AddWithValue("@newNama", TextNama.Text)
        cmd.Parameters.AddWithValue("@newJumlah", TextJumlah.Text)

        Try
            cmd.ExecuteNonQuery()
            MsgBox("Data Berhasil di edit")
        Catch ex As Exception
            MessageBox.Show("Error : ", ex.Message)
        Finally
            Conn.Close()
        End Try

        TextNama.Text = ""
        TextJumlah.Text = ""
        Viewdata()

        Application.OpenForms.OfType(Of Form1)().FirstOrDefault()?.Viewdata()
    End Sub

End Class