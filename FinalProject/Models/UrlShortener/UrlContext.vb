Imports System
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Linq

Partial Public Class UrlContext
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=UrlContext")
    End Sub

    Public Overridable Property Codes As DbSet(Of Code)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        modelBuilder.Entity(Of Code)() _
            .Property(Function(e) e.Status) _
            .IsFixedLength()
    End Sub


   




End Class
