@ModelType FinalProject.Code
@Code

End Code

<div class="row">



    <div class="col-md-12">

      <h2>URL Shortener</h2>

        <br />


        <div class="alert alert-dismissible alert-success">
            <button type="button" class="close" data-dismiss="alert">×</button>
            <p>Below just enter in any URL you like to turn into a small URL.</p>
            
            <p>Just be sure to leave out the <b>Http://</b> section
            of any URL.</p>  
            
            <p>With your new Shorten URL you can use it to post on Social Media, Email, and other many uses. </p>

            <br/>
            <p>It makes it easier for people to share and copy having an Shorter URL.</p>
        </div>




        <br />

        @Code

            If (IsPost) Then

            @<h4>@ViewBag.Message </h4>
                
                @<p><a href="~/s/@ViewBag.ShortCode">@ViewBag.ShortURL</a></p>
            
            Else

            @<h4>@ViewBag.Message</h4>

            End If

        End Code





        @Using (Html.BeginForm())
            @Html.AntiForgeryToken()

            @<div class="form-horizontal">

                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                <div class="col-md-10">
                    <div class="input-group">
                        @*@Html.LabelFor(Function(model) model.LongURL, htmlAttributes:=New With {.class = "control-label"})*@

                        <div class="input-group">
                            @Html.EditorFor(Function(model) model.LongURL, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = "Leave out Http://"}})
                            <span class="input-group-btn">
                                <button class="btn btn-success" type="submit" title="Submit" />
                                <span class="glyphicon glyphicon-play"></span>

                            </span>
                        </div>


                        @Html.ValidationMessageFor(Function(model) model.LongURL, "", New With {.class = "text-danger"})
                    </div>
                </div>


            </div>
        End Using

    </div>
</div>





@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
