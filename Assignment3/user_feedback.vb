﻿
Imports Microsoft.Data.SqlClient
Imports System.Configuration
Public Class user_feedback
    Dim currentRating As Double = 0.0
    Dim clickCount1 As Integer = 0
    Dim clickCount2 As Integer = 0
    Dim clickCount3 As Integer = 0
    Dim clickCount4 As Integer = 0
    Dim clickCount5 As Integer = 0
    Private initialText As String = "Write your feedback here..."
    Dim connectionString As String = ConfigurationManager.ConnectionStrings("MyConnectionString").ConnectionString

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        clickCount1 += 1
        If clickCount1 Mod 2 = 0 Then
            currentRating = 0.5
        Else
            currentRating = 1.0
        End If
        UpdateStars()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        clickCount2 += 1
        If clickCount2 Mod 2 = 0 Then
            currentRating = 1.5
        Else
            currentRating = 2.0
        End If
        UpdateStars()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        clickCount3 += 1
        If clickCount3 Mod 2 = 0 Then
            currentRating = 2.5
        Else
            currentRating = 3
        End If
        UpdateStars()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        clickCount4 += 1
        If clickCount4 Mod 2 = 0 Then
            currentRating = 3.5
        Else
            currentRating = 4
        End If
        UpdateStars()
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        clickCount5 += 1
        If clickCount5 Mod 2 = 0 Then
            currentRating = 4.5
        Else
            currentRating = 5
        End If
        UpdateStars()
    End Sub

    Private Sub UpdateStars()
        ' Update the star images based on the current rating
        Select Case currentRating
            Case 0.5

                PictureBox1.Image = My.Resources.half_star
                PictureBox2.Image = My.Resources.grey_star
                PictureBox3.Image = My.Resources.grey_star
                PictureBox4.Image = My.Resources.grey_star
                PictureBox5.Image = My.Resources.grey_star
            Case 1.0

                PictureBox1.Image = My.Resources.gold_star
                PictureBox2.Image = My.Resources.grey_star
                PictureBox3.Image = My.Resources.grey_star
                PictureBox4.Image = My.Resources.grey_star
                PictureBox5.Image = My.Resources.grey_star
            Case 1.5

                PictureBox1.Image = My.Resources.gold_star
                PictureBox2.Image = My.Resources.half_star
                PictureBox3.Image = My.Resources.grey_star
                PictureBox4.Image = My.Resources.grey_star
                PictureBox5.Image = My.Resources.grey_star
            Case 2.0

                PictureBox1.Image = My.Resources.gold_star
                PictureBox2.Image = My.Resources.gold_star
                PictureBox3.Image = My.Resources.grey_star
                PictureBox4.Image = My.Resources.grey_star
                PictureBox5.Image = My.Resources.grey_star
            Case 2.5

                PictureBox1.Image = My.Resources.gold_star
                PictureBox2.Image = My.Resources.gold_star
                PictureBox3.Image = My.Resources.half_star
                PictureBox4.Image = My.Resources.grey_star
                PictureBox5.Image = My.Resources.grey_star
            Case 3.0

                PictureBox1.Image = My.Resources.gold_star
                PictureBox2.Image = My.Resources.gold_star
                PictureBox3.Image = My.Resources.gold_star
                PictureBox4.Image = My.Resources.grey_star
                PictureBox5.Image = My.Resources.grey_star
            Case 3.5

                PictureBox1.Image = My.Resources.gold_star
                PictureBox2.Image = My.Resources.gold_star
                PictureBox3.Image = My.Resources.gold_star
                PictureBox4.Image = My.Resources.half_star
                PictureBox5.Image = My.Resources.grey_star
            Case 4

                PictureBox1.Image = My.Resources.gold_star
                PictureBox2.Image = My.Resources.gold_star
                PictureBox3.Image = My.Resources.gold_star
                PictureBox4.Image = My.Resources.gold_star
                PictureBox5.Image = My.Resources.grey_star
            Case 4.5

                PictureBox1.Image = My.Resources.gold_star
                PictureBox2.Image = My.Resources.gold_star
                PictureBox3.Image = My.Resources.gold_star
                PictureBox4.Image = My.Resources.gold_star
                PictureBox5.Image = My.Resources.half_star

            Case 5

                PictureBox1.Image = My.Resources.gold_star
                PictureBox2.Image = My.Resources.gold_star
                PictureBox3.Image = My.Resources.gold_star
                PictureBox4.Image = My.Resources.gold_star
                PictureBox5.Image = My.Resources.gold_star
        End Select
    End Sub







    ' Your PictureBox click event handlers and UpdateStars method here...

    Private Sub feedback_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles txtFeedback.Enter
        ' Clear the text when the user clicks inside if it matches the initial text
        If txtFeedback.Text = initialText Then
            txtFeedback.Text = ""
            txtFeedback.ForeColor = Color.Black ' Set the text color to black
        End If
    End Sub

    Private Sub feedback_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles txtFeedback.Leave
        ' Restore the initial text when the user leaves the TextBox without entering anything
        If String.IsNullOrWhiteSpace(txtFeedback.Text) Then
            txtFeedback.Text = initialText
            txtFeedback.ForeColor = Color.Gray ' Set the text color to gray
        End If
    End Sub
    Private Sub InitializeFeedbackTextBox()
        ' Set the initial text
        txtFeedback.Text = initialText
        ' Set the text color to gray
    End Sub


    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Dim reviewText As String = txtFeedback.Text
        Dim dealId As Integer = 1 ' Example deal ID, replace with actual deal ID
        Dim rating As Double = currentRating
        Dim reviewTime As DateTime = DateTime.Now

        If reviewText = initialText Then
            MessageBox.Show("Please enter your review.")
            Return
        End If

        ' Create SQL connection and command objects
        Using connection As New SqlConnection(connectionString)
            Try
                connection.Open()

                ' SQL query to insert review into the database
                Dim query As String = "INSERT INTO [dbo].[review] ([deal_id], [review_text], [review_time], [rating]) " &
                "VALUES (@dealId, @reviewText, @reviewTime, @rating)"

                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@dealId", dealId)
                    command.Parameters.AddWithValue("@reviewText", reviewText) ' Use SqlDbType.NVarChar for string
                    command.Parameters.AddWithValue("@reviewTime", reviewTime) ' Use SqlDbType.DateTime for DateTime
                    command.Parameters.AddWithValue("@rating", rating) ' Use SqlDbType.Float for double

                    ' Execute the SQL command
                    command.ExecuteNonQuery()

                    MessageBox.Show("Review submitted successfully.")
                End Using
            Catch ex As Exception
                MessageBox.Show("Error submitting review: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub user_feedback_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeFeedbackTextBox()

    End Sub
End Class