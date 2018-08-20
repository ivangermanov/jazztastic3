<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Jazztastic3ASPXWebForms.Login" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Jazztastic 3- Login</title>

    <!-- Bootstrap core CSS -->
    <link href="startbootstrap-one-page-wonder-gh-pages/vendor/bootstrap/css/bootstrap.css" rel="stylesheet">

    <!-- Custom fonts for this template -->
    <link href="https://fonts.googleapis.com/css?family=Catamaran:100,200,300,400,500,600,700,800,900" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i" rel="stylesheet">

    <!-- For the fonts and footer -->
    <link href="css/agency.min.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="startbootstrap-one-page-wonder-gh-pages/css/one-page-wonder.css" rel="stylesheet">
    <link href="css/login.css" rel="stylesheet">
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

    <img class="img-responsive" src="images/tickets_2_transformed_login.jpg" style="position: absolute; z-index: -100;" />
    <form runat="server">
        <header class="masthead text-center text-white">
            <div class="masthead-content">
                <div class="row">
                    <div class="col-1"></div>
                    <div class="col-md-4">
                    </div>
                    <div class="col-3"></div>
                    <div class="col-md-3">

                        <label for="inputEmail" class="sr-only">Email address</label>
                        <input type="email" id="email" name="inputEmail" class="form-control mt-2" placeholder="Email address" required="" autofocus="">
                        <label for="inputPassword" class="sr-only">Password</label>
                        <input type="password" id="password" name="inputPassword" class="form-control mt-2" placeholder="Password" required="">
                        <!--<button class="btn btn-lg btn-primary btn-block" type="submit">Sign in</button>-->
                        <asp:Button runat="server" class="btn btn-lg btn-primary btn-block" OnClick="Submit_Clicked" Text="Submit" />
                        <a class="btn btn-sm btn-primary mt-2" href="Register.aspx">Tickets</a>

                    </div>
                </div>
            </div>
        </header>
        <div class="container">
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
                                        <input maxlength="64" runat="server" class="w-75" type="email" id="email1" name="email" placeholder="your@email.com">
                                </div>
                                <asp:Button id="buttonSubscribe" runat="server" Text="Subscribe Now!" class="btn btn-lg btn-primary" OnClick="Subscribe_Click" />

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
            document.getElementById("email").required = false;
            document.getElementById("password").required = false;
        });
    </script>
    <!-- Bootstrap core JavaScript -->
    <script src="/jquery/jquery.min.js"></script>
    <script src="/bootstrap/js/bootstrap.bundle.min.js"></script>
</body>
</html>
