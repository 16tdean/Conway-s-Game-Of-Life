Public Class Form1
    Dim xDirection As Integer = 52
    Dim yDirection As Integer = 28

    Dim list(xDirection, yDirection) As Label
    Dim a(7, 2) As Integer
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For x = 0 To xDirection
            For y = 0 To yDirection
                Dim newLabel As New Label With {
                   .Location = New Point(100 + (25 * x), (25 * y)),
                   .Size = New Size(25, 25),
                   .BackColor = Color.White,
                   .BorderStyle = BorderStyle.FixedSingle,
                   .Tag = False
                }
                list(x, y) = newLabel 'adds all lables to list
                AddHandler list(x, y).Click, AddressOf Me.toggleLife
                Me.Controls.Add(list(x, y))
            Next
        Next

        a = {{-1, -1}, {-1, 0}, {-1, 1}, {0, -1}, {0, 1}, {1, -1}, {1, 0}, {1, 1}}
        lblSpeed.Text = 500
    End Sub

    Sub loadLabels()
        For x = 0 To xDirection
            For y = 0 To yDirection
                If list(x, y).Tag = True Then
                    list(x, y).BackColor = Color.Black
                Else
                    list(x, y).BackColor = Color.White
                End If
            Next
        Next
    End Sub

    Sub updateGame()
        Dim aliveCells As Integer
        Dim changeList(xDirection, yDirection) As Boolean
        Dim x2, y2 As Integer

        For x = 0 To xDirection
            For y = 0 To yDirection
                changeList(x, y) = list(x, y).Tag
            Next
        Next

        For x = 0 To xDirection
            For y = 0 To yDirection
                aliveCells = 0
                x2 = 0
                y2 = 0
                For i = 0 To 7
                    x2 = x + a(i, 0)
                    y2 = y + a(i, 1)

                    If xDirection < x2 Then
                        x2 = 0
                    End If

                    If x2 < 0 Then
                        x2 = xDirection
                    End If

                    If yDirection < y2 Then
                        y2 = 0
                    End If

                    If y2 < 0 Then
                        y2 = yDirection
                    End If

                    If xDirection >= x2 And x2 >= 0 And yDirection >= y2 And y2 >= 0 Then
                        If list(x2, y2).Tag Then
                            aliveCells += 1
                        End If

                    End If
                Next

                If list(x, y).Tag = False And aliveCells = 3 Then
                    changeList(x, y) = True
                ElseIf list(x, y).Tag = True And (aliveCells <> 2 And aliveCells <> 3) Then
                    changeList(x, y) = False
                End If

            Next
        Next

        For x = 0 To xDirection
            For y = 0 To yDirection
                list(x, y).Tag = changeList(x, y)
            Next
        Next
    End Sub

    Sub toggleLife(sender As Object, e As EventArgs)
        sender.tag = Not sender.tag
        loadLabels()
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Timer1.Enabled = Not Timer1.Enabled
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        updateGame()
        loadLabels()
    End Sub

    Function randomGen(min, max)
        Static generator As System.Random = New System.Random()
        Return generator.Next(min, max)
    End Function

    Sub loadBoard()
        Dim numAlive As Integer
        numAlive = randomGen(50, 400)
        For i = 0 To numAlive
            list(randomGen(0, xDirection + 1), randomGen(0, yDirection + 1)).Tag = True
        Next

        loadLabels()
    End Sub

    Private Sub btnRandomGen_Click(sender As Object, e As EventArgs) Handles btnRandomGen.Click
        loadBoard()
    End Sub

    Private Sub VScrollBar1_Scroll(sender As Object, e As ScrollEventArgs) Handles VScrollBar1.Scroll
        Timer1.Interval = VScrollBar1.Value + 1
        lblSpeed.Text = VScrollBar1.Value
    End Sub
End Class
