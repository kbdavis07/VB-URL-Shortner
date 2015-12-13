Public Class Shortener

    'final = rdm.Next(0, 100000).ToString("00000")

    'Generates a Random String of Everything of any length
    Public Function GetRandomString(iLength As Integer) As String
        Dim sResult As String = ""
        Dim rdm As New Random()

        Dim allowableChars() As Char = "LMNYZ0123$GH4567abcdefghijkTlmnuvwx!yzABCDEFQRS-X89IJ#KOPoUVWpqrst".ToCharArray()

        For i As Integer = 1 To iLength
            'sResult &= ChrW(rdm.Next(32, 126))

            sResult &= allowableChars(rdm.Next(allowableChars.Length - 1))

        Next

        Return sResult
    End Function

    







End Class
