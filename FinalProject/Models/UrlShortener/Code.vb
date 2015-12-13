Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("Code")>
Partial Public Class Code
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property Id As Guid

    Public Property OwnerId As Guid

    <StringLength(10)>
    Public Property Status As String

    <Column("ShortCode")>
    <Required>
    <StringLength(6)>
    Public Property ShortCode As String

    <Required>
    Public Property ShortURL As String

    <Required>
    Public Property LongURL As String

    <Column(TypeName:="datetime2")>
    Public Property Created As Date

    Public Property Hits As Integer?
End Class
