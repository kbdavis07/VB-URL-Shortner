Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports FinalProject


Public Class HomeController
    Inherits System.Web.Mvc.Controller

Dim Random = New Shortener

        Private db As New UrlContext

        Private Repo As New Repository


    Function About() As ActionResult
        ViewData("Message") = "Your application description page."

        Return View()
    End Function

    Function Contact() As ActionResult
        ViewData("Message") = "Your contact page."

        Return View()
    End Function




        ' GET: Codes/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim code As Code = db.Codes.Find(id)
            If IsNothing(code) Then
                Return HttpNotFound()
            End If
            Return View(code)
        End Function

        ' GET: 
        Function Index() As ActionResult

             ViewBag.Title = "URL Shortener"
             ViewBag.Message = "Please Enter in Your URL To Shorten Below"
             Return View()

        End Function

' POST: 
'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
<HttpPost()>
<ValidateAntiForgeryToken()>
Function Index(LongURL As String) As ActionResult



'''''''Step 1 Check if LongURL is already in DataBase'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


   'If Not String.IsNullOrEmpty(LongURL) Then

   '   Dim q = From a In db.Codes() Where a.LongURL = LongURL

   '     If Not q Is Nothing Then

   '            'Hack: Only way to get it to return a single value
   '            For Each item In q

   '                  ViewBag.Title = "URL Already Exists"
   '                  ViewBag.Message = "The URL you entered is already in our database here is the ShortURL :"

   '                  ViewBag.ShortCode = item.ShortCode
   '                  ViewBag.ShortURL = item.ShortURL

   '               Next

   '            Return View()

   '     End If
   'End If

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



      If Not String.IsNullOrEmpty(LongURL) Then

               'LongURL Does not Exist Now lets create a new one!

               'Create New Instance Object of Code
               Dim code = New Code

               'Generate A Short Code
               Dim ShortCode = Random.GetRandomString(4).ToString

               'ToDo: Check to see if ShortCode Already exist in Database?

               code.Id = Guid.NewGuid()
               code.OwnerId = Guid.NewGuid()
               code.ShortCode = ShortCode
               code.ShortURL = Request.Url.GetLeftPart(UriPartial.Authority) & "/s/" & ShortCode
               code.LongURL = LongURL
               code.Created = DateTime.Now
               code.Status = "New"

                     If ModelState.IsValid Then
                           db.Codes.Add(code)
                           db.SaveChanges()

                           ViewBag.Title = "URL is Ready"
                           ViewBag.Message = "Here is your New Shorten URL: "
                           ViewBag.ShortCode = code.ShortCode
                           ViewBag.ShortURL = code.ShortURL

                           Return View()
                     End If
      End If



         'Some Uncaught and Unknown Error Happen
         ViewBag.Title = "Houston We have a Problem"
         ViewBag.Message = "Something Went Wrong?"

         Return View()

End Function














        ' GET: Codes/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim code As Code = db.Codes.Find(id)
            If IsNothing(code) Then
                Return HttpNotFound()
            End If
            Return View(code)
        End Function

        ' POST: Codes/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="Id,OwnerId,Status,ShortCode,ShortURL,LongURL,Created,Hits")> ByVal code As Code) As ActionResult
            If ModelState.IsValid Then
                db.Entry(code).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(code)
        End Function

        ' GET: Codes/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim code As Code = db.Codes.Find(id)
            If IsNothing(code) Then
                Return HttpNotFound()
            End If
            Return View(code)
        End Function

        ' POST: Codes/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim code As Code = db.Codes.Find(id)
            db.Codes.Remove(code)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        
      ' GET: s/ShortCode
        Function s(ShortCode As String) As ActionResult

          If IsNothing(ShortCode) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
          End If

          'Dim code As Code = db.Codes.Find(id)

          Dim code = From item In db.Codes Select item

             If Not String.IsNullOrEmpty(ShortCode) Then

                  Dim RedirectURL = db.Codes.SingleOrDefault(Function(x) x.ShortCode.Equals(ShortCode))


           'Dim Hits As Integer = db.Codes.Select(Function(h) h.Hits + 1).ToString

           'c.Hits = Hits
           'c.Status = "Active"
           'db.Entry(c).State = EntityState.Modified
           'db.SaveChanges()

           
           Dim URLGOTO = ("Http://" & RedirectURL.LongURL)

           Response.StatusCode = 301
           Response.RedirectLocation = URLGOTO

           Return Redirect(URLGOTO)

        End If

            If IsNothing(code) Then
                Return HttpNotFound()
            End If
            Return View(code)

End Function





Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class

















 'If ModelState.IsValid Then

 '           '    'ShortURL =  Host+ShortCode

 '           '    '1st Need to check if User Entered LongURL already exist in the database
 '           '    'Dim DoesLongURLExists As Code = db.Codes.SingleOrDefault(Function(U) U.LongURL).LongURL = code.LongURL

 '           '    Dim DoesLongURLExists As Code = db.Codes.Where(Function(L) L.LongURL = code.LongURL).Select(Function(L) L)

 '           '    'User Entered LongURL already exists 
 '           '    If (code.LongURL = DoesLongURLExists.LongURL) Then

 '           '        'Returning the ShortURL of the LongURL found in Database
 '           '        ViewBag.ShortURL = DoesLongURLExists.ShortURL

 '           '        Return View()

 '           '    End If

 '           'Else

 '               'User Entered LongURL to Shorten does not exist, now we shorten the User's Long URL to a Short URL

 '               'Generate A Short Code
 '               Dim ShortCode = Random.GetRandomString(4).ToString

 '               ''See if Short Code is already in the Database
 '               'Dim CheckShortcode = db.Codes.Where(Function(U) U.ShortCode = ShortCode) _
 '               '                             .Select(Function(c) c.ShortCode).ToString



 '               ''If Short code not found, create new one
 '               'If CheckShortcode = String.Empty Then

 '                   'ToDo Check Cookie for Existing OwnerID

 '                   'New User, Does not have a cookie, Guid ID is used to identify the Short URL's a specific user creates
 '                   code.OwnerId = Guid.NewGuid()

 '                   'ToDo Need to save OwnerId to Cookie

 '                   'Gets BaseURL of Current Server and adds it to ShortCode to Create the Full ShortURL http://SomeSite.Com/ShortCode
 '                   code.ShortCode = ShortCode
 '                   code.ShortURL = Request.Url.GetLeftPart(UriPartial.Authority) & "/" & ShortCode
 '                   code.Created = DateTime.Now
 '                   code.Status = "New"

 '                   db.Codes.Add(code)
 '                   db.SaveChanges()

 '                   ViewBag.ShortURL = code.ShortURL

 '                   Return RedirectToAction("Create", "Codes")

 '               End If



 '           'End If

 '           'Uncaught not known Error
 '           Return View(code)