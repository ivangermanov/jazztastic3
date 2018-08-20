<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="navbar.aspx.cs" Inherits="Jazztastic3ASPXWebForms.templates.navbar" %>

<!-- Navigation -->
<link rel="stylesheet" href="../css/navbar.css">
<nav class="navbar navbar-expand-lg navbar-dark fixed-top" id="mainNav">
<div class="container"> 
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarResponsive">
            <ul class="navbar-nav">
                <li class="nav-item">
                    <a class="nav-link" href="../Index.aspx">Home</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="../Register.aspx">Tickets</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="../Location.aspx">Location</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="../Location.aspx#schedule">Schedule</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="../Map.aspx">Map</a>
                </li>
            </ul>
            <ul class="navbar-nav ml-auto">
                <li class="nav-item">
                    <a id="login" runat="server" class="nav-link" href="../Login.aspx">Log In</a>
                </li>
            </ul>
        </div>
    </div>
    <script src="../startbootstrap-one-page-wonder-gh-pages/vendor/jquery/jquery.min.js"></script>
    <script src="../startbootstrap-one-page-wonder-gh-pages/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="../jquery/jquery.magnific-popup.min.js"></script>
    <script src="../javascript/scrollreveal.min.js"></script>
    <script src="../jquery/jquery-easing/jquery.easing.min.js"></script>
    <script src="../javascript/navbar-scroll.js"></script>
    <script>
        $(document).ready(function() {
	    // get current URL path and assign 'active' class
        var href = location.href;
        var pathname = href.match(/([^\/]*)\/*$/)[1];
        var cleanPathname = "";
        for (var i = 0; i < pathname.length; i++)
        {
            if (pathname[i] != '#')
            {
                cleanPathname = cleanPathname + pathname[i];
            }
            else
            {
                break;
            }
        }
        $('.navbar-nav > li > a[href="'+cleanPathname+'"]').parent().addClass('active');
        })
    </script>
    <script src="../javascript/slowscroll.js"></script>
</nav>


