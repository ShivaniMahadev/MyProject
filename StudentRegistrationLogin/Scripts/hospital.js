$(document).ready(function () {
            $("body").on("click", "#btnUpload", function (event) {
                event.preventDefault();
                var file = $("#imageBrowes").get(0).files;
                var data = new FormData;
                //data.append("fileName", $("#fileName").val());
                data.append("ImageFile", file[0]);
                data.append("ProductName", "SamsungA8");

                $.ajax({
                    
                    type: "Post",
                    url: "/Register/ImageUpload",
                    data: data,
                    cache: false,
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                    async: false,
                    success: function (result) {
                        $("#fileProgress").show();
                        var percent = Math.round(100);
                        $('#fileProgress').attr('aria-valuenow', percent).css('width', percent + '%').text(percent + '%');
                        $("#fileProgress").attr('value', 100);
                        //$("#fileProgress").val(70);
                        $("#lblMessage").html("<b>" + result + "</b> has been uploaded.");
                        $('#FileBrowse').find("*").prop("disabled", true);
                        //$("#uploadedImage").append('<img src="/UploadedImage/' + result + '" class="img-responsive thumbnail" width="100" height="100"/>');
                        Registercall();

                    }
                    

                });

            });
            $("#imageBrowes").change(function () {

                var File = this.files;

                if (File && File[0]) {
                    ReadImage(File[0]);
                }

            })

        })


        var ReadImage = function (file) {

            var reader = new FileReader;
            var image = new Image;

            reader.readAsDataURL(file);
            reader.onload = function (_file) {

                image.src = _file.target.result;
                image.onload = function () {

                    var height = this.height;
                    var width = this.width;
                    var type = file.type;
                    var size = ~~(file.size / 1024) + "KB";

                    $("#targetImg").attr('src', _file.target.result);
                    $("#description").text("Size:" + size + ", " + height + "X " + width + ", " + type + "");
                    $("#imgPreview").show();

                }

            }

        }

        var ClearPreview = function () {
            $("#imageBrowes").val('');
            $("#description").text('');
            $("#imgPreview").hide();

        }


        //function LoadProgressBar(result) {
        //    var progressbar = $("#progressbarbtn");
        //    var progressLabel = $(".progress-label");
        //    progressbar.show();
        //    $("#progressbarbtn").progressbar({
        //        //value: false,
        //        change: function () {
        //            progressLabel.text(
        //                progressbar.progressbar("value") + "100%");  // Showing the progress increment value in progress bar
        //        },
        //        complete: function () {
        //            progressLabel.text("Loading Completed!");
        //            progressbar.progressbar("value", 0);  //Reinitialize the progress bar value 0
        //            progressLabel.text("");
        //            progressbar.hide(); //Hiding the progress bar
        //            var markup = "<tr><td>" + result + "</td><td><a href='#' onclick='DeleteFile(\"" + result + "\")'><span class='glyphicon glyphicon-remove red'></span></a></td></tr>"; // Binding the file name
        //            $("#ListofFiles tbody").append(markup);
        //            $('#imageBrowes').val('');
        //            $('#FileBrowse').find("*").prop("disabled", false);
        //        }
        //    });
        //    function progress() {
        //        var val = progressbar.progressbar("value") || 0;
        //        progressbar.progressbar("value", val + 1);
        //        if (val < 99) {
        //            setTimeout(progress, 25);
        //        }
        //    }
        //    setTimeout(progress, 100);
        //}

        function Registercall() {
            $.ajax({

                type: "Post",
                url: "/Account/Register",
                contentType: false,
                processData: false,
                success: function (response) {

                    ClearPreview();
                }

            });
        }

