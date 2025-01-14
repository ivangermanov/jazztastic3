﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="footer.aspx.cs" Inherits="Jazztastic3ASPXWebForms.templates.footer" %>

<link rel="stylesheet" href="../css/Footer-with-button-logo.css">
<script defer src="https://use.fontawesome.com/releases/v5.0.9/js/all.js" integrity="sha384-8iPTk2s/jMVj81dnzb/iFR2sdA7u06vHJyyLlAd4snFpCl/SnyUjRrbdJsw1pGIl" crossorigin="anonymous"></script>

<footer class="pt-4" id="myFooter">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-4 pt-4">
                    <h2><a href="#"><img class="img-fluid" src="../images/logo_main.png"></a></h2>
                </div>
                <div class="col-sm-4 pt-4">
                        <div class="span12">
                            <div class="thumbnail center well well-small text-center">
                                <h3>Subscribe to newsletter</h3>
                                
                                <form action="" method="post">
                                    <div class="input-prepend"><span class="add-on"><i class="icon-envelope"></i></span>
                                        <input class="w-75" type="email" id="" name="" placeholder="your@email.com">
                                    </div>
                                    <input type="submit" value="Subscribe Now!" class="btn btn-lg btn-primary" />
                            </form>
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
