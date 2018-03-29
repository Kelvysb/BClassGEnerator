Public Class frmTest
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim objGenerator As BClassGenerator.ClassGenerator
        Dim objPattern As BClassGenerator.clsPattern
        Dim objReturn As List(Of BClassGenerator.clsGeneratedFile)
        Dim strAuxReturn As String

        Try

            objGenerator = New BClassGenerator.ClassGenerator("192.168.159.200", "KICHEIRO_DISTRIBUIDORA", "Adapta", "safaribaiao", BDataBase.DataBase.enmDataBaseType.MsSql)
            objPattern = New BClassGenerator.clsPattern()
            objPattern.BaseName = "Cliente"

            txtBase.Text = objPattern.serialize

            objReturn = objGenerator.fnGenerateByTable("TBCLI", objPattern)

            strAuxReturn = ""
            For Each File In objReturn
                For Each Line In File.Lines
                    strAuxReturn = strAuxReturn & Line
                Next
            Next
            txtOutput.Text = strAuxReturn

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class
