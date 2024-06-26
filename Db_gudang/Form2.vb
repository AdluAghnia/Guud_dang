﻿Imports MySql.Data.MySqlClient

Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            TextId.Text = barisTerpilih.Cells(0).Value.ToString()
            TextNama.Text = barisTerpilih.Cells(1).Value.ToString()
            TextJumlah.Text = barisTerpilih.Cells(2).Value.ToString()
        End If

    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        Call Koneksi()
        Dim query As String = "DELETE FROM barang WHERE id=@id_barang"
        Dim cmd As New MySqlCommand(query, Conn)
        cmd.Parameters.AddWithValue("@id_barang", TextId.Text)

        Try
            cmd.ExecuteNonQuery()
            MsgBox("Data Berhasil di hapus")
        Catch ex As Exception
            MessageBox.Show("Error : ", ex.Message)
        Finally
            Conn.Close()
        End Try

        TextId.Text = ""
        TextNama.Text = ""
        TextJumlah.Text = ""
        Viewdata()

        Application.OpenForms.OfType(Of Form1)().FirstOrDefault()?.Viewdata()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub
End Class