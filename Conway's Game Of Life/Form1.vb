Public Class Form1
    Dim list(10, 10) As Label
    Dim a(7, 2) As Integer
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For x = 0 To 10
            For y = 0 To 10 'creates 42 labels
                Dim newLabel As New Label With {
                   .Location = New Point(100 + (75 * x), (75 * y)),
                   .Size = New Size(75, 75),
                   .BackColor = Color.White,
                   .BorderStyle = BorderStyle.FixedSingle,
                   .Tag = 0
                }
                list(x, y) = newLabel 'adds all lables to list
                AddHandler list(x, y).Click, AddressOf Me.Click
                Me.Controls.Add(list(x, y))
            Next
        Next

        a = {{-1, -1}, {-1, 0}, {-1, 1}, {0, -1}, {0, 1}, {1, -1}, {1, 0}, {1, 1}}
    End Sub

    Sub loadLabels()
        For x = 0 To 10
            For y = 0 To 10
                If list(x, y).Tag = 1 Then
                    list(x, y).BackColor = Color.Black
                Else
                    list(x, y).BackColor = Color.White
                End If
            Next
        Next
    End Sub

    Sub updateGame()
        Dim aliveCells As Integer
        For x = 0 To 10
            For y = 0 To 10
                aliveCells = 0
                For i = 0 To 7
                    If 11 > x + a(i, 0) And x + a(i, 0) >= 0 And 11 > y + a(i, 1) And y + a(i, 1) >= 0 Then
                        If list(x + a(i, 0), y + a(i, 1)).Tag = 1 Then
                            aliveCells += 1
                        End If
                    End If
                Next

                If list(x, y).Tag = 0 And aliveCells = 3 Then
                    list(x, y).Tag = 1
                ElseIf list(x, y).Tag = 1 And (aliveCells <> 2 Or aliveCells <> 3) Then
                    list(x, y).Tag = 0
                End If

            Next
        Next
    End Sub

    Sub click(sender As Object, e As EventArgs)
        If sender.tag = 1 Then
            sender.tag = 0
        Else
            sender.tag = 1
        End If

        loadLabels()
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        updateGame()
        loadLabels()
    End Sub
End Class
