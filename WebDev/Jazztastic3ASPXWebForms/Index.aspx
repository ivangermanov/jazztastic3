<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Jazztastic3ASPXWebForms.Index" %>

<!DOCTYPE html>
<html>

<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <title>Jazztastic 3</title>

    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- Bootstrap core CSS -->
    <link rel="stylesheet" href="startbootstrap-one-page-wonder-gh-pages/vendor/bootstrap/css/bootstrap.css">

    <!-- Custom fonts for this template -->
    <link href="https://fonts.googleapis.com/css?family=Catamaran:100,200,300,400,500,600,700,800,900" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i" rel="stylesheet">

    <!-- For the about -->
    <link href="css/agency.min.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="css/index.css" rel="stylesheet">

    <!-- Animation library -->
    <link href="animations/aos-master/dist/aos.css" rel="stylesheet">

    <link rel="stylesheet" href="javascript/FlipClock-master/src/flipclock/css/flipclock.css" />
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
        <script src="javascript/FlipClock-master/compiled/flipclock.min.js"></script>
    </nav>

    <header class="text-center text-white">
        <div class="parallax-window" data-parallax="scroll" data-image-src="images/index_jazz.jpg">
            <div class="masthead-content pt-5">
                <div class="container pt-5">
                    <h1 class="masthead-heading mb-0">
                        <img src="images/jazzstastic3logo.png" style="width:60%;"></h1>
                    <div class="row"><div class="col-2 mr-5"></div><div class="col-sm"><div class="flip-counter"></div></div></div>
                    <a href="Register.aspx" class="btn btn-primary btn-xl rounded-pill">GET TICKETS</a>
                </div>
            </div>
            <a class="ct-btn-scroll ct-js-btn-scroll scroll" id="arrow" href="#performers">
                <img alt="Arrow Down Icon" class="ct-btn-scroll" src="https://www.solodev.com/assets/anchor/arrow-down.png"></a>
        </div>
    </header>

    <section id="performers">
        <section class="pt-5">
            <div class="container">
                <div class="row d-flex justify-content-center">
                    <div data-aos="fade-up" class="col-md-8 pb-80 header-text">
                        <h1 class="text-center">Our Performers</h1>
                        <p class="text-center">
                            Are you ready to partake in the long-awaited sequel of award-winning Jazztastic 1 and Jazztastic 2?
                            Jazztastic 3 makes sure to provide you with the most popular and in-demand contemporary jazz artists!
                            <br>
                            Check it out!
                        </p>
                    </div>
                </div>
                <div class="row align-items-center">
                    <div class="col-lg-6 order-lg-2">
                        <div data-aos="fade-left" class="p-5">
                            <img class="img-thumbnail" src="images/jazz_concert_1_resized.jpg" alt="">
                        </div>
                    </div>
                    <div class="col-lg-6 order-lg-1">
                        <div data-aos="fade-right" class="p-5">
                            <h2 class="display-4">Animals As Leaders</h2>
                            <p>
                                They are Genre Bending. Jazz-fusion. It is a gem of a beast. 
                                Yes, beast. They are a prominent band within the djent scene. Prosthetic Records 
                                released the band's eponymous debut album in 2009. They went on to release the albums Weightless (2011), 
                                The Joy of Motion (2014), and The Madness of Many (2016). And now they've come
                                all the way from Washington D.C. here to perform at Jazztastic 3!
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section>
            <div class="container">
                <div class="row align-items-center">
                    <div class="col-lg-6">
                        <div data-aos="fade-right" class="p-5">
                            <img class="img-thumbnail" src="images/jazz_concert_2.jpg" alt="">
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div data-aos="fade-left" class="p-5">
                            <h2 class="display-4">Panzerballett!</h2>
                            <p>
                                Panzerballett (Germany) is a Munich instrumental quintet led by guitarist, composer and arranger 
                                Jan Zehrfeld whose music style is best described as jazz-metal. Panzerballett is also known 
                                for exceptional concerts. The humor of the band, which is only implied in the band's albums  
                                occurs during their live performances more obviously, particularly guitarist Zehrfeld supplies, 
                                cable Rasta wig and sunglasses, dressed in a stage show, with exaggerated facial expressions 
                                and gestures, dance and exuberant Hüpfeinlagen and headbanging. 
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section>
            <div class="container">
                <div class="row align-items-center">
                    <div class="col-lg-6 order-lg-2">
                        <div data-aos="fade-left" class="p-5">
                            <img class="img-thumbnail" src="images/jazz_concert_3.jpg" alt="">
                        </div>
                    </div>
                    <div class="col-lg-6 order-lg-1">
                        <div data-aos="fade-right" class="p-5">
                            <h2 class="display-4">New Black Eagle Jazz Band</h2>
                            <p>
                                The New Black Eagle Jazz Band is a New Orleans Style Jazz band founded in 1971 and based in New England. 
                                Four of the members had previously been in the Black Eagle Jazz Band led by Tommy Sancton. 
                                The band has seven core members. Music performed by the band has been used as soundtrack music
                                in the Ken Burns documentaries Jazz and Baseball. 
                                The band has also been a guest on A Prairie Home Companion and NPR. They have released over
                                40 recordings.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section>
            <div class="container">
                <div class="row align-items-center">
                    <div class="col-lg-6">
                        <div data-aos="fade-right" class="p-5">
                            <img class="img-thumbnail" src="images/performers/george_benson.jpg" alt="">
                        </div>
                    </div>
                    <div class="col-lg-6 order-lg-1">
                        <div data-aos="fade-left" class="p-5">
                            <h2 class="display-4">George Benson</h2>
                            <p>
                                Simply one of the greatest guitarists in jazz history, George Benson is an amazingly versatile
                                musician, whose adept skills find him crossing easily between straight-ahead jazz, smooth jazz,
                                and contemporary R&B. Blessed with supreme taste, a beautiful, rounded guitar tone, 
                                terrific speed, a marvelous sense of logic in building solos, and, always, an unquenchable
                                urge to swing, Benson's inspirations may have been Charlie Christian and Wes Montgomery, but 
                                his style is completely his own.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section>
            <div class="container">
                <div class="row align-items-center">
                    <div class="col-lg-6 order-lg-2">
                        <div data-aos="fade-left" class="p-5">
                            <img class="img-thumbnail" src="images/performers/jaga_jazzist.jpg" alt="">
                        </div>
                    </div>
                    <div class="col-lg-6 order-lg-1">
                        <div data-aos="fade-right" class="p-5">
                            <h2 class="display-4">Jaga Jazzist</h2>
                            <p>
                                Jaga Jazzist (also known as Jaga) is an experimental jazz band from Norway that rose to
                                prominence when the BBC named their second album, A Livingroom Hush 
                                (Smalltown Supersound/Ninja Tune), the best jazz album of 2002. And now they're bringing 
                                their best to Jazztastic 3!
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section>
            <div class="container">
                <div class="row align-items-center">
                    <div class="col-lg-6">
                        <div data-aos="fade-right" class="p-5">
                            <img class="img-thumbnail" src="images/performers/james_carter.jpg" alt="">
                        </div>
                    </div>
                    <div class="col-lg-6 order-lg-1">
                        <div data-aos="fade-left" class="p-5">
                            <h2 class="display-4">James Carter</h2>
                            <p>
                                After Wynton Marsalis, no one caused more of an uproar than James Carter did when he 
                                appeared on the New York jazz scene from his native Detroit. Carter's debut recording, 
                                JC on the Set, issued in Japan when he was only 23, and in the States a year later in 1993, 
                                was universally acclaimed as the finest debut by a saxophonist in decades.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section>
            <div class="container">
                <div class="row align-items-center">
                    <div class="col-lg-6 order-lg-2">
                        <div data-aos="fade-left" class="p-5">
                            <img class="img-thumbnail" src="images/performers/jazzanova.jpg" alt="">
                        </div>
                    </div>
                    <div class="col-lg-6 order-lg-1">
                        <div data-aos="fade-right" class="p-5">
                            <h2 class="display-4">Jazzanova</h2>
                            <p>
                                Jazzanova is a German Berlin-based DJ/producer collective consisting of Alexander Barck, 
                                Claas Brieler, Jürgen von Knoblauch, Roskow Kretschmann, Stefan Leisering, and Axel Reinemer. 
                                Formed in 1995,[2] the group's music is characterized by nu jazz, chill-out and jazz house 
                                as well as Latin jazz styles. They founded the record label Sonar Kollektiv in 1997.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section>
            <div class="container">
                <div class="row align-items-center">
                    <div class="col-lg-6">
                        <div data-aos="fade-right" class="p-5">
                            <img class="img-thumbnail" src="images/performers/preservation_hall_jazz_band.png" alt="">
                        </div>
                    </div>
                    <div class="col-lg-6 order-lg-1">
                        <div data-aos="fade-left" class="p-5">
                            <h2 class="display-4">Preservation Hall Jazz Band</h2>
                            <p>
                                Preservation Hall Jazz Band is a New Orleans jazz band founded in New Orleans by tuba player
                                Allan Jaffe in the early 1960s. The band derives its name from Preservation Hall in the French
                                Quarter. In 2005, the Hall's doors were closed for a period of time due to Hurricane Katrina, 
                                but the band continued to tour. And now.. they're coming to Jazztastic 3!
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section class="pt-5 pb-4" id="about">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 text-center">
                        <h2 data-aos="fade-up" class="section-heading text-uppercase">About
                            <img hspace="20" src="images/jazzstastic3logo.png" width="35%">by
                            <img hspace="20" src="images/logo_main.png">
                        </h2>
                        <h3 data-aos="fade-up" class="section-subheading text-muted">Eventmaster is one of the oldest established event-organising companies, specialized in providing
                            top-notch concerts and music events. Being your most trusted organizer company for such a long time
                            we have brought award-winning Jazztastic 1, 2 and now 3 to your attention.
                        </h3>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <ul class="timeline">
                            <li>
                                <div class="timeline-image">
                                    <img class="rounded-circle img-fluid" src="images/jazz_about_1.png" alt="">
                                </div>
                                <div class="timeline-panel">
                                    <div class="timeline-heading">
                                        <h4>2010-2011</h4>
                                        <h4 class="subheading">Our Humble Beginnings</h4>
                                    </div>
                                    <div class="timeline-body">
                                        <p class="text-muted">
                                            Eventmaster was founded in 2010. We organised several small events in 2010, ranging from Jazz to rap and R&B. Since then
                                            we have been gaining the trust of the public and were soon recognised as one of the best new and growing event-organizing companies.
                                        </p>
                                    </div>
                                </div>
                            </li>
                            <li class="timeline-inverted">
                                <div class="timeline-image">
                                    <img class="rounded-circle img-fluid" src="images/jazz_about_2.png" alt="">
                                </div>
                                <div class="timeline-panel">
                                    <div class="timeline-heading">
                                        <h4>March 2013</h4>
                                        <h4 class="subheading">Jazztastic 1 is born</h4>
                                    </div>
                                    <div class="timeline-body">
                                        <p class="text-muted">
                                            2013 was the birth year of Jazztastic 1, which swept the world off her feet with its convivial atmosphere,
                                            cozy environment and brilliant artists and musicians. Jazztastic 1 brought such astonishment upon its
                                            audience that it was awarded with the Best Jazz Event award of 2013.
                                        </p>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="timeline-image">
                                    <img class="rounded-circle img-fluid" src="images/jazz_about_3.png" alt="">
                                </div>
                                <div class="timeline-panel">
                                    <div class="timeline-heading">
                                        <h4>December 2015</h4>
                                        <h4 class="subheading">But the people wanted more.. Jazztastic 2</h4>
                                    </div>
                                    <div class="timeline-body">
                                        <p class="text-muted">
                                            Due to the massive success of Jazztastic 1, its visitors wanted to reexperience and
                                            relive that same feeling of awe and inspiration brought about by it. And Eventmaster listened.
                                            Jazztastic 2 managed to improve even more than its predecessor, bringing astonishment yet again
                                            to its audience.
                                        </p>
                                    </div>
                                </div>
                            </li>
                            <li class="timeline-inverted">
                                <div class="timeline-image">
                                    <img class="rounded-circle img-fluid" src="images/jazz_about_4.png" alt="">
                                </div>
                                <div class="timeline-panel">
                                    <div class="timeline-heading">
                                        <h4>1<sup>st</sup>-3<sup>rd</sup> July 2018</h4>
                                        <h4 class="subheading">And now... Jazztastic 3</h4>
                                    </div>
                                    <div>
                                        <div class="timeline-body">
                                            <p class="text-muted">
                                                3 years after Jazztastic 2 was too long a time for the audience to contend with.
                                            It desired more, so Eventmaster provided. Jazztastic 3 was born out of
                                            the thirst for awe and excitement. And now.. it's your turn to partake. Don't ponder
                                            whether to participate, as the loyalty of the public has proven Jazztastic's authority and prestige
                                            in the Jazz event scene, the choice is obvious!
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </li>
                                <li class="timeline-inverted"">
                            <a href="Register.aspx" style="text-decoration:none">
                                <div id="partOfStory" class="timeline-image">
                                    <h4>Be Part
                                        <br>
                                        Of Our
                                        <br>
                                        Story!
                                    </h4>
                                </div>
                            </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </section>
    </section>
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

                            <form runat="server">
                                <div class="input-prepend">
                                    <span class="add-on"><span class="input-group-label">
                                        <i class="fa fa-envelope" style="font-size: 2em"></i>
                                    </span>
                                        <input maxlength="64" runat="server" class="w-75" type="email" id="email" name="email" placeholder="your@email.com">
                                </div>
                                <asp:Button runat="server" Text="Subscribe Now!" class="btn btn-lg btn-primary" OnClick="Subscribe_Click" />
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
    <script src="animations/aos-master/dist/aos.js"></script>
    <script>
        AOS.init();
    </script>
    <!-- JQuery parallax-->
    <script src="startbootstrap-one-page-wonder-gh-pages/vendor/jquery/parallax.js-1.5.0/parallax.js"></script>

    <script>
			var clock;

			$(document).ready(function() {

				// Grab the current date
                var currentDate = new Date();
				var jazztastic3date = new Date(2018, 6, 1, 0, 0, 0, 0);

                // Calculate the difference in seconds between the future and current date
                var diff = jazztastic3date.getTime() / 1000 - currentDate.getTime() / 1000;

				// Instantiate a coutdown FlipClock
				clock = $('.flip-counter').FlipClock(diff, {
					clockFace: 'DailyCounter',
					countdown: true
				});
			});
    </script>

</body>

</html>
