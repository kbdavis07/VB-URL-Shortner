Imports System.Data.Entity.Validation
Imports System.Diagnostics


Public Class Repository

    Private db As New UrlContext



    Public Function Add(model)

        Return db.Codes.Add(model)

    End Function





    Public Function Save()

        Try
            Return db.SaveChanges()

        Catch ex As DbEntityValidationException

            'Retrieve the error messages as a list of strings.
            Dim errorMessages = ex.EntityValidationErrors.SelectMany(Function(x) x.ValidationErrors).Select(Function(x) x.ErrorMessage)

            'Join the list to a single string.
            Dim fullErrorMessage = String.Join("; ", errorMessages)

            'Combine the original exception message with the new one.
            Dim exceptionMessage = String.Concat(ex.Message, "The DataBase validation errors are: ", fullErrorMessage)

            'Throw a new DbEntityValidationException with the improved exception message.
            Throw New DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors)

        End Try

    End Function




End Class
