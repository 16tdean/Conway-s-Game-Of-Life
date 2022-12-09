Public Class Form1
    Dim xDirection As Integer = 30
    Dim yDirection As Integer = 19

    Dim list(xDirection, yDirection) As Label
    Dim a(7, 2) As Integer
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For x = 0 To xDirection
            For y = 0 To yDirection
                Dim newLabel As New Label With {
                   .Location = New Point(100 + (50 * x), (50 * y)),
                   .Size = New Size(50, 50),
                   .BackColor = Color.White,
                   .BorderStyle = BorderStyle.FixedSingle,
                   .Tag = 0
                }
                list(x, y) = newLabel 'adds all lables to list
                AddHandler list(x, y).Click, AddressOf Me.toggleLife
                Me.Controls.Add(list(x, y))
            Next
        Next

        a = {{-1, -1}, {-1, 0}, {-1, 1}, {0, -1}, {0, 1}, {1, -1}, {1, 0}, {1, 1}}
    End Sub

    Sub loadLabels()
        For x = 0 To xDirection
            For y = 0 To yDirection
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
        Dim changeList(xDirection, yDirection) As Integer
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
                        If list(x2, y2).Tag = 1 Then
                            aliveCells += 1
                        End If

                    End If
                Next

                If list(x, y).Tag = 0 And aliveCells = 3 Then
                    changeList(x, y) = 1
                ElseIf list(x, y).Tag = 1 And (aliveCells <> 2 And aliveCells <> 3) Then
                    changeList(x, y) = 0
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
        If sender.tag = 1 Then
            sender.tag = 0
        Else
            sender.tag = 1
        End If

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
        numAlive = randomGen(0, 300)
        For i = 0 To numAlive
            list(randomGen(0, xDirection), randomGen(0, yDirection)).Tag = 1
        Next

        loadLabels()
    End Sub

    Private Sub btnRandomGen_Click(sender As Object, e As EventArgs) Handles btnRandomGen.Click
        loadBoard()
    End Sub
End Class
