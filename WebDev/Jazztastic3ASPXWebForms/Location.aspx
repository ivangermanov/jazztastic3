<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Location.aspx.cs" Inherits="Jazztastic3ASPXWebForms.Location" %>

<!DOCTYPE html>
<html>

<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <title>Jazztastic 3 - Location</title>

    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- Bootstrap core CSS -->
    <link rel="stylesheet" href="startbootstrap-one-page-wonder-gh-pages/vendor/bootstrap/css/bootstrap.css">

    <!-- Custom fonts for this template -->
    <link href="https://fonts.googleapis.com/css?family=Catamaran:100,200,300,400,500,600,700,800,900" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i" rel="stylesheet">

    <!-- For the about -->
    <link href="css/agency.min.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="css/location.css" rel="stylesheet">
</head>
<body>
    <!-- Navigation -->
    <link rel="stylesheet" href="css/navbar.css">
    <nav class="navbar navbar-expand-lg navbar-dark fixed-top" id="mainNav">
        <div class="container">
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarResponsive">
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <a class="nav-link" href="Index.aspx">Home</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="Register.aspx">Tickets</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="Location.aspx">Location</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="Location.aspx#schedule">Schedule</a>
                    </li>
                </ul>
                <ul class="navbar-nav ml-auto">
                    <% if (Convert.ToBoolean(Session["LoggedIn"]))
                        { %>
                    <li class="nav-item">
                        <a class="nav-link" href="UserPanel.aspx">User Panel</a>
                    </li>
                    <% } %>
                    <li class="nav-item">
                        <a id="loginNav" runat="server" class="nav-link" href="Login.aspx">Log In</a>
                    </li>
                </ul>
            </div>
        </div>
        <script src="startbootstrap-one-page-wonder-gh-pages/vendor/jquery/jquery.min.js"></script>
        <script src="startbootstrap-one-page-wonder-gh-pages/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
        <script src="jquery/jquery.magnific-popup.min.js"></script>
        <script src="javascript/scrollreveal.min.js"></script>
        <script src="jquery/jquery-easing/jquery.easing.min.js"></script>
        <script src="javascript/navbar-scroll.js"></script>
        <script>
            $(document).ready(function () {
                // get current URL path and assign 'active' class
                var href = location.href;
                var pathname = href.match(/([^\/]*)\/*$/)[1];
                var cleanPathname = "";
                for (var i = 0; i < pathname.length; i++) {
                    if (pathname[i] != '#') {
                        cleanPathname = cleanPathname + pathname[i];
                    }
                    else {
                        break;
                    }
                }
                $('.navbar-nav > li > a[href="' + cleanPathname + '"]').parent().addClass('active');
            })
        </script>
        <script src="javascript/slowscroll.js"></script>
    </nav>

    <header class="text-center text-white">
        <div class="parallax-window" data-parallax="scroll" data-image-src="images/location_2.jpg">
            <!-- <img class="img-responsive" src="images/index_jazz.jpg" alt=""> -->
            <div class="masthead-content pt-5">
                <div class="container pt-5">
                    <h1 class="masthead-heading mb-0 pt-5">
                        <img src="images/jazzstastic3logo.png" width="60%"></h1>
                    <h1 class="masthead-heading mb-0">Location</h1>
                    <!-- <h2 class="masthead-subheading mb-0">Will Rock Your Socks Off</h2> -->
                </div>
            </div>
            <a class="ct-btn-scroll ct-js-btn-scroll scroll" id="arrow" href="#location">
                <img alt="Arrow Down Icon" class="ct-btn-scroll" src="https://www.solodev.com/assets/anchor/arrow-down.png"></a>
        </div>
    </header>
    <form runat="server">
        <!--Google map-->
        <section id="location" class="pt-3 pb-2">
            <div class="row justify-content-center">
                <div class="col-xs-6">
                    <div>
                        <h2>Jazztastic 3</h2>
                        <div>
                            <iframe frameborder="0" src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d9640.019542427113!2d6.72369!3d52.840297!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0xe1bd06427483d7af!2sMolecaten+Park+Kuierpad!5e0!3m2!1sen!2sus!4v1524171665990" style="width: 100%; height: 475px;"></iframe>
                        </div>
                    </div>
                </div>
                <div class="pl-5 pr-5">
                    <div class="row">
                        <div class="col-xs-6">
                            <h2>Contact us</h2>
                        </div>
                    </div>
                    <div class="row bg-light pt-3 pb-3 mb-4">
                        <div class="col-8">
                            <h6>ADDRESS :</h6>
                        </div>
                        <div class="col-5">
                            Rachelsmolen 1,
                        5612 MA Eindhoven,
                        Netherlands
                        </div>
                        <div class="col-7">
                            <p class="m-0 text-danger">
                                <i class="fa fa-phone-square" aria-hidden="true"></i>
                                +31 1234567890
                            </p>
                            <p class="m-0 text-info">
                                <i class="fa fa-envelope" aria-hidden="true"></i>
                                eventM@yopmail.com
                            </p>
                        </div>
                    </div>
                    <div class="row bg-light pt-3 pb-3 mb-4">
                        <div class="col-6">

                            <div class="form-row mb-3">
                                <div class="col">
                                    <input runat="server" required type="text" id="name1" name="name" class="form-control" placeholder="Name">
                                </div>
                                <div class="col">
                                    <input runat="server" type="text" id="company1" class="form-control" placeholder="Company (optional)">
                                </div>
                            </div>
                            <div class="form-row mb-3">
                                <div class="col">
                                    <input runat="server" required type="email" id="email" class="form-control" placeholder="Email">
                                </div>
                                <div class="col">
                                    <input runat="server" type="text" id="mobile1" class="form-control" placeholder="Mobile (opional)">
                                </div>
                            </div>
                            <div class="form-group">
                                <textarea runat="server" required style="resize: none" class="form-control" id="message1" rows="3" placeholder="Message"></textarea>
                            </div>
                            <asp:Button type="button" ID="button1" runat="server" Text="Send" class="btn btn-danger mb-4 w-100" OnClick="SendMail_ServerClick" />
                        </div>
                        <div class="col-6">
                            <div style="width: 100%">
                                <iframe width="100%" height="300px" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2075.7177287984214!2d5.479412299538159!3d51.45180827760805!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x47c6d92199730073%3A0x2a4de046a5d850af!2sRachelsmolen+1%2C+5612+MA+Eindhoven!5e0!3m2!1sen!2snl!4v1522691708244" frameborder="0" scrolling="no" marginheight="0" marginwidth="0"></iframe>
                            </div>
                        </div>
                    </div>
                </div>
        </section>


        <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDTCgViryuy3KnNeAUtctNmogJ7Rm_vcW4&callback=initMap"></script>

        <div id="schedule" class="text-center text-white pb-3" style="background-color: #5a6978;">
            <h1 class="masthead-heading mb-0 pt-3">Schedule</h1>
            <div class="container">
                <div class="row">
                    <div class="col">
                        <div class="row">
                            <div class="col mt-2 mb-2" style="min-width: 375px;">
                                <div class="card" style="border-radius: 21px 21px 9px 9px; border: none;">
                                    <div class="card-header" style="background-color: #252526; border: none; border-radius: 20px 20px 0px 0px;">
                                        Day 1
                                    </div>
                                    <div class="card-body" style="background-color: #78A1BB;">
                                        <div class="card m-2 " style="border: none;">
                                            <div class="card-header" style="background-color: #252526;">
                                                <div class="row">
                                                    <div class="col-6">Animals as Leaders 1</div>
                                                    <div class="col-6">13:00</div>
                                                </div>
                                            </div>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-8 ">
                                                        <p class="card-text" style="color: black;">Animals as Leaders playing their Weightless album on stage, warming up the audience for more!</p>
                                                    </div>
                                                    <div class="col">
                                                        <div class="row ">
                                                            <button type="button" class="btn btn-primary mt-1 btn_local" onclick="">location</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card m-2 " style="border: none;">
                                            <div class="card-header" style="background-color: #252526;">
                                                <div class="row">
                                                    <div class="col-6">New Black Eagle Jazz Band</div>
                                                    <div class="col-6">16:00</div>
                                                </div>
                                            </div>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-8 ">
                                                        <p class="card-text" style="color: black;">After decades being split up, Jazztastic 3 has gathered New Black Eagle Jazz Band's seven veteran members to perform just for you!</p>
                                                    </div>
                                                    <div class="col">
                                                        <div class="row ">
                                                            <button type="button" class="btn btn-primary mt-1 btn_local" onclick="">location</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card m-2 " style="">
                                            <div class="card-header" style="background-color: #252526;">
                                                <div class="row">
                                                    <div class="col-6">Panzerballett!</div>
                                                    <div class="col-6">20:00</div>
                                                </div>
                                            </div>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-8 ">
                                                        <p class="card-text" style="color: black;">And for an appropriate ending of the evening, the Munich instrumental quintet Panzerballett is going to bring some jazz-metal!</p>
                                                    </div>
                                                    <div class="col">
                                                        <div class="row ">
                                                            <button type="button" class="btn btn-primary mt-1 btn_local" onclick="">location</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col mt-2 mb-2" style="min-width: 375px;">
                                <div class="card" style="border-radius: 21px 21px 9px 9px; border: none;">
                                    <div class="card-header" style="background-color: #252526; border: none; border-radius: 20px 20px 0px 0px;">
                                        Day 2
                                    </div>
                                    <div class="card-body" style="background-color: #78A1BB; border-radius: 0px 0px 10px 10px;">
                                        <div class="card m-2 " style="border: none;">
                                            <div class="card-header" style="background-color: #252526;">
                                                <div class="row">
                                                    <div class="col-6">Preservation Hall Jazz Band</div>
                                                    <div class="col-6">14:00</div>
                                                </div>
                                            </div>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-8 ">
                                                        <p class="card-text" style="color: black;">After a proper night's respite, Preservation Hall Jazz Band is ready to play some relaxing afternoon jazz.</p>
                                                    </div>
                                                    <div class="col">
                                                        <div class="row ">
                                                            <button type="button" class="btn btn-primary mt-1 btn_local" onclick="">location</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card m-2 " style="border: none;">
                                            <div class="card-header" style="background-color: #252526;">
                                                <div class="row">
                                                    <div class="col-6">George Benson</div>
                                                    <div class="col-6">17:00</div>
                                                </div>
                                            </div>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-8 ">
                                                        <p class="card-text" style="color: black;">George Benson, the man, the artist, the singer, do not miss one of his best performances. </p>
                                                    </div>
                                                    <div class="col">
                                                        <div class="row ">
                                                            <button type="button" class="btn btn-primary mt-1 btn_local" onclick="">location</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card m-2 " style="border: none;">
                                            <div class="card-header" style="background-color: #252526;">
                                                <div class="row">
                                                    <div class="col-6">Jaga Jazzist</div>
                                                    <div class="col-6">17:00</div>
                                                </div>
                                            </div>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-8 ">
                                                        <p class="card-text" style="color: black;">The band that will take you back to Jazz' roots through an incredibile journey.</p>
                                                    </div>
                                                    <div class="col">
                                                        <div class="row ">
                                                            <button type="button" class="btn btn-primary mt-1 btn_local" onclick="">location</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card m-2 " style="border: none;">
                                            <div class="card-header" style="background-color: #252526;">
                                                <div class="row">
                                                    <div class="col-6">James Carter</div>
                                                    <div class="col-6">18:00</div>
                                                </div>
                                            </div>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-8 ">
                                                        <p class="card-text" style="color: black;">Coming straight out of Detroit, bringing the essence of Jazz with him in his saxophone, the great James Carter.</p>
                                                    </div>
                                                    <div class="col">
                                                        <div class="row ">
                                                            <button type="button" class="btn btn-primary mt-1 btn_local" onclick="">location</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col mt-2 mb-2" style="min-width: 375px;">
                                <div class="card" style="border-radius: 21px 21px 9px 9px; border: none;">
                                    <div class="card-header" style="background-color: #252526; border: none; border-radius: 20px 20px 0px 0px;">
                                        Day 3
                                    </div>
                                    <div class="card-body" style="background-color: #78A1BB;">
                                        <div class="card m-2 " style="border: none;">
                                            <div class="card-header" style="background-color: #252526;">
                                                <div class="row">
                                                    <div class="col-6">Jazzanova</div>
                                                    <div class="col-6">20:00</div>
                                                </div>
                                            </div>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-8 ">
                                                        <p class="card-text" style="color: black;">Bright and warm Jazzanova,they will let you hear Jazz from places light-years distant.</p>
                                                    </div>
                                                    <div class="col">

                                                        <div class="row ">
                                                            <button type="button" class="btn btn-primary mt-1 btn_local" onclick="">location</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card m-2 " style="border: none;">
                                            <div class="card-header" style="background-color: #252526;">
                                                <div class="row">
                                                    <div class="col-6">Preservation Hall Jazz Band</div>
                                                    <div class="col-6">21:00</div>
                                                </div>
                                            </div>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-8 ">
                                                        <p class="card-text" style="color: black;">From the Heart of Jazz, their music brought down hurricanes, the unstoppable band will make you fall in love with the Jazz.</p>
                                                    </div>
                                                    <div class="col">

                                                        <div class="row ">
                                                            <button type="button" class="btn btn-primary mt-1 btn_local" onclick="">location</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                      
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        
         

        <!-- Modal location-->
       <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" id="locationmodal" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body"  >
                        <img id="map1" src="images/sub1.png" class="img-fluid" style="display:none;"/>
                        <img id="map2" src="images/sub2.png" class="img-fluid" style="display:none;" />
                        <img id="map3" src="images/sub3.png" class="img-fluid" style="display:none;" />
                        <img id="map4" src="images/sub4.png" class="img-fluid" style="display:none;" />
                        <img id="map5" src="images/sub5.png" class="img-fluid" style="display:none;" />
                        <img id="map6" src="images/sub6.png" class="img-fluid" style="display:none;" />
                        <img id="map7" src="images/sub7.png" class="img-fluid" style="display:none;" />
                        <img id="map8" src="images/sub8.png" class="img-fluid" style="display:none;" />

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Footer -->
        <link rel="stylesheet" href="../css/Footer-with-button-logo.css">
        <script defer src="https://use.fontawesome.com/releases/v5.0.9/js/all.js" integrity="sha384-8iPTk2s/jMVj81dnzb/iFR2sdA7u06vHJyyLlAd4snFpCl/SnyUjRrbdJsw1pGIl" crossorigin="anonymous"></script>

        <footer class="pt-4" id="myFooter">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-sm-4 pt-4">
                        <h2><a href="#">
                            <img class="img-fluid" src="../images/logo_main.png"></a></h2>
                    </div>
                    <div class="col-sm-4 pt-4">
                        <div class="span12">
                            <div class="thumbnail center well well-small text-center">
                                <h3>Subscribe to newsletter</h3>


                                <div class="input-prepend">
                                    <span class="add-on"><span class="input-group-label">
                                        <i class="fa fa-envelope" style="font-size: 2em"></i>
                                    </span>
                                        <input maxlength="64" runat="server" class="w-75" type="email" id="email1" name="email1" placeholder="your@email.com">
                                </div>
                                <asp:Button type="button" ID="buttonSubscribe" runat="server" Text="Subscribe Now!" class="btn btn-lg btn-primary" OnClick="Subscribe_Click" />

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="social-networks">
                            <a href="#" class="twitter"><i class="fab fa-twitter"></i></a>
                            <a href="#" class="facebook"><i class="fab fa-facebook"></i></a>
                            <a href="#" class="instagram"><i class="fab fa-instagram"></i></a>
                            <a href="#" class="google"><i class="fab fa-google-plus"></i></a>
                        </div>
                        <button type="button" class="btn btn-primary btn-lg"><a href="../Location.aspx#location">Contact us</a></button>
                    </div>
                </div>
            </div>
            <div class="footer-copyright">
                <p>© Copyright Eventmaster, Jazztastic3 2018 </p>
            </div>
        </footer>
    </form>
    <script>
        $("#buttonSubscribe").click(function (event) {
            document.getElementById("name").required = false;
            document.getElementById("email").required = false;
            document.getElementById("message").required = false;
        });
    </script>
    <script src="jquery/jquery-easing/jquery.easing.min.js"></script>
    <!-- JQuery parallax-->
    <script src="startbootstrap-one-page-wonder-gh-pages/vendor/jquery/parallax.js-1.5.0/parallax.js"></script>
    <script src="javascript/slowscroll.js"></script>

    <!-- Modal script-->
    <script type="text/javascript">
        
        leng = 0;
        $(".btn_local").click(function () {
            $("#map" + leng).hide();
            leng = this.parentNode.parentNode.parentNode.parentNode.parentNode.firstChild.nextSibling.firstChild.nextSibling.childNodes[1].firstChild.textContent.length;

            leng = leng % 10 + Math.floor(leng / 10)-1;
            $("#map" + leng).show(0);
            recalModalLocation();



        });


        function recalModalLocation()
        {
            
            $('#locationmodal').modal('show');
        }
    </script>
</body>
</html>
