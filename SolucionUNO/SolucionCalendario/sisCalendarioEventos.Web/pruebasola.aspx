<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pruebasola.aspx.vb" Inherits="sisCalendarioEventos.Web.pruebasola" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="bootstrap/css/font-awesome.min.css" rel="stylesheet" />
    <link href="bootstrap/css/calendar.css" rel="stylesheet" />
    <link href="bootstrap/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="bootstrap/css/font-awesome.min-3.2.1.css" rel="stylesheet" />
    <link href="bootstrap/css/funkyradio.css" rel="stylesheet" />
    <link href="typehead/scroll.css" rel="stylesheet" />
    
    <link href="bootstrap/css/fileinput.min.css" rel="stylesheet" />
    <link href="bootstrap/css/fileinput.css" rel="stylesheet" />


    <script src="bootstrap/js/jquery.min.js"></script>
    <script src="bootstrap/js/es-ES.js"></script>
    <script src="bootstrap/js/moment.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="bootstrap/js/bootstrap-datetimepicker.js"></script>
    <script src="bootstrap/js/bootstrap-datetimepicker.es.js"></script>
    <script src="bootstrap/js/underscore-min.js"></script>
    <script src="bootstrap/js/calendar.js"></script>
    <script src="typehead/bootstrap3-typeahead.min.js"></script>
    <script src="pdfConverter/fileinput.min.js"></script>
    <script src="pdfConverter/fileinput.js"></script>
    <script src="pdfConverter/moment.min.js"></script>
</head>
<body>
 <script type="text/javascript">

     function singleFileSelected(evt) {
         //var selectedFile = evt.target.files can use this  or select input file element 
         //and access it's files object
         var selectedFile = ($("#UploadedFile"))[0].files[0];//FileControl.files[0];

         if (selectedFile.type.match(imageType)) {
             var reader = new FileReader();
             reader.onload = function (e) {

                 $("#Imagecontainer").empty();
                 var dataURL = reader.result;
                 var img = new Image()
                 img.src = dataURL;
                 img.className = "thumb";
                 $("#Imagecontainer").append(img);
             };
             reader.readAsDataURL(selectedFile);
         }
         if (selectedFile) {
             var FileSize = 0;
             var imageType = /image.*/;
             if (selectedFile.size > 1048576) {
                 FileSize = Math.round(selectedFile.size * 100 / 1048576) / 100 + " MB";
             }
             else if (selectedFile.size > 1024) {
                 FileSize = Math.round(selectedFile.size * 100 / 1024) / 100 + " KB";
             }
             else {
                 FileSize = selectedFile.size + " Bytes";
             }
             // here we will add the code of thumbnail preview of upload images

             $("#FileName").text("Name : " + selectedFile.name);
             $("#FileType").text("type : " + selectedFile.type);
             $("#FileSize").text("Size : " + FileSize);
         }
     }

     function UploadFile() {
         //we can create form by passing the form to Constructor of formData object
         //or creating it manually using append function 
         //but please note file name should be same like the action Parameter
         //var dataString = new FormData();
         //dataString.append("UploadedFile", selectedFile);

         var form = $('#FormUpload')[0];
         var dataString = new FormData(form);
         $.ajax({
             url: '/Uploader/Upload',  //Server script to process data
             type: 'POST',
             xhr: function () {  // Custom XMLHttpRequest
                 var myXhr = $.ajaxSettings.xhr();
                 if (myXhr.upload) { // Check if upload property exists
                     //myXhr.upload.onprogress = progressHandlingFunction
                     myXhr.upload.addEventListener('progress', progressHandlingFunction,
                     false); // For handling the progress of the upload
                 }
                 return myXhr;
             },
             //Ajax events
             success: successHandler,
             error: errorHandler,
             complete: completeHandler,
             // Form data
             data: dataString,
             //Options to tell jQuery not to process data or worry about content-type.
             cache: false,
             contentType: false,
             processData: false
         });
     }

     function progressHandlingFunction(e) {
         if (e.lengthComputable) {
             var percentComplete = Math.round(e.loaded * 100 / e.total);
             $("#FileProgress").css("width",
             percentComplete + '%').attr('aria-valuenow', percentComplete);
             $('#FileProgress span').text(percentComplete + "%");
         }
         else {
             $('#FileProgress span').text('unable to compute');
         }
     }

 </script>
<div id="FormContent">
           <form id="FormUpload"
           enctype="multipart/form-data" method="post">
               <span class="btn btn-success fileinput-button">
                   <i class="fa fa-check"></i>
                   <span>Add files...</span>
                   <input type="file"
                   name="UploadedFile" id="UploadedFile" />
               </span>
               <button class="btn btn-primary start"
               type="button" id="Submit_btn">
                   <i class="fa fa-check"></i>
                   <span>Start upload</span>
               </button>
                <button class="btn btn-warning cancel"
                type="button" id="Cancel_btn">
                   <i class="fa fa-circle"></i>
                   <span>close</span>
               </button>
           </form>
           <div class="progress CustomProgress">
               <div id="FileProgress"
               class="progress-bar" role="progressbar"
       aria-valuenow="0" aria-valuemin="0"
       aria-valuemax="100" style="width: 0%;">
                   <span></span>
               </div>
           </div>
           <div class="InfoContainer">
               <div id="Imagecontainer"></div>
               <div id="FileName" class="info">
               </div>
               <div id="FileType" class="info">
               </div>
               <div id="FileSize" class="info">
               </div>
           </div>
       </div>
</body>
</html>
