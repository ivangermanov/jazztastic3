<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPanel.aspx.cs" Inherits="Jazztastic3ASPXWebForms.UserPanel" %>


<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Jazztastic 3- User Panel</title>

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

    <!-- Custom JavaScript -->

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
                        <a id="loginNav" runat="server" class="nav-link" href="/Login.aspx">Log In</a>
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


    <img class="img-responsive" src="images/location_jazz.jpg" style="position: absolute; z-index: -100;" />
    <form runat="server" autocomplete="off" id="regForm" onsubmit="return false;">
    <header class="pb-5 text-center text-white">

        <div id="register" class="pb-5 pt-5">
            <div class="container-fluid pt-3 pb-5">
                <div class="row">
                    <!-- Require Styling Here -->
                    <div class="mx-auto">
                        <div class="card text-center">
                            <div class="card-header" style="background-color: #d57e6f;">
                                <h1>User Panel</h1>
                            </div>
                            <div class="card-body text-info table-responsive-lg" style="background-color: #dedfd9;";">
                                <table class="table" style="overflow-x:auto;">
                                    <tbody>
                                        <tr>
                                            <td>Visitor No</td>
                                            <td runat="server" id="visitorNo">Visitor No</td>
                                        </tr>
                                        <tr>
                                            <td>Government ID</td>
                                            <td runat="server" id="governmentId">Government ID</td>
                                        </tr>
                                        <tr>
                                            <td>First Name</td>
                                            <td runat="server" id="firstName">First Name</td>
                                        </tr>
                                        <tr>
                                            <td>Last Name</td>
                                            <td runat="server" id="lastName">Last Name</td>
                                        </tr>
                                        <tr>
                                            <td>Date of birth</td>
                                            <td runat="server" id="dob">DoB</td>
                                        </tr>
                                        <tr>
                                            <td>Email</td>
                                            <td runat="server" id="email">Email</td>
                                        </tr>
                                        <tr>
                                            <td>Money</td>
                                            <td runat="server" id="money">Money</td>
                                        </tr>
                                        
                                        <tr>
                                            <td>Ticket</td>
                                            <td runat="server" id="ticket">Ticket</td>
                                        </tr>
                                        <tr>
                                            <td>Camping</td>
                                            <td runat="server" id="camping">Camping</td>
                                        </tr>
                                    </tbody>
                                </table>
                                <!--<h2 runat="server" id="test">Test</h2>-->
                                <button type="button" class="btn btn-lg btn-primary btn-block" data-toggle="modal" data-target="#addMoneyModal">
                                  Add Money
                                </button>
                                <% if (!Convert.ToBoolean(Session["TicketType"]))
                                    { %>
                                <button type="button" class="btn btn-lg btn-primary btn-block" data-toggle="modal" data-target="#changeTicketModal">
                                  Extend Ticket
                                </button>
                                <% } %>
                                <% if (areaLetterBackEnd == null || campingSpotBackEnd == null || spotsTakenBackEnd == null)
                                    { %>
                                <button id="map_enabler" type="button" onclick="EnableMap()" class="btn btn-lg btn-primary btn-block" data-toggle="modal" data-target="#campingTicketModal" data-backdrop="static" data-keyboard="false">
                                  Add Camping
                                </button>
                                <script>
                                    function EnableMap() {
                                        document.getElementById("map_container").style.display = "block";
                                    }
                                </script>
                                <% } %>
                                <button runat="server" class="btn btn-lg btn-primary btn-block mt-3" onclick="MakeFormSubmittable();" onserverclick="DownloadTicket_ServerClick">Download Ticket</button>

                                <!-- Add Money Modal -->
                                <div class="modal fade" id="addMoneyModal" tabindex="-1" role="dialog" aria-labelledby="addMoneyModalLabel" aria-hidden="true">
                                  <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                      <div class="modal-header">
                                        <h5 class="modal-title" id="addMoneyModalLabel">Add Money</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                          <span aria-hidden="true">&times;</span>
                                        </button>
                                      </div>
                                      <div class="modal-body">
                                        
                                            <input runat="server" name="tbBalance" id="tbBalance" autocomplete="off" type="text" class="form-control" placeholder="5.00" maxlength="7">
                                            <Button type="button" runat="server" onclick="MakeFormSubmittable();" onserverclick="btnAddMoney_ServerClick" class="btn btn-lg btn-primary btn-block mt-3">Submit</Button>
                                        
                                      </div>
                                      <div class="modal-footer">
                                        <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                                      </div>
                                    </div>
                                  </div>
                                </div>

                                <div class="modal fade" id="campingTicketModal" tabindex="-1" role="dialog" aria-labelledby="campingTicketModelLabel" aria-hidden="true">
                                  <div class="modal-dialog modal-lg" role="document">
                                    <div class="modal-content">
                                      <div class="modal-header">
                                      </div>
                                      <div class="modal-body">

<script src="../jquery.panzoom.js"></script>
<script src="../jquery.mousewheel.js"></script>
                                                  <script>
            var spotNumber = 0;
            var areaNumber = "";
            function CheckSpotAvailability(spotNo, areaNo) {
                spotNumber = spotNo;
                areaNumber = areaNo;
                $.ajax({
                    type: "POST",
                    url: "AspDotNetAjax.aspx/AvailableSpot",
                    data: "{ spotNo: '" + parseInt(spotNo) + "',areaNo: '" + areaNo + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: "true",
                    cache: "false",
                    success: onSucceed,
                    Error: onError
                });
            }

            // On Success
            function onSucceed(results, currentContext, methodName) {
                var success = results.d;
                if (success === false) {
                    $('#<%=mapValidation.ClientID%>').css('color', 'red');
                    $('#<%=mapValidation.ClientID%>').html("Spot is not available!");
                    spotNumber = 0;
                    areaNumber = 0;
                }
                else {
                    $('#<%=mapValidation.ClientID%>').css('color', 'green');
                    $('#<%=mapValidation.ClientID%>').html("Spot is available!");
                }
            }

            // On Error
            function onError(results, currentContext, methodName) {
                document.getElementById("mapValidation").innerHTML = "Error";
            }
    </script>
<div id="map_container" class="row" style="display: none; height: 635px;">
    <div class="col-md">
        <div class="card mt-4 mb-4">
            <div id="pickYourSpot" class="card-header">
                Pick your camping spot!
            </div>
            <div class="card-body">
                <div id="final_output" style="display:none;">
                    <h4>Camping spot selected!</h4>
                    <h3 id="campig_spot_output"> Your camping spot is: </h3>
                    <h3 id="campig_spot_number"></h3>
                    <h3>How many people are you going to share your spot with?</h3>
                    <input runat="server" type="range" value="1" min="1" max="6" id="rangeOneToSix" oninput="oneToSix.innerHTML=rangeOneToSix.value">
                    <h3 id="oneToSix">1</h3>
                </div>
                <div id="map">
                    <div id="focal0" class="parent general_focal">
                        <div class="panzoom0 wheel_zoom" style="min-height: 320px; max-height: 420px; max-width: 620px">
                            <img id="myimage0" src="map.png" usemap="#image-map0">
                            <map name="image-map0">
                                <!--<area data-key="5" target="" alt="5" title="5" href="javascript:void(0);" coords="1356,1302,1186,1692,1186,1763,1232,1791,1254,1837,1263,1902,1328,1908,1396,1843,1588,1831,1662,1682,1709,1574,1696,1416,1656,1370,1477,1324,1409,1302" shape="poly">-->
                                <!--<area data-key="4" target="" alt="4" title="4" href="javascript:void(0);" coords="1699,1345,1743,1367,1786,1392,1854,1401,1981,1438,2005,1450,2012,1509,2058,1525,2008,1651,1981,1716,1950,1757,1910,1747,1857,1750,1798,1726,1746,1704,1690,1682,1699,1633,1721,1565,1718,1478" shape="poly">-->
                                <!--<area data-key="3" target="" alt="3" title="3" href="javascript:void(0);" coords="1860,1766,1721,2134,1872,2187,2027,1831" shape="poly">-->
                                <area data-key="2" target="" value="A" href="javascript:void(0);" coords="2438,1596,2732,1552,2751,1630,2797,1633,2856,1710,2942,1720,2992,1685,3004,1726,2958,1877,2330,1896,2336,1837,2383,1797,2435,1701" shape="poly">
                                <area data-key="1" target="" value="B" href="javascript:void(0);" coords="2976,445,2803,946,2775,1061,2803,1175,2837,1348,2927,1367,2986,1342,3013,1333,3051,1221,3069,1064,3088,906,3153,791,3236,677,3255,575,3236,525,3038,433" shape="poly">
                                <!--<area data-key="10" target="" alt="10" title="10" href="javascript:void(0);" coords="2636,289,2321,1133,2330,1239,2423,1576,2723,1545,2831,1437,2815,1291,2763,1062,2800,911,2862,707,2924,533,2973,394,2896,329,2778,308" shape="poly">-->
                                <area data-key="9" target="" value="C" href="javascript:void(0);" coords="2188,275,2132,346,2117,426,2129,482,2077,541,2036,637,2024,661,1993,686,1925,810,1854,943,1838,996,1783,1113,1724,1249,1962,1361,2098,1416,2188,1447,2404,869,2624,290,2321,179,2250,170,2213,204" shape="poly">
                                <!--<area data-key="8" target="" alt="8" title="8" href="javascript:void(0);" coords="1118,1939,1257,1930,1273,1970,1378,2029,1430,2051,1368,2211,1288,2205,1236,2125,1180,2091,1065,2094,1028,2081,1069,2020,1106,1979" shape="poly">-->
                                <!--<area data-key="7" target="" alt="7" title="7" href="javascript:void(0);" coords="1365,1908,1415,1862,1501,1849,1600,1840,1668,1689,1786,1738,1628,2100,1539,2125,1483,2128,1427,2103,1452,2032,1297,1970,1310,1924,1341,1921" shape="poly">-->
                                <!--<area data-key="6" target="" alt="6" title="6" href="javascript:void(0);" coords="1226,1228,1019,1698,1093,1738,1133,1732,1161,1704,1192,1648,1353,1268,1291,1246" shape="poly">-->
                            </map>
                        </div>
                    </div>
                    <div class="well">
                        <h2 runat="server" id="mapValidation" style="color: red; font-size: 2em;"></h2>
                    </div>
                </div>
            </div>
            <div class="card-footer" id="bsm">
                <div class="row">
                    <div class="col">
                        <button type="button" id="back_btn" class="btn btn-primary btn-xl ml-2" onclick="decrease_counter()" style="display:none;">Back!</button>
                    </div>
                    <div class="col">
                        <button type="button" id="continue_btn" class="btn btn-xl btn-primary ml-2" onclick="increase_counter()" style="display:none;">Continue!</button>
                    </div>
                    <Button runat="server" onclick="MakeFormSubmittable();" id="campingSubmitButton" style="display:none" onserverclick="AddCamping_ServerClick" class="btn btn-xl btn-primary btn-block mt-3">Submit</Button>
                    <input runat="server" type="checkbox" value="" id ="finishedCampingCheckbox" name="finishedCampingCheckbox" style="display:none;"/>
                </div>
            </div>
        </div>
        <div id="map-container" style="visibility: hidden;">

            <div id="temporary1">
                <!-- Image Map Generated by http://www.image-map.net/ -->
                <img id="myimage1" src="map_resources/1030_groot_0002_Livello-3.png" usemap="#image-map1">

                <map name="image-map1">
                    <area data-key='H4' target="" value="1" href="javascript:void(0);" onclick='CheckSpotAvailability(1, "B");' coords="204,46,234,69" shape="rect">
                    <area data-key='H5' target="" value="2" href="javascript:void(0);" onclick='CheckSpotAvailability(2, "B");' coords="234,49,259,69" shape="rect">
                    <area data-key='H6' target="" value="3" href="javascript:void(0);" onclick='CheckSpotAvailability(3, "B");' coords="259,51,283,81" shape="rect">
                    <area data-key='H7' target="" value="4" href="javascript:void(0);" onclick='CheckSpotAvailability(4, "B");' coords="283,60,305,90" shape="rect">
                    <area data-key='H8' target="" value="5" href="javascript:void(0);" onclick='CheckSpotAvailability(5, "B");' coords="305,70,327,98" shape="rect">
                    <area data-key='H9' target="" value="6" href="javascript:void(0);" onclick='CheckSpotAvailability(6, "B");' coords="326,83,349,108" shape="rect">
                    <area data-key='H10' target="" value="7" href="javascript:void(0);" onclick='CheckSpotAvailability(7, "B");' coords="350,91,373,121" shape="rect">
                    <area data-key='H11' target="" value="8" href="javascript:void(0);" onclick='CheckSpotAvailability(8, "B");' coords="373,100,398,131" shape="rect">
                    <area data-key='H12' target="" value="9" href="javascript:void(0);" onclick='CheckSpotAvailability(9, "B");' coords="398,115,425,141" shape="rect">
                    <area data-key='H13' target="" value="10" href="javascript:void(0);" onclick='CheckSpotAvailability(10, "B");' coords="424,128,449,157" shape="rect">
                    <area data-key='H14' target="" value="11" href="javascript:void(0);" onclick='CheckSpotAvailability(11, "B");' coords="465,151,442,181" shape="rect">
                    <area data-key='H15' target="" value="12" href="javascript:void(0);" onclick='CheckSpotAvailability(12, "B");' coords="184,99,212,125" shape="rect">
                    <area data-key='H16' target="" value="13" href="javascript:void(0);" onclick='CheckSpotAvailability(13, "B");' coords="208,121,241,145" shape="rect">
                    <area data-key='H17' target="" value="14" href="javascript:void(0);" onclick='CheckSpotAvailability(14, "B");' coords="242,133,267,161" shape="rect">
                    <area data-key='H18' target="" value="15" href="javascript:void(0);" onclick='CheckSpotAvailability(15, "B");' coords="271,150,302,175" shape="rect">
                    <area data-key='H19' target="" value="16" href="javascript:void(0);" onclick='CheckSpotAvailability(16, "B");' coords="302,161,332,189" shape="rect">
                    <area data-key='H20' target="" value="17" href="javascript:void(0);" onclick='CheckSpotAvailability(17, "B");' coords="335,175,363,201" shape="rect">
                    <area data-key='H21' target="" value="18" href="javascript:void(0);" onclick='CheckSpotAvailability(18, "B");' coords="365,184,403,204" shape="rect">
                    <area data-key='H22' target="" value="19" href="javascript:void(0);" onclick='CheckSpotAvailability(19, "B");' coords="161,184,195,203" shape="rect">
                    <area data-key='H23' target="" value="20" href="javascript:void(0);" onclick='CheckSpotAvailability(20, "B");' coords="197,181,229,199" shape="rect">
                    <area data-key='H24' target="" value="21" href="javascript:void(0);" onclick='CheckSpotAvailability(21, "B");' coords="231,182,259,207" shape="rect">
                    <area data-key='H25' target="" value="22" href="javascript:void(0);" onclick='CheckSpotAvailability(22, "B");' coords="259,190,288,215" shape="rect">
                    <area data-key='H26' target="" value="23" href="javascript:void(0);" onclick='CheckSpotAvailability(23, "B");' coords="286,201,313,227" shape="rect">
                    <area data-key='H27' target="" value="24" href="javascript:void(0);" onclick='CheckSpotAvailability(24, "B");' coords="314,214,345,243" shape="rect">
                    <area data-key='H28' target="" value="25" href="javascript:void(0);" onclick='CheckSpotAvailability(25, "B");' coords="146,229,179,263" shape="rect">
                    <area data-key='H29' target="" value="26" href="javascript:void(0);" onclick='CheckSpotAvailability(26, "B");' coords="175,251,203,289" shape="rect">
                    <area data-key='H30' target="" value="27" href="javascript:void(0);" onclick='CheckSpotAvailability(27, "B");' coords="204,268,233,307" shape="rect">
                    <area data-key='H31' target="" value="28" href="javascript:void(0);" onclick='CheckSpotAvailability(28, "B");' coords="234,286,260,319" shape="rect">
                    <area data-key='H32' target="" value="29" href="javascript:void(0);" onclick='CheckSpotAvailability(29, "B");' coords="186,305,218,331" shape="rect">
                    <area data-key='H33' target="" value="30" href="javascript:void(0);" onclick='CheckSpotAvailability(30, "B");' coords="220,315,249,345" shape="rect">
                    <area data-key='H34' target="" value="31" href="javascript:void(0);" onclick='CheckSpotAvailability(31, "B");' coords="113,348,138,375" shape="rect">
                    <area data-key='H35' target="" value="32" href="javascript:void(0);" onclick='CheckSpotAvailability(32, "B");' coords="122,377,152,400" shape="rect">
                    <area data-key='H36' target="" value="33" href="javascript:void(0);" onclick='CheckSpotAvailability(33, "B");' coords="148,393,177,419" shape="rect">
                    <area data-key='H37' target="" value="34" href="javascript:void(0);" onclick='CheckSpotAvailability(34, "B");' coords="177,403,204,431" shape="rect">
                    <area data-key='H38' target="" value="35" href="javascript:void(0);" onclick='CheckSpotAvailability(35, "B");' coords="206,411,229,444" shape="rect">
                    <area data-key='H39' target="" value="36" href="javascript:void(0);" onclick='CheckSpotAvailability(36, "B");' coords="62,437,85,472" shape="rect">
                    <area data-key='H40' target="" value="37" href="javascript:void(0);" onclick='CheckSpotAvailability(37, "B");' coords="75,422,111,444" shape="rect">
                    <area data-key='H41' target="" value="38" href="javascript:void(0);" onclick='CheckSpotAvailability(38, "B");' coords="111,415,137,436" shape="rect">
                    <area data-key='H42' target="" value="39" href="javascript:void(0);" onclick='CheckSpotAvailability(39, "B");' coords="138,417,170,445" shape="rect">
                    <area data-key='H43' target="" value="40" href="javascript:void(0);" onclick='CheckSpotAvailability(40, "B");' coords="167,428,193,456" shape="rect">
                    <area data-key='H44' target="" value="41" href="javascript:void(0);" onclick='CheckSpotAvailability(41, "B");' coords="192,436,219,469" shape="rect">
                    <area data-key='H45' target="" value="42" href="javascript:void(0);" onclick='CheckSpotAvailability(42, "B");' coords="45,490,68,534" shape="rect">
                    <area data-key='H46' target="" value="43" href="javascript:void(0);" onclick='CheckSpotAvailability(43, "B");' coords="50,534,81,563" shape="rect">
                    <area data-key='H47' target="" value="44" href="javascript:void(0);" onclick='CheckSpotAvailability(44, "B");' coords="76,552,108,578" shape="rect">
                    <area data-key='H48' target="" value="45" href="javascript:void(0);" onclick='CheckSpotAvailability(45, "B");' coords="110,559,138,582" shape="rect">
                    <area data-key='H49' target="" value="46" href="javascript:void(0);" onclick='CheckSpotAvailability(46, "B");' coords="139,561,166,586" shape="rect">
                    <area data-key='H50' target="" value="47" href="javascript:void(0);" onclick='CheckSpotAvailability(47, "B");' coords="166,563,193,585" shape="rect">
                    <area data-key='H51' target="" value="48" href="javascript:void(0);" onclick='CheckSpotAvailability(48, "B");' coords="193,564,226,587" shape="rect">
                    <area data-key='H52' target="" value="49" href="javascript:void(0);" onclick='CheckSpotAvailability(49, "B");' coords="7,622,27,662" shape="rect">
                    <area data-key='H53' target="" value="50" href="javascript:void(0);" onclick='CheckSpotAvailability(50, "B");' coords="7,596,46,623" shape="rect">
                    <area data-key='H54' target="" value="51" href="javascript:void(0);" onclick='CheckSpotAvailability(51, "B");' coords="36,586,76,605" shape="rect">
                    <area data-key='H55' target="" value="52" href="javascript:void(0);" onclick='CheckSpotAvailability(52, "B");' coords="75,583,108,605" shape="rect">
                    <area data-key='H56' target="" value="53" href="javascript:void(0);" onclick='CheckSpotAvailability(53, "B");' coords="108,585,140,606" shape="rect">
                    <area data-key='H57' target="" value="54" href="javascript:void(0);" onclick='CheckSpotAvailability(54, "B");' coords="140,584,166,610" shape="rect">
                    <area data-key='H58' target="" value="55" href="javascript:void(0);" onclick='CheckSpotAvailability(55, "B");' coords="166,589,194,609" shape="rect">
                    <area data-key='H59' target="" value="56" href="javascript:void(0);" onclick='CheckSpotAvailability(56, "B");' coords="195,589,226,614" shape="rect">
                    <area data-key='H60' target="" value="57" href="javascript:void(0);" onclick='CheckSpotAvailability(57, "B");' coords="28,690,58,715" shape="rect">
                    <area data-key='H61' target="" value="58" href="javascript:void(0);" onclick='CheckSpotAvailability(58, "B");' coords="60,699,91,717" shape="rect">
                    <area data-key='H62' target="" value="59" href="javascript:void(0);" onclick='CheckSpotAvailability(59, "B");' coords="91,690,127,714" shape="rect">
                    <area data-key='H63' target="" value="60" href="javascript:void(0);" onclick='CheckSpotAvailability(60, "B");' coords="126,689,159,708" shape="rect">
                    <area data-key='H64' target="" value="61" href="javascript:void(0);" onclick='CheckSpotAvailability(61, "B");' coords="160,687,191,710" shape="rect">
                    <area data-key='H65' target="" value="62" href="javascript:void(0);" onclick='CheckSpotAvailability(62, "B");' coords="193,688,221,710" shape="rect">
                    <area data-key='H66' target="" value="63" href="javascript:void(0);" onclick='CheckSpotAvailability(63, "B");' coords="221,690,250,715" shape="rect">
                    <area data-key='H67' target="" value="64" href="javascript:void(0);" onclick='CheckSpotAvailability(64, "B");' coords="82,719,113,741" shape="rect">
                    <area data-key='H68' target="" value="65" href="javascript:void(0);" onclick='CheckSpotAvailability(65, "B");' coords="113,717,146,738" shape="rect">
                    <area data-key='H69' target="" value="66" href="javascript:void(0);" onclick='CheckSpotAvailability(66, "B");' coords="147,711,180,737" shape="rect">
                    <area data-key='H70' target="" value="67" href="javascript:void(0);" onclick='CheckSpotAvailability(67, "B");' coords="179,714,215,738" shape="rect">
                    <area data-key='H71' target="" value="68" href="javascript:void(0);" onclick='CheckSpotAvailability(68, "B");' coords="216,715,247,744" shape="rect">
                    <area data-key='H72' target="" value="69" href="javascript:void(0);" onclick='CheckSpotAvailability(69, "B");' coords="20,793,44,815" shape="rect">
                    <area data-key='H73' target="" value="70" href="javascript:void(0);" onclick='CheckSpotAvailability(70, "B");' coords="40,809,59,835" shape="rect">
                    <area data-key='H74' target="" value="71" href="javascript:void(0);" onclick='CheckSpotAvailability(71, "B");' coords="58,819,82,838" shape="rect">
                    <area data-key='H75' target="" value="72" href="javascript:void(0);" onclick='CheckSpotAvailability(72, "B");' coords="81,815,109,836" shape="rect">
                    <area data-key='H76' target="" value="73" href="javascript:void(0);" onclick='CheckSpotAvailability(73, "B");' coords="110,814,132,831" shape="rect">
                    <area data-key='H77' target="" value="74" href="javascript:void(0);" onclick='CheckSpotAvailability(74, "B");'coords="132,809,161,831" shape="rect">
                    <area data-key='H78' target="" value="75" href="javascript:void(0);" onclick='CheckSpotAvailability(75, "B");'coords="160,807,187,829" shape="rect">
                    <area data-key='H79' target="" value="76" href="javascript:void(0);" onclick='CheckSpotAvailability(76, "B");' coords="61,871,82,900" shape="rect">
                    <area data-key='H80' target="" value="77" href="javascript:void(0);" onclick='CheckSpotAvailability(77, "B");' coords="71,854,96,879" shape="rect">
                    <area data-key='H81' target="" value="78" href="javascript:void(0);" onclick='CheckSpotAvailability(78, "B");' coords="91,844,118,862" shape="rect">
                    <area data-key='H82' target="" value="79" href="javascript:void(0);" onclick='CheckSpotAvailability(79, "B");' coords="114,838,142,855" shape="rect">
                    <area data-key='H83' target="" value="80" href="javascript:void(0);" onclick='CheckSpotAvailability(80, "B");' coords="140,835,163,857" shape="rect">
                    <area data-key='H84' target="" value="81" href="javascript:void(0);" onclick='CheckSpotAvailability(81, "B");' coords="164,837,186,859" shape="rect">
                    <area data-key='H85' target="" value="82" href="javascript:void(0);" onclick='CheckSpotAvailability(82, "B");' coords="54,913,79,939" shape="rect">
                    <area data-key='H86' target="" value="83" href="javascript:void(0);" onclick='CheckSpotAvailability(83, "B");' coords="75,931,102,952" shape="rect">
                    <area data-key='H87' target="" value="84" href="javascript:void(0);" onclick='CheckSpotAvailability(84, "B");' coords="103,935,127,957" shape="rect">
                    <area data-key='H88' target="" value="85" href="javascript:void(0);" onclick='CheckSpotAvailability(85, "B");' coords="129,932,159,951" shape="rect">
                </map>
            </div>

            <div id="temporary2">
                <!-- Image Map Generated by http://www.image-map.net/ -->
                <img id="myimage2" src="map_resources/1030_groot_0003_Livello-4.png" usemap="#image-map2">

                <map name="image-map2">
                    <area data-key='I4' target=""  value="1"  href="javascript:void(0);" onclick='CheckSpotAvailability(1, "A");' coords="24,312,53,335" shape="rect">
                    <area data-key='I5' target=""  value="2"  href="javascript:void(0);" onclick='CheckSpotAvailability(2, "A");' coords="81,309,104,337" shape="rect">
                    <area data-key='I6' target=""  value="3"  href="javascript:void(0);" onclick='CheckSpotAvailability(3, "A");' coords="126,321,151,348" shape="rect">
                    <area data-key='I7' target=""  value="4"  href="javascript:void(0);" onclick='CheckSpotAvailability(4, "A");' coords="177,321,210,341" shape="rect">
                    <area data-key='I8' target=""  value="5"  href="javascript:void(0);" onclick='CheckSpotAvailability(5, "A");' coords="227,319,252,341" shape="rect">
                    <area data-key='I9' target=""  value="6"  href="javascript:void(0);" onclick='CheckSpotAvailability(6, "A");' coords="274,320,301,347" shape="rect">
                    <area data-key='I10' target=""  value="7"  href="javascript:void(0);" onclick='CheckSpotAvailability(7, "A");' coords="344,314,370,338" shape="rect">
                    <area data-key='I11' target=""  value="8"  href="javascript:void(0);" onclick='CheckSpotAvailability(8, "A");' coords="385,285,410,315" shape="rect">
                    <area data-key='I12' target=""  value="9"  href="javascript:void(0);" onclick='CheckSpotAvailability(9, "A");' coords="434,299,465,325" shape="rect">
                    <area data-key='I13' target=""  value="10"  href="javascript:void(0);" onclick='CheckSpotAvailability(10, "A");' coords="478,270,501,299" shape="rect">
                    <area data-key='I14' target=""  value="11"  href="javascript:void(0);" onclick='CheckSpotAvailability(11, "A");' coords="512,295,540,326" shape="rect">
                    <area data-key='I15' target=""  value="12"  href="javascript:void(0);" onclick='CheckSpotAvailability(12, "A");' coords="555,280,583,306" shape="rect">
                    <area data-key='I16' target=""  value="13"  href="javascript:void(0);" onclick='CheckSpotAvailability(13, "A");' coords="619,259,646,290" shape="rect">
                    <area data-key='I17' target=""  value="14"  href="javascript:void(0);" onclick='CheckSpotAvailability(14, "A");' coords="646,176,675,208" shape="rect">
                    <area data-key='I18' target=""  value="15"  href="javascript:void(0);" onclick='CheckSpotAvailability(15, "A");' coords="600,197,628,221" shape="rect">
                    <area data-key='I19' target=""  value="16"  href="javascript:void(0);" onclick='CheckSpotAvailability(16, "A");' coords="549,207,575,230" shape="rect">
                    <area data-key='I20' target=""  value="17"  href="javascript:void(0);" onclick='CheckSpotAvailability(17, "A");' coords="507,186,537,214" shape="rect">
                    <area data-key='I21' target=""  value="18"  href="javascript:void(0);" onclick='CheckSpotAvailability(18, "A");' coords="447,204,479,233" shape="rect">
                    <area data-key='I22' target=""  value="19"  href="javascript:void(0);" onclick='CheckSpotAvailability(19, "A");' coords="392,216,422,249" shape="rect">
                    <area data-key='I23' target=""  value="20"  href="javascript:void(0);" onclick='CheckSpotAvailability(20, "A");' coords="351,239,379,266" shape="rect">
                    <area data-key='I24' target=""  value="21"  href="javascript:void(0);" onclick='CheckSpotAvailability(21, "A");' coords="301,255,331,283" shape="rect">
                    <area data-key='I25' target=""  value="22"  href="javascript:void(0);" onclick='CheckSpotAvailability(22, "A");' coords="249,264,274,290" shape="rect">
                    <area data-key='I26' target=""  value="23"  href="javascript:void(0);" onclick='CheckSpotAvailability(23, "A");' coords="205,266,233,291" shape="rect">
                    <area data-key='I27' target=""  value="24"  href="javascript:void(0);" onclick='CheckSpotAvailability(24, "A");' coords="161,261,192,294" shape="rect">
                    <area data-key='I28' target=""  value="25"  href="javascript:void(0);" onclick='CheckSpotAvailability(25, "A");' coords="100,246,132,278" shape="rect">
                    <area data-key='I29' target=""  value="26"  href="javascript:void(0);" onclick='CheckSpotAvailability(26, "A");' coords="100,199,135,231" shape="rect">
                    <area data-key='I30' target=""  value="27"  href="javascript:void(0);" onclick='CheckSpotAvailability(27, "A");' coords="151,203,180,237" shape="rect">
                    <area data-key='I31' target=""  value="28"  href="javascript:void(0);" onclick='CheckSpotAvailability(28, "A");' coords="195,217,231,247" shape="rect">
                    <area data-key='I32' target=""  value="29"  href="javascript:void(0);" onclick='CheckSpotAvailability(29, "A");' coords="242,206,269,233" shape="rect">
                    <area data-key='I33' target=""  value="30"  href="javascript:void(0);" onclick='CheckSpotAvailability(30, "A");' coords="291,192,316,217" shape="rect">
                    <area data-key='I34' target=""  value="31"  href="javascript:void(0);" onclick='CheckSpotAvailability(31, "A");' coords="347,178,375,201" shape="rect">
                    <area data-key='I35' target=""  value="32"  href="javascript:void(0);" onclick='CheckSpotAvailability(32, "A");' coords="416,167,448,194" shape="rect">
                    <area data-key='I36' target=""  value="33"  href="javascript:void(0);" onclick='CheckSpotAvailability(33, "A");' coords="468,150,498,178" shape="rect">
                    <area data-key='I37' target=""  value="34"  href="javascript:void(0);" onclick='CheckSpotAvailability(34, "A");' coords="440,104,478,132" shape="rect">
                    <area data-key='I38' target=""  value="35"  href="javascript:void(0);" onclick='CheckSpotAvailability(35, "A");' coords="389,108,424,141" shape="rect">
                    <area data-key='I39' target=""  value="36"  href="javascript:void(0);" onclick='CheckSpotAvailability(36, "A");' coords="342,119,369,146" shape="rect">
                    <area data-key='I40' target=""  value="37"  href="javascript:void(0);" onclick='CheckSpotAvailability(37, "A");' coords="284,143,309,171" shape="rect">
                    <area data-key='I41' target=""  value="38"  href="javascript:void(0);" onclick='CheckSpotAvailability(38, "A");' coords="226,150,257,185" shape="rect">
                    <area data-key='I42' target=""  value="39"  href="javascript:void(0);" onclick='CheckSpotAvailability(39, "A");' coords="173,152,200,183" shape="rect">
                    <area data-key='I43' target=""  value="40"  href="javascript:void(0);" onclick='CheckSpotAvailability(40, "A");' coords="119,144,149,177" shape="rect">
                    <area data-key='I44' target=""  value="41"  href="javascript:void(0);" onclick='CheckSpotAvailability(41, "A");' coords="120,89,151,125" shape="rect">
                    <area data-key='I45' target=""  value="42"  href="javascript:void(0);" onclick='CheckSpotAvailability(42, "A");' coords="173,101,204,131" shape="rect">
                    <area data-key='I46' target=""  value="43"  href="javascript:void(0);" onclick='CheckSpotAvailability(43, "A");' coords="230,91,257,123" shape="rect">
                    <area data-key='I47' target=""  value="44"  href="javascript:void(0);" onclick='CheckSpotAvailability(44, "A");' coords="277,80,307,115" shape="rect">
                    <area data-key='I48' target=""  value="45"  href="javascript:void(0);" onclick='CheckSpotAvailability(45, "A");' coords="328,66,357,92" shape="rect">
                    <area data-key='I49' target=""  value="46"  href="javascript:void(0);" onclick='CheckSpotAvailability(46, "A");' coords="384,44,416,77" shape="rect">
                    <area data-key='I50' target=""  value="47"  href="javascript:void(0);" onclick='CheckSpotAvailability(47, "A");' coords="334,22,366,54" shape="rect">
                    <area data-key='I51' target=""  value="48"  href="javascript:void(0);" onclick='CheckSpotAvailability(48, "A");' coords="260,32,296,59" shape="rect">
                    <area data-key='I52' target=""  value="49"  href="javascript:void(0);" onclick='CheckSpotAvailability(49, "A");' coords="197,48,228,77" shape="rect">
                    <area data-key='I53' target=""  value="50"  href="javascript:void(0);" onclick='CheckSpotAvailability(50, "A");' coords="144,45,176,81" shape="rect">
                </map>


            </div>







            <!--<div id="temporary3">
                <img id="myimage3" src="map_resources/1030_groot_0004_Livello-5.png" usemap="#image-map3">

                <map name="image-map3">
                    <area data-key='J4' target="" alt="campc1" title="campc1" href="javascript:void(0);" coords="169,43,142,8" shape="rect">
                    <area data-key='J5' target="" alt="campc2" title="campc2" href="javascript:void(0);" coords="133,45,154,73" shape="rect">
                    <area data-key='J6' target="" alt="campc3" title="campc3" href="javascript:void(0);" coords="120,79,139,108" shape="rect">
                    <area data-key='J7' target="" alt="campc4" title="campc4" href="javascript:void(0);" coords="107,113,126,143" shape="rect">
                    <area data-key='J8' target="" alt="campc5" title="campc5" href="javascript:void(0);" coords="93,147,111,180" shape="rect">
                    <area data-key='J9' target="" alt="campc16" title="campc16" href="javascript:void(0);" coords="97,209,124,237" shape="rect">
                    <area data-key='J10' target="" alt="campc17" title="campc17" href="javascript:void(0);" coords="148,227,173,253" shape="rect">
                    <area data-key='J11' target="" alt="campc18" title="campc18" href="javascript:void(0);" coords="194,246,219,270" shape="rect">
                    <area data-key='J12' target="" alt="campc19" title="campc19" href="javascript:void(0);" coords="171,294,203,323" shape="rect">
                    <area data-key='J13' target="" alt="campc20" title="campc20" href="javascript:void(0);" coords="128,276,158,304" shape="rect">
                    <area data-key='J14' target="" alt="campc21" title="campc21" href="javascript:void(0);" coords="81,257,108,286" shape="rect">
                    <area data-key='J15' target="" alt="campc22" title="campc22" href="javascript:void(0);" coords="56,308,83,336" shape="rect">
                    <area data-key='J16' target="" alt="campc23" title="campc23" href="javascript:void(0);" coords="105,330,130,357" shape="rect">
                    <area data-key='J17' target="" alt="campc24" title="campc24" href="javascript:void(0);" coords="146,347,181,377" shape="rect">
                    <area data-key='J18' target="" alt="campc25" title="campc25" href="javascript:void(0);" coords="126,387,158,415" shape="rect">
                    <area data-key='J19' target="" alt="campc26" title="campc26" href="javascript:void(0);" coords="85,368,111,395" shape="rect">
                    <area data-key='J20' target="" alt="campc27" title="campc27" href="javascript:void(0);" coords="41,352,70,377" shape="rect">
                    <area data-key='J21' target="" alt="campd1" title="campd1" href="javascript:void(0);" coords="237,47,263,81" shape="rect">
                    <area data-key='J22' target="" alt="campd3" title="campd3" href="javascript:void(0);" coords="281,64,303,98" shape="rect">
                    <area data-key='J23' target="" alt="campd4" title="campd4" href="javascript:void(0);" coords="270,92,289,125" shape="rect">
                    <area data-key='J24' target="" alt="campd5" title="campd5" href="javascript:void(0);" coords="261,123,280,154" shape="rect">
                    <area data-key='J25' target="" alt="campd6" title="campd6" href="javascript:void(0);" coords="248,151,271,180" shape="rect">
                    <area data-key='J26' target="" alt="campd8" title="campd8" href="javascript:void(0);" coords="239,181,260,212" shape="rect">
                    <area data-key='J27' target="" alt="campd9" title="campd9" href="javascript:void(0);" coords="229,205,251,242" shape="rect">
                    <area data-key='J28' target="" alt="campd12" title="campd12" href="javascript:void(0);" coords="191,188,211,220" shape="rect">
                    <area data-key='J29' target="" alt="campd13" title="campd13" href="javascript:void(0);" coords="202,151,224,189" shape="rect">
                    <area data-key='J30' target="" alt="campd14" title="campd14" href="javascript:void(0);" coords="219,108,239,146" shape="rect">
                    <area data-key='J31' target="" alt="campd15" title="campd15" href="javascript:void(0);" coords="228,75,252,112" shape="rect">
                    <area data-key='J32' target="" alt="campd18" title="campd18" href="javascript:void(0);" coords="207,61,223,90" shape="rect">
                    <area data-key='J33' target="" alt="campd19" title="campd19" href="javascript:void(0);" coords="198,85,215,114" shape="rect">
                    <area data-key='J34' target="" alt="campd20" title="campd20" href="javascript:void(0);" coords="190,107,210,136" shape="rect">
                    <area data-key='J35' target="" alt="campd21" title="campd21" href="javascript:void(0);" coords="174,142,198,180" shape="rect">
                    <area data-key='J36' target="" alt="campd22" title="campd22" href="javascript:void(0);" coords="164,176,184,213" shape="rect">
                    <area data-key='J37' target="" alt="campd27" title="campd27" href="javascript:void(0);" coords="122,159,141,193" shape="rect">
                    <area data-key='J38' target="" alt="campd28" title="campd28" href="javascript:void(0);" coords="135,125,154,162" shape="rect">
                    <area data-key='J39' target="" alt="campd34" title="campd34" href="javascript:void(0);" coords="213,36,235,69" shape="rect">
                </map>


            </div>-->
            <!--<div id="temporary4">
                <img id="myimage4" src="map_resources/1030_groot_0005_Livello-6.png" usemap="#image-map4">

                <map name="image-map4">
                    <area data-key='A4' target="" alt="Area_F_Camp_1" title="Area_F_Camp_1" href="javascript:void(0);" coords="23,29,48,46" shape="rect">
                    <area data-key='A5' target="" alt="Area_F_Camp_2" title="Area_F_Camp_2" href="javascript:void(0);" coords="52,41,77,62" shape="rect">
                    <area data-key='A6' target="" alt="Area_F_Camp_3" title="Area_F_Camp_3" href="javascript:void(0);" coords="107,97,87,68" shape="rect">
                    <area data-key='A7' target="" alt="Area_F_Camp_4" title="Area_F_Camp_4" href="javascript:void(0);" coords="142,74,168,93" shape="rect">
                    <area data-key='A8' target="" alt="Area_F_Camp_5" title="Area_F_Camp_5" href="javascript:void(0);" coords="172,82,197,100" shape="rect">
                    <area data-key='A9' target="" alt="Area_F_Camp_6" title="Area_F_Camp_6" href="javascript:void(0);" coords="217,92,237,113" shape="rect">
                    <area data-key='A10' target="" alt="Area_F_Camp_7" title="Area_F_Camp_7" href="javascript:void(0);" coords="260,103,282,124" shape="rect">
                    <area data-key='A11' target="" alt="Area_F_Camp_8" title="Area_F_Camp_8" href="javascript:void(0);" coords="302,116,326,137" shape="rect">
                    <area data-key='A12' target="" alt="Area_F_Camp_9" title="Area_F_Camp_9" href="javascript:void(0);" coords="286,162,308,193" shape="rect">
                    <area data-key='A13' target="" alt="Area_F_Camp_10" title="Area_F_Camp_10" href="javascript:void(0);" coords="314,173,333,206" shape="rect">
                    <area data-key='A14' target="" alt="Area_F_Camp_11" title="Area_F_Camp_11" href="javascript:void(0);" coords="221,146,202,119" shape="rect">
                    <area data-key='A15' target="" alt="Area_F_Camp_12" title="Area_F_Camp_12" href="javascript:void(0);" coords="48,133,69,152" shape="rect">
                    <area data-key='A16' target="" alt="Area_F_Camp_13" title="Area_F_Camp_13" href="javascript:void(0);" coords="74,135,98,152" shape="rect">
                    <area data-key='A17' target="" alt="Area_F_Camp_14" title="Area_F_Camp_14" href="javascript:void(0);" coords="116,143,135,170" shape="rect">
                    <area data-key='A18' target="" alt="Area_F_Camp_15" title="Area_F_Camp_15" href="javascript:void(0);" coords="164,147,184,176" shape="rect">
                    <area data-key='A19' target="" alt="Area_F_Camp_16" title="Area_F_Camp_16" href="javascript:void(0);" coords="192,155,210,188" shape="rect">
                    <area data-key='A20' target="" alt="Area_F_Camp_17" title="Area_F_Camp_17" href="javascript:void(0);" coords="80,181,54,163" shape="rect">
                    <area data-key='A21' target="" alt="Area_F_Camp_18" title="Area_F_Camp_18" href="javascript:void(0);" coords="129,198,109,173" shape="rect">
                    <area data-key='A22' target="" alt="Area_F_Camp_19" title="Area_F_Camp_19" href="javascript:void(0);" coords="156,176,173,196" shape="rect">
                    <area data-key='A23' target="" alt="Area_F_Camp_20" title="Area_F_Camp_20" href="javascript:void(0);" coords="180,190,199,214" shape="rect">
                    <area data-key='A24' target="" alt="Area_F_Camp_21" title="Area_F_Camp_21" href="javascript:void(0);" coords="267,200,286,230" shape="rect">
                    <area data-key='A25' target="" alt="Area_F_Camp_22" title="Area_F_Camp_22" href="javascript:void(0);" coords="290,215,309,243" shape="rect">
                    <area data-key='A26' target="" alt="Area_F_Camp_23" title="Area_F_Camp_23" href="javascript:void(0);" coords="82,191,98,223" shape="rect">
                    <area data-key='A27' target="" alt="Area_F_Camp_24" title="Area_F_Camp_24" href="javascript:void(0);" coords="103,202,120,226" shape="rect">
                    <area data-key='A28' target="" alt="Area_F_Camp_25" title="Area_F_Camp_25" href="javascript:void(0);" coords="148,198,164,222" shape="rect">
                    <area data-key='A29' target="" alt="Area_F_Camp_26" title="Area_F_Camp_26" href="javascript:void(0);" coords="171,217,186,238" shape="rect">
                    <area data-key='A30' target="" alt="Area_F_Camp_27" title="Area_F_Camp_27" href="javascript:void(0);" coords="256,228,272,255" shape="rect">
                    <area data-key='A31' target="" alt="Area_F_Camp_28" title="Area_F_Camp_28" href="javascript:void(0);" coords="277,240,293,266" shape="rect">
                    <area data-key='A32' target="" alt="Area_F_Camp_29" title="Area_F_Camp_29" href="javascript:void(0);" coords="57,232,78,252" shape="rect">
                    <area data-key='A33' target="" alt="Area_F_Camp_30" title="Area_F_Camp_30" href="javascript:void(0);" coords="98,228,114,259" shape="rect">
                    <area data-key='A34' target="" alt="Area_F_Camp_31" title="Area_F_Camp_31" href="javascript:void(0);" coords="138,221,152,252" shape="rect">
                    <area data-key='A35' target="" alt="Area_F_Camp_32" title="Area_F_Camp_32" href="javascript:void(0);" coords="163,238,180,259" shape="rect">
                    <area data-key='A36' target="" alt="Area_F_Camp_33" title="Area_F_Camp_33" href="javascript:void(0);" coords="242,256,260,278" shape="rect">
                    <area data-key='A37' target="" alt="Area_F_Camp_34" title="Area_F_Camp_34" href="javascript:void(0);" coords="265,266,282,294" shape="rect">
                    <area data-key='A38' target="" alt="Area_F_Camp_35" title="Area_F_Camp_35" href="javascript:void(0);" coords="66,258,95,274" shape="rect">
                    <area data-key='A39' target="" alt="Area_F_Camp_36" title="Area_F_Camp_36" href="javascript:void(0);" coords="125,251,142,281" shape="rect">
                    <area data-key='A40' target="" alt="Area_F_Camp_37" title="Area_F_Camp_37" href="javascript:void(0);" coords="152,266,173,298" shape="rect">
                    <area data-key='A41' target="" alt="Area_F_Camp_38" title="Area_F_Camp_38" href="javascript:void(0);" coords="227,283,243,312" shape="rect">
                    <area data-key='A42' target="" alt="Area_F_Camp_39" title="Area_F_Camp_39" href="javascript:void(0);" coords="269,325,253,293" shape="rect">
                    <area data-key='A43' target="" alt="Area_F_Camp_40" title="Area_F_Camp_40" href="javascript:void(0);" coords="306,322,324,351" shape="rect">
                    <area data-key='A44' target="" alt="Area_F_Camp_41" title="Area_F_Camp_41" href="javascript:void(0);" coords="113,281,131,306" shape="rect">
                    <area data-key='A45' target="" alt="Area_F_Camp_42" title="Area_F_Camp_42" href="javascript:void(0);" coords="142,300,159,323" shape="rect">
                    <area data-key='A46' target="" alt="Area_F_Camp_43" title="Area_F_Camp_43" href="javascript:void(0);" coords="205,316,228,336" shape="rect">
                    <area data-key='A47' target="" alt="Area_F_Camp_44" title="Area_F_Camp_44" href="javascript:void(0);" coords="268,335,288,354" shape="rect">
                    <area data-key='A48' target="" alt="Area_F_Camp_45" title="Area_F_Camp_45" href="javascript:void(0);" coords="60,298,74,318" shape="rect">
                    <area data-key='A49' target="" alt="Area_F_Camp_46" title="Area_F_Camp_46" href="javascript:void(0);" coords="132,324,151,344" shape="rect">
                    <area data-key='A50' target="" alt="Area_F_Camp_47" title="Area_F_Camp_47" href="javascript:void(0);" coords="226,367,208,340" shape="rect">
                    <area data-key='A51' target="" alt="Area_F_Camp_48" title="Area_F_Camp_48" href="javascript:void(0);" coords="50,320,66,338" shape="rect">
                    <area data-key='A52' target="" alt="Area_F_Camp_49" title="Area_F_Camp_49" href="javascript:void(0);" coords="101,323,117,352" shape="rect">
                    <area data-key='A53' target="" alt="Area_F_Camp_50" title="Area_F_Camp_50" href="javascript:void(0);" coords="123,344,140,370" shape="rect">
                    <area data-key='A54' target="" alt="Area_F_Camp_51" title="Area_F_Camp_51" href="javascript:void(0);" coords="198,371,216,390" shape="rect">
                    <area data-key='A55' target="" alt="Area_F_Camp_52" title="Area_F_Camp_52" href="javascript:void(0);" coords="76,363,59,347" shape="rect">
                    <area data-key='A56' target="" alt="Area_F_Camp_53" title="Area_F_Camp_53" href="javascript:void(0);" coords="90,353,106,374" shape="rect">
                    <area data-key='A57' target="" alt="Area_F_Camp_54" title="Area_F_Camp_54" href="javascript:void(0);" coords="131,378,151,398" shape="rect">
                    <area data-key='A58' target="" alt="Area_F_Camp_55" title="Area_F_Camp_55" href="javascript:void(0);" coords="174,395,194,411" shape="">
                </map>


            </div>-->
            <!--<div id="temporary5">
                <img id="myimage5" src="map_resources/1030_groot_0006_Livello-7.png" usemap="#image-map5">

                <map name="image-map5">
                    <area data-key='B4' target="" alt="Area_G_Camp_1" title="Area_G_Camp_1" href="javascript:void(0);" coords="192,22,209,47" shape="rect">
                    <area data-key='B5' target="" alt="Area_G_Camp_2" title="Area_G_Camp_2" href="javascript:void(0);" coords="224,31,247,54" shape="rect">
                    <area data-key='B6' target="" alt="Area_G_Camp_3" title="Area_G_Camp_3" href="javascript:void(0);" coords="263,43,281,61" shape="rect">
                    <area data-key='B7' target="" alt="Area_G_Camp_4" title="Area_G_Camp_4" href="javascript:void(0);" coords="311,54,333,76" shape="rect">
                    <area data-key='B8' target="" alt="Area_G_Camp_5" title="Area_G_Camp_5" href="javascript:void(0);" coords="365,74,396,99" shape="rect">
                    <area data-key='B9' target="" alt="Area_G_Camp_6" title="Area_G_Camp_6" href="javascript:void(0);" coords="411,94,426,121" shape="rect">
                    <area data-key='B10' target="" alt="Area_G_Camp_7" title="Area_G_Camp_7" href="javascript:void(0);" coords="444,93,466,113" shape="rect">
                    <area data-key='B11' target="" alt="Area_G_Camp_8" title="Area_G_Camp_8" href="javascript:void(0);" coords="472,101,490,123" shape="rect">
                    <area data-key='B12' target="" alt="Area_G_Camp_9" title="Area_G_Camp_9" href="javascript:void(0);" coords="495,127,517,152" shape="rect">
                    <area data-key='B13' target="" alt="Area_G_Camp_10" title="Area_G_Camp_10" href="javascript:void(0);" coords="271,86,290,106" shape="rect">
                    <area data-key='B14' target="" alt="Area_G_Camp_11" title="Area_G_Camp_11" href="javascript:void(0);" coords="295,84,316,103" shape="rect">
                    <area data-key='B15' target="" alt="Area_G_Camp_12" title="Area_G_Camp_12" href="javascript:void(0);" coords="369,117,385,139" shape="rect">
                    <area data-key='B16' target="" alt="Area_G_Camp_13" title="Area_G_Camp_13" href="javascript:void(0);" coords="397,123,414,164" shape="rect">
                    <area data-key='B17' target="" alt="Area_G_Camp_14" title="Area_G_Camp_14" href="javascript:void(0);" coords="177,78,194,94" shape="rect">
                    <area data-key='B18' target="" alt="Area_G_Camp_15" title="Area_G_Camp_15" href="javascript:void(0);" coords="199,87,218,104" shape="rect">
                    <area data-key='B19' target="" alt="Area_G_Camp_16" title="Area_G_Camp_16" href="javascript:void(0);" coords="229,118,254,137" shape="rect">
                    <area data-key='B20' target="" alt="Area_G_Camp_17" title="Area_G_Camp_17" href="javascript:void(0);" coords="279,110,298,127" shape="rect">
                    <area data-key='B21' target="" alt="Area_G_Camp_18" title="Area_G_Camp_18" href="javascript:void(0);" coords="289,133,303,149" shape="rect">
                    <area data-key='B22' target="" alt="Area_G_Camp_19" title="Area_G_Camp_19" href="javascript:void(0);" coords="307,120,324,142" shape="rect">
                    <area data-key='B23' target="" alt="Area_G_Camp_20" title="Area_G_Camp_20" href="javascript:void(0);" coords="368,143,384,163" shape="rect">
                    <area data-key='B24' target="" alt="Area_G_Camp_21" title="Area_G_Camp_21" href="javascript:void(0);" coords="507,179,524,205" shape="rect">
                    <area data-key='B25' target="" alt="Area_G_Camp_22" title="Area_G_Camp_22" href="javascript:void(0);" coords="171,138,190,161" shape="rect">
                    <area data-key='B26' target="" alt="Area_G_Camp_23" title="Area_G_Camp_23" href="javascript:void(0);" coords="215,160,240,180" shape="rect">
                    <area data-key='B27' target="" alt="Area_G_Camp_24" title="Area_G_Camp_24" href="javascript:void(0);" coords="253,162,273,183" shape="rect">
                    <area data-key='B28' target="" alt="Area_G_Camp_25" title="Area_G_Camp_25" href="javascript:void(0);" coords="509,209,524,220" shape="rect">
                    <area data-key='B29' target="" alt="Area_G_Camp_26" title="Area_G_Camp_26" href="javascript:void(0);" coords="509,226,527,237" shape="rect">
                    <area data-key='B30' target="" alt="Area_G_Camp_27" title="Area_G_Camp_27" href="javascript:void(0);" coords="140,149,156,168" shape="rect">
                    <area data-key='B31' target="" alt="Area_G_Camp_28" title="Area_G_Camp_28" href="javascript:void(0);" coords="239,181,259,201" shape="rect">
                    <area data-key='B32' target="" alt="Area_G_Camp_29" title="Area_G_Camp_29" href="javascript:void(0);" coords="508,267,529,280" shape="rect">
                    <area data-key='B33' target="" alt="Area_G_Camp_30" title="Area_G_Camp_30" href="javascript:void(0);" coords="510,288,527,298" shape="rect">
                    <area data-key='B34' target="" alt="Area_G_Camp_31" title="Area_G_Camp_31" href="javascript:void(0);" coords="120,204,138,221" shape="rect">
                    <area data-key='B35' target="" alt="Area_G_Camp_32" title="Area_G_Camp_32" href="javascript:void(0);" coords="149,213,170,237" shape="rect">
                    <area data-key='B36' target="" alt="Area_G_Camp_33" title="Area_G_Camp_33" href="javascript:void(0);" coords="180,236,204,253" shape="rect">
                    <area data-key='B37' target="" alt="Area_G_Camp_34" title="Area_G_Camp_34" href="javascript:void(0);" coords="503,316,519,348" shape="rect">
                    <area data-key='B38' target="" alt="Area_G_Camp_35" title="Area_G_Camp_35" href="javascript:void(0);" coords="100,226,120,244" shape="rect">
                    <area data-key='B39' target="" alt="Area_G_Camp_36" title="Area_G_Camp_36" href="javascript:void(0);" coords="131,237,151,255" shape="rect">
                    <area data-key='B40' target="" alt="Area_G_Camp_37" title="Area_G_Camp_37" href="javascript:void(0);" coords="159,253,175,275" shape="rect">
                    <area data-key='B41' target="" alt="Area_G_Camp_38" title="Area_G_Camp_38" href="javascript:void(0);" coords="193,263,213,283" shape="rect">
                    <area data-key='B42' target="" alt="Area_G_Camp_39" title="Area_G_Camp_39" href="javascript:void(0);" coords="214,285,231,306" shape="rect">
                    <area data-key='B43' target="" alt="Area_G_Camp_40" title="Area_G_Camp_40" href="javascript:void(0);" coords="462,330,485,353" shape="rect">
                    <area data-key='B44' target="" alt="Area_G_Camp_41" title="Area_G_Camp_41" href="javascript:void(0);" coords="98,246,113,262" shape="rect">
                    <area data-key='B45' target="" alt="Area_G_Camp_42" title="Area_G_Camp_42" href="javascript:void(0);" coords="171,284,188,296" shape="rect">
                    <area data-key='B46' target="" alt="Area_G_Camp_43" title="Area_G_Camp_43" href="javascript:void(0);" coords="184,299,199,310" shape="rect">
                    <area data-key='B47' target="" alt="Area_G_Camp_44" title="Area_G_Camp_44" href="javascript:void(0);" coords="203,312,223,328" shape="rect">
                    <area data-key='B48' target="" alt="Area_G_Camp_45" title="Area_G_Camp_45" href="javascript:void(0);" coords="426,352,445,372" shape="rect">
                    <area data-key='B49' target="" alt="Area_G_Camp_46" title="Area_G_Camp_46" href="javascript:void(0);" coords="451,361,470,383" shape="rect">
                    <area data-key='B50' target="" alt="Area_G_Camp_47" title="Area_G_Camp_47" href="javascript:void(0);" coords="474,385,490,408" shape="rect">
                    <area data-key='B51' target="" alt="Area_G_Camp_48" title="Area_G_Camp_48" href="javascript:void(0);" coords="89,264,105,284" shape="rect">
                    <area data-key='B52' target="" alt="Area_G_Camp_49" title="Area_G_Camp_49" href="javascript:void(0);" coords="73,287,88,312" shape="rect">
                    <area data-key='B53' target="" alt="Area_G_Camp_50" title="Area_G_Camp_50" href="javascript:void(0);" coords="454,423,469,452" shape="rect">
                    <area data-key='B54' target="" alt="Area_G_Camp_51" title="Area_G_Camp_51" href="javascript:void(0);" coords="67,315,81,330" shape="rect">
                    <area data-key='B55' target="" alt="Area_G_Camp_52" title="Area_G_Camp_52" href="javascript:void(0);" coords="83,323,101,343" shape="rect">
                    <area data-key='B56' target="" alt="Area_G_Camp_53" title="Area_G_Camp_53" href="javascript:void(0);" coords="106,330,122,351" shape="rect">
                    <area data-key='B57' target="" alt="Area_G_Camp_54" title="Area_G_Camp_54" href="javascript:void(0);" coords="128,340,146,358" shape="rect">
                    <area data-key='B58' target="" alt="Area_G_Camp_55" title="Area_G_Camp_55" href="javascript:void(0);" coords="152,350,170,364" shape="rect">
                    <area data-key='B59' target="" alt="Area_G_Camp_56" title="Area_G_Camp_56" href="javascript:void(0);" coords="179,356,200,371" shape="rect">
                    <area data-key='B60' target="" alt="Area_G_Camp_57" title="Area_G_Camp_57" href="javascript:void(0);" coords="388,428,401,450" shape="rect">
                    <area data-key='B61' target="" alt="Area_G_Camp_58" title="Area_G_Camp_58" href="javascript:void(0);" coords="422,443,438,468" shape="rect">
                    <area data-key='B62' target="" alt="Area_G_Camp_59" title="Area_G_Camp_59" href="javascript:void(0);" coords="64,342,86,356" shape="rect">
                    <area data-key='B63' target="" alt="Area_G_Camp_60" title="Area_G_Camp_60" href="javascript:void(0);" coords="96,352,118,370" shape="rect">
                    <area data-key='B64' target="" alt="Area_G_Camp_61" title="Area_G_Camp_61" href="javascript:void(0);" coords="125,361,140,381" shape="rect">
                    <area data-key='B65' target="" alt="Area_G_Camp_62" title="Area_G_Camp_62" href="javascript:void(0);" coords="150,371,167,391" shape="rect">
                    <area data-key='B66' target="" alt="Area_G_Camp_63" title="Area_G_Camp_63" href="javascript:void(0);" coords="175,377,194,390" shape="rect">
                    <area data-key='B67' target="" alt="Area_G_Camp_64" title="Area_G_Camp_64" href="javascript:void(0);" coords="370,456,386,476" shape="rect">
                    <area data-key='B68' target="" alt="Area_G_Camp_65" title="Area_G_Camp_65" href="javascript:void(0);" coords="406,477,424,500" shape="rect">
                    <area data-key='B69' target="" alt="Area_G_Camp_66" title="Area_G_Camp_66" href="javascript:void(0);" coords="48,388,63,403" shape="rect">
                    <area data-key='B70' target="" alt="Area_G_Camp_67" title="Area_G_Camp_67" href="javascript:void(0);" coords="70,398,86,414" shape="rect">
                    <area data-key='B71' target="" alt="Area_G_Camp_68" title="Area_G_Camp_68" href="javascript:void(0);" coords="97,404,111,421" shape="rect">
                    <area data-key='B72' target="" alt="Area_G_Camp_69" title="Area_G_Camp_69" href="javascript:void(0);" coords="121,412,134,427" shape="rect">
                    <area data-key='B73' target="" alt="Area_G_Camp_70" title="Area_G_Camp_70" href="javascript:void(0);" coords="139,425,157,435" shape="rect">
                    <area data-key='B74' target="" alt="Area_G_Camp_71" title="Area_G_Camp_71" href="javascript:void(0);" coords="169,426,182,439" shape="rect">
                    <area data-key='B75' target="" alt="Area_G_Camp_72" title="Area_G_Camp_72" href="javascript:void(0);" coords="188,424,208,436" shape="rect">
                    <area data-key='B76' target="" alt="Area_G_Camp_73" title="Area_G_Camp_73" href="javascript:void(0);" coords="34,408,49,442" shape="rect">
                    <area data-key='B77' target="" alt="Area_G_Camp_74" title="Area_G_Camp_74" href="javascript:void(0);" coords="80,426,101,443" shape="rect">
                    <area data-key='B78' target="" alt="Area_G_Camp_75" title="Area_G_Camp_75" href="javascript:void(0);" coords="110,437,126,452" shape="rect">
                    <area data-key='B79' target="" alt="Area_G_Camp_76" title="Area_G_Camp_76" href="javascript:void(0);" coords="135,447,153,461" shape="rect">
                    <area data-key='B80' target="" alt="Area_G_Camp_77" title="Area_G_Camp_77" href="javascript:void(0);" coords="167,450,185,465" shape="rect">
                    <area data-key='B81' target="" alt="Area_G_Camp_78" title="Area_G_Camp_78" href="javascript:void(0);" coords="196,449,217,461" shape="rect">
                    <area data-key='B82' target="" alt="Area_G_Camp_79" title="Area_G_Camp_79" href="javascript:void(0);" coords="32,450,54,470" shape="rect">
                    <area data-key='B83' target="" alt="Area_G_Camp_80" title="Area_G_Camp_80" href="javascript:void(0);" coords="223,475,237,493" shape="rect">
                    <area data-key='B84' target="" alt="Area_G_Camp_81" title="Area_G_Camp_81" href="javascript:void(0);" coords="242,479,255,503" shape="rect">
                    <area data-key='B85' target="" alt="Area_G_Camp_82" title="Area_G_Camp_82" href="javascript:void(0);" coords="401,516,420,538" shape="rect">
                    <area data-key='B86' target="" alt="Area_G_Camp_83" title="Area_G_Camp_83" href="javascript:void(0);" coords="56,480,75,503" shape="rect">
                    <area data-key='B87' target="" alt="Area_G_Camp_84" title="Area_G_Camp_84" href="javascript:void(0);" coords="110,493,137,506" shape="rect">
                    <area data-key='B88' target="" alt="Area_G_Camp_85" title="Area_G_Camp_85" href="javascript:void(0);" coords="152,494,174,504" shape="rect">
                    <area data-key='B89' target="" alt="Area_G_Camp_86" title="Area_G_Camp_86" href="javascript:void(0);" coords="213,495,224,509" shape="rect">
                    <area data-key='B90' target="" alt="Area_G_Camp_87" title="Area_G_Camp_87" href="javascript:void(0);" coords="132,525,157,536" shape="rect">
                    <area data-key='B91' target="" alt="Area_G_Camp_88" title="Area_G_Camp_88" href="javascript:void(0);" coords="161,513,185,530" shape="rect">
                    <area data-key='B92' target="" alt="Area_G_Camp_89" title="Area_G_Camp_89" href="javascript:void(0);" coords="205,517,218,531" shape="rect">
                    <area data-key='B93' target="" alt="Area_G_Camp_90" title="Area_G_Camp_90" href="javascript:void(0);" coords="255,524,274,545" shape="rect">
                    <area data-key='B94' target="" alt="Area_G_Camp_91" title="Area_G_Camp_91" href="javascript:void(0);" coords="296,538,321,556" shape="rect">
                    <area data-key='B95' target="" alt="Area_G_Camp_92" title="Area_G_Camp_92" href="javascript:void(0);" coords="351,534,380,550" shape="rect">
                    <area data-key='B96' target="" alt="Area_G_Camp_93" title="Area_G_Camp_93" href="javascript:void(0);" coords="107,549,128,569" shape="rect">
                    <area data-key='B97' target="" alt="Area_G_Camp_94" title="Area_G_Camp_94" href="javascript:void(0);" coords="196,533,211,556" shape="rect">
                    <area data-key='B98' target="" alt="Area_G_Camp_95" title="Area_G_Camp_95" href="javascript:void(0);" coords="180,557,197,575" shape="rect">
                    <area data-key='B99' target="" alt="Area_G_Camp_96" title="Area_G_Camp_96" href="javascript:void(0);" coords="107,574,123,601" shape="rect">
                    <area data-key='B100' target="" alt="Area_G_Camp_97" title="Area_G_Camp_97" href="javascript:void(0);" coords="177,583,190,600" shape="rect">
                    <area data-key='B101' target="" alt="Area_G_Camp_98" title="Area_G_Camp_98" href="javascript:void(0);" coords="156,600,174,621" shape="rect">
                </map>



            </div>-->
            <!--<div id="temporary6">

                <img id="myimage6" src="map_resources/1030_groot_0007_Livello-8.png" usemap="#image-map6">

                <map name="image-map6">
                    <area data-key='C4' target="" alt="Area_H_Camp_1" title="Area_H_Camp_1" href="javascript:void(0);" coords="236,31,252,46" shape="rect">
                    <area data-key='C5' target="" alt="Area_H_Camp_2" title="Area_H_Camp_2" href="javascript:void(0);" coords="259,34,273,52" shape="rect">
                    <area data-key='C6' target="" alt="Area_H_Camp_3" title="Area_H_Camp_3" href="javascript:void(0);" coords="281,43,299,61" shape="rect">
                    <area data-key='C7' target="" alt="Area_H_Camp_4" title="Area_H_Camp_4" href="javascript:void(0);" coords="203,42,221,59" shape="rect">
                    <area data-key='C8' target="" alt="Area_H_Camp_5" title="Area_H_Camp_5" href="javascript:void(0);" coords="300,69,314,81" shape="rect">
                    <area data-key='C9' target="" alt="Area_H_Camp_6" title="Area_H_Camp_6" href="javascript:void(0);" coords="194,60,212,79" shape="rect">
                    <area data-key='C10' target="" alt="Area_H_Camp_7" title="Area_H_Camp_7" href="javascript:void(0);" coords="292,82,304,99" shape="rect">
                    <area data-key='C11' target="" alt="Area_H_Camp_8" title="Area_H_Camp_8" href="javascript:void(0);" coords="189,79,203,100" shape="rect">
                    <area data-key='C12' target="" alt="Area_H_Camp_9" title="Area_H_Camp_9" href="javascript:void(0);" coords="286,101,300,115" shape="rect">
                    <area data-key='C13' target="" alt="Area_H_Camp_10" title="Area_H_Camp_10" href="javascript:void(0);" coords="178,101,193,120" shape="rect">
                    <area data-key='C14' target="" alt="Area_H_Camp_11" title="Area_H_Camp_11" href="javascript:void(0);" coords="170,125,183,140" shape="rect">
                    <area data-key='C15' target="" alt="Area_H_Camp_12" title="Area_H_Camp_12" href="javascript:void(0);" coords="256,155,274,176" shape="rect">
                    <area data-key='C16' target="" alt="Area_H_Camp_13" title="Area_H_Camp_13" href="javascript:void(0);" coords="180,151,194,165" shape="rect">
                    <area data-key='C17' target="" alt="Area_H_Camp_14" title="Area_H_Camp_14" href="javascript:void(0);" coords="205,161,218,176" shape="rect">
                    <area data-key='C18' target="" alt="Area_H_Camp_15" title="Area_H_Camp_15" href="javascript:void(0);" coords="229,170,241,186" shape="rect">
                    <area data-key='C19' target="" alt="Area_H_Camp_16" title="Area_H_Camp_16" href="javascript:void(0);" coords="165,173,185,192" shape="rect">
                    <area data-key='C20' target="" alt="Area_H_Camp_17" title="Area_H_Camp_17" href="javascript:void(0);" coords="191,186,209,201" shape="rect">
                    <area data-key='C21' target="" alt="Area_H_Camp_18" title="Area_H_Camp_18" href="javascript:void(0);" coords="215,198,235,212" shape="rect">
                    <area data-key='C22' target="" alt="Area_H_Camp_19" title="Area_H_Camp_19" href="javascript:void(0);" coords="138,190,155,209" shape="rect">
                    <area data-key='C23' target="" alt="Area_H_Camp_20" title="Area_H_Camp_20" href="javascript:void(0);" coords="228,223,243,246" shape="rect">
                    <area data-key='C24' target="" alt="Area_H_Camp_21" title="Area_H_Camp_21" href="javascript:void(0);" coords="131,210,144,229" shape="rect">
                    <area data-key='C25' target="" alt="Area_H_Camp_22" title="Area_H_Camp_22" href="javascript:void(0);" coords="218,251,234,268" shape="rect">
                    <area data-key='C26' target="" alt="Area_H_Camp_23" title="Area_H_Camp_23" href="javascript:void(0);" coords="122,230,137,249" shape="rect">
                    <area data-key='C27' target="" alt="Area_H_Camp_24" title="Area_H_Camp_24" href="javascript:void(0);" coords="208,275,223,293" shape="rect">
                    <area data-key='C28' target="" alt="Area_H_Camp_25" title="Area_H_Camp_25" href="javascript:void(0);" coords="112,254,126,270" shape="rect">
                    <area data-key='C29' target="" alt="Area_H_Camp_26" title="Area_H_Camp_26" href="javascript:void(0);" coords="199,295,211,317" shape="rect">
                    <area data-key='C30' target="" alt="Area_H_Camp_27" title="Area_H_Camp_27" href="javascript:void(0);" coords="105,274,118,295" shape="rect">
                    <area data-key='C31' target="" alt="Area_H_Camp_28" title="Area_H_Camp_28" href="javascript:void(0);" coords="93,295,106,314" shape="rect">
                    <area data-key='C32' target="" alt="Area_H_Camp_29" title="Area_H_Camp_29" href="javascript:void(0);" coords="181,327,195,348" shape="rect">
                    <area data-key='C33' target="" alt="Area_H_Camp_30" title="Area_H_Camp_30" href="javascript:void(0);" coords="83,316,98,338" shape="rect">
                    <area data-key='C34' target="" alt="Area_H_Camp_31" title="Area_H_Camp_31" href="javascript:void(0);" coords="171,351,187,368" shape="rect">
                    <area data-key='C35' target="" alt="Area_H_Camp_32" title="Area_H_Camp_32" href="javascript:void(0);" coords="76,337,87,353" shape="rect">
                    <area data-key='C36' target="" alt="Area_H_Camp_33" title="Area_H_Camp_33" href="javascript:void(0);" coords="163,370,176,389" shape="rect">
                    <area data-key='C37' target="" alt="Area_H_Camp_34" title="Area_H_Camp_34" href="javascript:void(0);" coords="86,366,108,377" shape="rect">
                    <area data-key='C38' target="" alt="Area_H_Camp_35" title="Area_H_Camp_35" href="javascript:void(0);" coords="115,371,127,390" shape="rect">
                    <area data-key='C39' target="" alt="Area_H_Camp_36" title="Area_H_Camp_36" href="javascript:void(0);" coords="136,385,151,400" shape="rect">
                    <area data-key='C40' target="" alt="Area_H_Camp_37" title="Area_H_Camp_37" href="javascript:void(0);" coords="49,379,68,402" shape="rect">
                    <area data-key='C41' target="" alt="Area_H_Camp_38" title="Area_H_Camp_38" href="javascript:void(0);" coords="140,412,157,434" shape="rect">
                    <area data-key='C42' target="" alt="Area_H_Camp_39" title="Area_H_Camp_39" href="javascript:void(0);" coords="42,407,56,427" shape="rect">
                    <area data-key='C43' target="" alt="Area_H_Camp_40" title="Area_H_Camp_40" href="javascript:void(0);" coords="137,435,150,460" shape="rect">
                    <area data-key='C44' target="" alt="Area_H_Camp_41" title="Area_H_Camp_41" href="javascript:void(0);" coords="27,430,44,453" shape="rect">
                    <area data-key='C45' target="" alt="Area_H_Camp_42" title="Area_H_Camp_42" href="javascript:void(0);" coords="125,462,141,481" shape="rect">
                    <area data-key='C46' target="" alt="Area_H_Camp_43" title="Area_H_Camp_43" href="javascript:void(0);" coords="14,461,32,483" shape="rect">
                    <area data-key='C47' target="" alt="Area_H_Camp_44" title="Area_H_Camp_44" href="javascript:void(0);" coords="106,484,125,496" shape="rect">
                    <area data-key='C48' target="" alt="Area_H_Camp_45" title="Area_H_Camp_45" href="javascript:void(0);" coords="77,488,96,502" shape="rect">
                </map>

            </div>-->
            <!--<div id="temporary7">
                 Image Map Generated by http://www.image-map.net/ 
                <img id="myimage7" src="map_resources/1030_groot_0008_Livello-9.png" usemap="#image-map7">

                <map name="image-map7">
                    <area data-key='D4' target="" alt="Area_I_Camp_1" title="Area_I_Camp_1" href="javascript:void(0);" coords="366,34,380,54" shape="rect">
                    <area data-key='D5' target="" alt="Area_I_Camp_2" title="Area_I_Camp_2" href="javascript:void(0);" coords="406,28,422,44" shape="rect">
                    <area data-key='D6' target="" alt="Area_I_Camp_3" title="Area_I_Camp_3" href="javascript:void(0);" coords="442,41,463,63" shape="rect">
                    <area data-key='D7' target="" alt="Area_I_Camp_4" title="Area_I_Camp_4" href="javascript:void(0);" coords="451,70,467,97" shape="rect">
                    <area data-key='D8' target="" alt="Area_I_Camp_5" title="Area_I_Camp_5" href="javascript:void(0);" coords="411,84,427,102" shape="rect">
                    <area data-key='D9' target="" alt="Area_I_Camp_6" title="Area_I_Camp_6" href="javascript:void(0);" coords="401,105,419,129" shape="rect">
                    <area data-key='D10' target="" alt="Area_I_Camp_7" title="Area_I_Camp_7" href="javascript:void(0);" coords="422,147,437,164" shape="rect">
                    <area data-key='D11' target="" alt="Area_I_Camp_8" title="Area_I_Camp_8" href="javascript:void(0);" coords="372,166,391,182" shape="rect">
                    <area data-key='D12' target="" alt="Area_I_Camp_9" title="Area_I_Camp_9" href="javascript:void(0);" coords="337,149,359,170" shape="rect">
                    <area data-key='D13' target="" alt="Area_I_Camp_10" title="Area_I_Camp_10" href="javascript:void(0);" coords="344,99,361,118" shape="rect">
                    <area data-key='D14' target="" alt="Area_I_Camp_11" title="Area_I_Camp_11" href="javascript:void(0);" coords="314,165,331,185" shape="rect">
                    <area data-key='D15' target="" alt="Area_I_Camp_12" title="Area_I_Camp_12" href="javascript:void(0);" coords="339,175,362,199" shape="rect">
                    <area data-key='D16' target="" alt="Area_I_Camp_13" title="Area_I_Camp_13" href="javascript:void(0);" coords="369,189,390,210" shape="rect">
                    <area data-key='D17' target="" alt="Area_I_Camp_14" title="Area_I_Camp_14" href="javascript:void(0);" coords="388,223,404,246" shape="rect">
                    <area data-key='D18' target="" alt="Area_I_Camp_15" title="Area_I_Camp_15" href="javascript:void(0);" coords="376,250,389,268" shape="rect">
                    <area data-key='D19' target="" alt="Area_I_Camp_16" title="Area_I_Camp_16" href="javascript:void(0);" coords="341,242,357,255" shape="rect">
                    <area data-key='D20' target="" alt="Area_I_Camp_17" title="Area_I_Camp_17" href="javascript:void(0);" coords="317,234,331,247" shape="rect">
                    <area data-key='D21' target="" alt="Area_I_Camp_18" title="Area_I_Camp_18" href="javascript:void(0);" coords="286,225,305,237" shape="rect">
                    <area data-key='D22' target="" alt="Area_I_Camp_19" title="Area_I_Camp_19" href="javascript:void(0);" coords="265,200,279,216" shape="rect">
                    <area data-key='D23' target="" alt="Area_I_Camp_20" title="Area_I_Camp_20" href="javascript:void(0);" coords="268,171,281,184" shape="rect">
                    <area data-key='D24' target="" alt="Area_I_Camp_21" title="Area_I_Camp_21" href="javascript:void(0);" coords="179,178,195,203" shape="rect">
                    <area data-key='D25' target="" alt="Area_I_Camp_22" title="Area_I_Camp_22" href="javascript:void(0);" coords="201,209,217,227" shape="rect">
                    <area data-key='D26' target="" alt="Area_I_Camp_23" title="Area_I_Camp_23" href="javascript:void(0);" coords="230,221,245,243" shape="rect">
                    <area data-key='D27' target="" alt="Area_I_Camp_24" title="Area_I_Camp_24" href="javascript:void(0);" coords="279,246,300,265" shape="rect">
                    <area data-key='D28' target="" alt="Area_I_Camp_25" title="Area_I_Camp_25" href="javascript:void(0);" coords="307,257,327,277" shape="rect">
                    <area data-key='D29' target="" alt="Area_I_Camp_26" title="Area_I_Camp_26" href="javascript:void(0);" coords="339,268,352,288" shape="rect">
                    <area data-key='D30' target="" alt="Area_I_Camp_27" title="Area_I_Camp_27" href="javascript:void(0);" coords="94,197,109,215" shape="rect">
                    <area data-key='D31' target="" alt="Area_I_Camp_28" title="Area_I_Camp_28" href="javascript:void(0);" coords="141,218,159,236" shape="rect">
                    <area data-key='D32' target="" alt="Area_I_Camp_29" title="Area_I_Camp_29" href="javascript:void(0);" coords="181,239,197,254" shape="rect">
                    <area data-key='D33' target="" alt="Area_I_Camp_30" title="Area_I_Camp_30" href="javascript:void(0);" coords="208,266,226,284" shape="rect">
                    <area data-key='D34' target="" alt="Area_I_Camp_31" title="Area_I_Camp_31" href="javascript:void(0);" coords="227,290,242,303" shape="rect">
                    <area data-key='D35' target="" alt="Area_I_Camp_32" title="Area_I_Camp_32" href="javascript:void(0);" coords="266,308,281,323" shape="rect">
                    <area data-key='D36' target="" alt="Area_I_Camp_33" title="Area_I_Camp_33" href="javascript:void(0);" coords="287,317,302,335" shape="rect">
                    <area data-key='D37' target="" alt="Area_I_Camp_34" title="Area_I_Camp_34" href="javascript:void(0);" coords="310,324,330,344" shape="rect">
                    <area data-key='D38' target="" alt="Area_I_Camp_35" title="Area_I_Camp_35" href="javascript:void(0);" coords="350,314,370,333" shape="rect">
                    <area data-key='D39' target="" alt="Area_I_Camp_36" title="Area_I_Camp_36" href="javascript:void(0);" coords="164,253,176,269" shape="rect">
                    <area data-key='D40' target="" alt="Area_I_Camp_37" title="Area_I_Camp_37" href="javascript:void(0);" coords="185,275,199,289" shape="rect">
                    <area data-key='D41' target="" alt="Area_I_Camp_38" title="Area_I_Camp_38" href="javascript:void(0);" coords="193,295,215,312" shape="rect">
                    <area data-key='D42' target="" alt="Area_I_Camp_39" title="Area_I_Camp_39" href="javascript:void(0);" coords="213,316,226,332" shape="rect">
                    <area data-key='D43' target="" alt="Area_I_Camp_40" title="Area_I_Camp_40" href="javascript:void(0);" coords="232,335,250,356" shape="rect">
                    <area data-key='D44' target="" alt="Area_I_Camp_41" title="Area_I_Camp_41" href="javascript:void(0);" coords="259,344,276,365" shape="rect">
                    <area data-key='D45' target="" alt="Area_I_Camp_42" title="Area_I_Camp_42" href="javascript:void(0);" coords="283,350,298,368" shape="rect">
                    <area data-key='D46' target="" alt="Area_I_Camp_43" title="Area_I_Camp_43" href="javascript:void(0);" coords="303,354,318,373" shape="rect">
                    <area data-key='D47' target="" alt="Area_I_Camp_44" title="Area_I_Camp_44" href="javascript:void(0);" coords="320,389,334,405" shape="rect">
                    <area data-key='D48' target="" alt="Area_I_Camp_45" title="Area_I_Camp_45" href="javascript:void(0);" coords="292,410,308,427" shape="rect">
                    <area data-key='D49' target="" alt="Area_I_Camp_46" title="Area_I_Camp_46" href="javascript:void(0);" coords="268,410,288,428" shape="rect">
                    <area data-key='D50' target="" alt="Area_I_Camp_47" title="Area_I_Camp_47" href="javascript:void(0);" coords="245,408,263,426" shape="rect">
                    <area data-key='D51' target="" alt="Area_I_Camp_48" title="Area_I_Camp_48" href="javascript:void(0);" coords="227,414,241,428" shape="rect">
                    <area data-key='D52' target="" alt="Area_I_Camp_49" title="Area_I_Camp_49" href="javascript:void(0);" coords="202,399,218,416" shape="rect">
                    <area data-key='D53' target="" alt="Area_I_Camp_50" title="Area_I_Camp_50" href="javascript:void(0);" coords="178,419,189,431" shape="rect">
                    <area data-key='D54' target="" alt="Area_I_Camp_51" title="Area_I_Camp_51" href="javascript:void(0);" coords="188,381,199,396" shape="rect">
                    <area data-key='D55' target="" alt="Area_I_Camp_52" title="Area_I_Camp_52" href="javascript:void(0);" coords="164,364,181,377" shape="rect">
                    <area data-key='D56' target="" alt="Area_I_Camp_53" title="Area_I_Camp_53" href="javascript:void(0);" coords="129,324,145,342" shape="rect">
                    <area data-key='D57' target="" alt="Area_I_Camp_54" title="Area_I_Camp_54" href="javascript:void(0);" coords="136,301,154,317" shape="rect">
                    <area data-key='D58' target="" alt="Area_I_Camp_55" title="Area_I_Camp_55" href="javascript:void(0);" coords="145,283,162,296" shape="rect">
                </map>



            </div>-->
            <!--<div id="temporary8">
                <img id="myimage8" src="map_resources/1030_groot_0009_Livello-10.png" usemap="#image-map8">

                <map name="image-map8">
                    <area data-key='E4' target="" alt="Area_J_Camp_1" title="Area_J_Camp_1" href="javascript:void(0);" coords="183,40,208,58" shape="rect">
                    <area data-key='E5' target="" alt="Area_J_Camp_2" title="Area_J_Camp_2" href="javascript:void(0);" coords="222,53,243,72" shape="rect">
                    <area data-key='E6' target="" alt="Area_J_Camp_3" title="Area_J_Camp_3" href="javascript:void(0);" coords="259,66,282,87" shape="rect">
                    <area data-key='E7' target="" alt="Area_J_Camp_4" title="Area_J_Camp_4" href="javascript:void(0);" coords="296,82,322,99" shape="rect">
                    <area data-key='E8' target="" alt="Area_J_Camp_5" title="Area_J_Camp_5" href="javascript:void(0);" coords="333,97,357,122" shape="rect">
                    <area data-key='E9' target="" alt="Area_J_Camp_6" title="Area_J_Camp_6" href="javascript:void(0);" coords="365,125,379,153" shape="rect">
                    <area data-key='E10' target="" alt="Area_J_Camp_7" title="Area_J_Camp_7" href="javascript:void(0);" coords="359,163,373,192" shape="rect">
                    <area data-key='E11' target="" alt="Area_J_Camp_8" title="Area_J_Camp_8" href="javascript:void(0);" coords="351,198,365,228" shape="rect">
                    <area data-key='E12' target="" alt="Area_J_Camp_9" title="Area_J_Camp_9" href="javascript:void(0);" coords="320,235,343,250" shape="rect">
                    <area data-key='E13' target="" alt="Area_J_Camp_10" title="Area_J_Camp_10" href="javascript:void(0);" coords="286,237,307,255" shape="rect">
                    <area data-key='E14' target="" alt="Area_J_Camp_11" title="Area_J_Camp_11" href="javascript:void(0);" coords="258,222,277,241" shape="rect">
                    <area data-key='E15' target="" alt="Area_J_Camp_12" title="Area_J_Camp_12" href="javascript:void(0);" coords="242,188,259,212" shape="rect">
                    <area data-key='E16' target="" alt="Area_J_Camp_13" title="Area_J_Camp_13" href="javascript:void(0);" coords="214,161,235,181" shape="rect">
                    <area data-key='E17' target="" alt="Area_J_Camp_14" title="Area_J_Camp_14" href="javascript:void(0);" coords="144,140,176,154" shape="rect">
                    <area data-key='E18' target="" alt="Area_J_Camp_15" title="Area_J_Camp_15" href="javascript:void(0);" coords="111,141,135,155" shape="rect">
                </map>


            </div>-->
            <div id="temporary9">

                <!-- Image Map Generated by http://www.image-map.net/ -->
                <img id="myimage9" src="map_resources/1030_groot_0000_Livello-1.png" usemap="#image-map9">

                <map name="image-map9">
                    <area data-key='F4' target=""  value="1"  href="javascript:void(0);" onclick='CheckSpotAvailability(1, "C");' coords="561,1050,527,1086" shape="rect">
                    <area data-key='F5' target=""  value="2"  href="javascript:void(0);" onclick='CheckSpotAvailability(2, "C");' coords="572,1014,540,1046" shape="rect">
                    <area data-key='F6' target=""  value="3"  href="javascript:void(0);" onclick='CheckSpotAvailability(3, "C");' coords="585,977,550,1013" shape="rect">
                    <area data-key='F7' target=""  value="4"  href="javascript:void(0);" onclick='CheckSpotAvailability(4, "C");' coords="524,945,557,969" shape="rect">
                    <area data-key='F8' target=""  value="5"  href="javascript:void(0);" onclick='CheckSpotAvailability(5, "C");' coords="587,938,560,967" shape="rect">
                    <area data-key='F9' target=""  value="6"  href="javascript:void(0);" onclick='CheckSpotAvailability(6, "C");' coords="607,918,580,891" shape="rect">
                    <area data-key='F10' target=""  value="7"  href="javascript:void(0);" onclick='CheckSpotAvailability(7, "C");' coords="559,882,584,908" shape="rect">
                    <area data-key='F11' target=""  value="8"  href="javascript:void(0);" onclick='CheckSpotAvailability(8, "C");' coords="535,871,564,901" shape="rect">
                    <area data-key='F12' target=""  value="9"  href="javascript:void(0);" onclick='CheckSpotAvailability(9, "C");' coords="543,823,571,845" shape="rect">
                    <area data-key='F13' target=""  value="10"  href="javascript:void(0);" onclick='CheckSpotAvailability(10, "C");' coords="570,819,598,845" shape="rect">
                    <area data-key='F14' target=""  value="11"  href="javascript:void(0);" onclick='CheckSpotAvailability(11, "C");' coords="623,819,598,842" shape="rect">
                    <area data-key='F15' target=""  value="12"  href="javascript:void(0);" onclick='CheckSpotAvailability(12, "C");' coords="642,755,615,783" shape="rect">
                    <area data-key='F16' target=""  value="13"  href="javascript:void(0);" onclick='CheckSpotAvailability(13, "C");' coords="589,757,615,782" shape="rect">
                    <area data-key='F17' target=""  value="14"  href="javascript:void(0);" onclick='CheckSpotAvailability(14, "C");' coords="562,761,589,784" shape="rect">
                    <area data-key='F18' target=""  value="15"  href="javascript:void(0);" onclick='CheckSpotAvailability(15, "C");' coords="534,763,562,787" shape="rect">
                    <area data-key='F19' target=""  value="16"  href="javascript:void(0);" onclick='CheckSpotAvailability(16, "C");' coords="529,724,551,748" shape="rect">
                    <area data-key='F20' target=""  value="17"  href="javascript:void(0);" onclick='CheckSpotAvailability(17, "C");' coords="550,723,571,746" shape="rect">
                    <area data-key='F21' target=""  value="18"  href="javascript:void(0);" onclick='CheckSpotAvailability(18, "C");' coords="570,721,591,744" shape="rect">
                    <area data-key='F22' target=""  value="19"  href="javascript:void(0);" onclick='CheckSpotAvailability(19, "C");' coords="591,719,610,744" shape="rect">
                    <area data-key='F23' target=""  value="20"  href="javascript:void(0);" onclick='CheckSpotAvailability(20, "C");' coords="645,713,617,743" shape="rect">
                    <area data-key='F24' target=""  value="21"  href="javascript:void(0);" onclick='CheckSpotAvailability(21, "C");' coords="627,672,653,700" shape="rect">
                    <area data-key='F25' target=""  value="22"  href="javascript:void(0);" onclick='CheckSpotAvailability(22, "C");' coords="601,665,627,695" shape="rect">
                    <area data-key='F26' target=""  value="23"  href="javascript:void(0);" onclick='CheckSpotAvailability(23, "C");' coords="574,661,601,690" shape="rect">
                    <area data-key='F27' target=""  value="24"  href="javascript:void(0);" onclick='CheckSpotAvailability(24, "C");' coords="547,655,574,683" shape="rect">
                    <area data-key='F28' target=""  value="25"  href="javascript:void(0);" onclick='CheckSpotAvailability(25, "C");' coords="545,701,518,672" shape="rect">
                    <area data-key='F29' target=""  value="26"  href="javascript:void(0);" onclick='CheckSpotAvailability(26, "C");' coords="625,652,596,629" shape="rect">
                    <area data-key='F30' target=""  value="27"  href="javascript:void(0);" onclick='CheckSpotAvailability(27, "C");' coords="624,633,652,655" shape="rect">
                    <area data-key='F31' target=""  value="28"  href="javascript:void(0);" onclick='CheckSpotAvailability(28, "C");' coords="652,636,677,657" shape="rect">
                    <area data-key='F32' target=""  value="29"  href="javascript:void(0);" onclick='CheckSpotAvailability(29, "C");' coords="700,600,724,626" shape="rect">
                    <area data-key='F33' target=""  value="30"  href="javascript:void(0);" onclick='CheckSpotAvailability(30, "C");' coords="710,576,732,604" shape="rect">
                    <area data-key='F34' target=""  value="31"  href="javascript:void(0);" onclick='CheckSpotAvailability(31, "C");' coords="719,551,738,579" shape="rect">
                    <area data-key='F35' target=""  value="32"  href="javascript:void(0);" onclick='CheckSpotAvailability(32, "C");' coords="723,528,744,552" shape="rect">
                    <area data-key='F36' target=""  value="33"  href="javascript:void(0);" onclick='CheckSpotAvailability(33, "C");' coords="729,504,748,530" shape="rect">
                    <area data-key='F37' target=""  value="34"  href="javascript:void(0);" onclick='CheckSpotAvailability(34, "C");' coords="730,479,750,503" shape="rect">
                    <area data-key='F38' target=""  value="35"  href="javascript:void(0);" onclick='CheckSpotAvailability(35, "C");' coords="733,454,751,478" shape="rect">
                    <area data-key='F39' target=""  value="36"  href="javascript:void(0);" onclick='CheckSpotAvailability(36, "C");' coords="732,428,752,453" shape="rect">
                    <area data-key='F40' target=""  value="37"  href="javascript:void(0);" onclick='CheckSpotAvailability(37, "C");' coords="734,398,760,415" shape="rect">
                    <area data-key='F41' target=""  value="38"  href="javascript:void(0);" onclick='CheckSpotAvailability(38, "C");' coords="746,382,773,399" shape="rect">
                    <area data-key='F42' target=""  value="39"  href="javascript:void(0);" onclick='CheckSpotAvailability(39, "C");' coords="771,356,794,375" shape="rect">
                    <area data-key='F43' target=""  value="40"  href="javascript:void(0);" onclick='CheckSpotAvailability(40, "C");' coords="781,340,802,357" shape="rect">
                    <area data-key='F44' target=""  value="41"  href="javascript:void(0);" onclick='CheckSpotAvailability(41, "C");' coords="792,322,811,340" shape="rect">
                    <area data-key='F45' target=""  value="42"  href="javascript:void(0);" onclick='CheckSpotAvailability(42, "C");' coords="801,304,821,323" shape="rect">
                    <area data-key='F46' target=""  value="43"  href="javascript:void(0);" onclick='CheckSpotAvailability(43, "C");' coords="813,285,831,304" shape="rect">
                    <area data-key='F47' target=""  value="44"  href="javascript:void(0);" onclick='CheckSpotAvailability(44, "C");' coords="825,266,843,287" shape="rect">
                    <area data-key='F48' target=""  value="45"  href="javascript:void(0);" onclick='CheckSpotAvailability(45, "C");' coords="836,249,854,268" shape="rect">
                    <area data-key='F49' target=""  value="46"  href="javascript:void(0);" onclick='CheckSpotAvailability(46, "C");' coords="847,233,863,251" shape="rect">
                    <area data-key='F50' target=""  value="47"  href="javascript:void(0);" onclick='CheckSpotAvailability(47, "C");' coords="848,190,868,211" shape="rect">
                    <area data-key='F51' target=""  value="48"  href="javascript:void(0);" onclick='CheckSpotAvailability(48, "C");' coords="833,183,849,202" shape="rect">
                    <area data-key='F52' target=""  value="49"  href="javascript:void(0);" onclick='CheckSpotAvailability(49, "C");' coords="816,172,834,193" shape="rect">
                    <area data-key='F53' target=""  value="50"  href="javascript:void(0);" onclick='CheckSpotAvailability(50, "C");' coords="801,165,816,184" shape="rect">
                    <area data-key='F54' target=""  value="51"  href="javascript:void(0);" onclick='CheckSpotAvailability(51, "C");' coords="764,177,788,212" shape="rect">
                    <area data-key='F55' target=""  value="52"  href="javascript:void(0);" onclick='CheckSpotAvailability(52, "C");' coords="748,204,768,238" shape="rect">
                    <area data-key='F56' target=""  value="53"  href="javascript:void(0);" onclick='CheckSpotAvailability(53, "C");' coords="687,194,707,220" shape="rect">
                    <area data-key='F57' target=""  value="54"  href="javascript:void(0);" onclick='CheckSpotAvailability(54, "C");' coords="694,169,716,195" shape="rect">
                    <area data-key='F58' target=""  value="55"  href="javascript:void(0);" onclick='CheckSpotAvailability(55, "C");' coords="702,142,724,170" shape="rect">
                    <area data-key='F59' target=""  value="56"  href="javascript:void(0);" onclick='CheckSpotAvailability(56, "C");' coords="614,109,634,134" shape="rect">
                    <area data-key='F60' target=""  value="57"  href="javascript:void(0);" onclick='CheckSpotAvailability(57, "C");' coords="603,131,622,151" shape="rect">
                    <area data-key='F61' target=""  value="58"  href="javascript:void(0);" onclick='CheckSpotAvailability(58, "C");' coords="594,150,611,170" shape="rect">
                    <area data-key='F62' target=""  value="59"  href="javascript:void(0);" onclick='CheckSpotAvailability(59, "C");' coords="581,170,599,190" shape="rect">
                    <area data-key='F63' target=""  value="60"  href="javascript:void(0);" onclick='CheckSpotAvailability(60, "C");' coords="535,198,553,218" shape="rect">
                    <area data-key='F64' target=""  value="61"  href="javascript:void(0);" onclick='CheckSpotAvailability(61, "C");' coords="528,177,544,195" shape="rect">
                    <area data-key='F65' target=""  value="62"  href="javascript:void(0);" onclick='CheckSpotAvailability(62, "C");' coords="513,161,533,178" shape="rect">
                    <area data-key='F66' target=""  value="63"  href="javascript:void(0);" onclick='CheckSpotAvailability(63, "C");' coords="494,154,512,170" shape="rect">
                    <area data-key='F67' target=""  value="64"  href="javascript:void(0);" onclick='CheckSpotAvailability(64, "C");' coords="438,175,464,199" shape="rect">
                    <area data-key='F68' target=""  value="65"  href="javascript:void(0);" onclick='CheckSpotAvailability(65, "C");' coords="425,202,442,234" shape="rect">
                    <area data-key='F69' target=""  value="66"  href="javascript:void(0);" onclick='CheckSpotAvailability(66, "C");' coords="416,237,434,270" shape="rect">
                    <area data-key='F70' target=""  value="67"  href="javascript:void(0);" onclick='CheckSpotAvailability(67, "C");' coords="413,272,435,313" shape="rect">
                    <area data-key='F71' target=""  value="68"  href="javascript:void(0);" onclick='CheckSpotAvailability(68, "C");' coords="421,363,395,385" shape="rect">
                    <area data-key='F72' target=""  value="69"  href="javascript:void(0);" onclick='CheckSpotAvailability(69, "C");' coords="370,382,398,413" shape="rect">
                    <area data-key='F73' target=""  value="70"  href="javascript:void(0);" onclick='CheckSpotAvailability(70, "C");' coords="358,409,376,438" shape="rect">
                    <area data-key='F74' target=""  value="71"  href="javascript:void(0);" onclick='CheckSpotAvailability(71, "C");' coords="344,438,362,464" shape="rect">
                    <area data-key='F75' target=""  value="72"  href="javascript:void(0);" onclick='CheckSpotAvailability(72, "C");' coords="337,467,353,489" shape="rect">
                    <area data-key='F76' target=""  value="73"  href="javascript:void(0);" onclick='CheckSpotAvailability(73, "C");' coords="287,547,306,570" shape="rect">
                    <area data-key='F77' target=""  value="74"  href="javascript:void(0);" onclick='CheckSpotAvailability(74, "C");' coords="274,568,292,593" shape="rect">
                    <area data-key='F78' target=""  value="75"  href="javascript:void(0);" onclick='CheckSpotAvailability(75, "C");' coords="260,591,280,612" shape="rect">
                    <area data-key='F79' target=""  value="76"  href="javascript:void(0);" onclick='CheckSpotAvailability(76, "C");' coords="249,611,268,638" shape="rect">
                    <area data-key='F80' target=""  value="77"  href="javascript:void(0);" onclick='CheckSpotAvailability(77, "C");' coords="233,637,255,666" shape="rect">
                    <area data-key='F81' target=""  value="78"  href="javascript:void(0);" onclick='CheckSpotAvailability(78, "C");' coords="216,664,239,693" shape="rect">
                    <area data-key='F82' target=""  value="79"  href="javascript:void(0);" onclick='CheckSpotAvailability(79, "C");' coords="200,694,223,722" shape="rect">
                    <area data-key='F83' target=""  value="80"  href="javascript:void(0);" onclick='CheckSpotAvailability(80, "C");' coords="185,720,208,751" shape="rect">
                    <area data-key='F84' target=""  value="81"  href="javascript:void(0);" onclick='CheckSpotAvailability(81, "C");' coords="170,748,193,777" shape="rect">
                    <area data-key='F85' target=""  value="82"  href="javascript:void(0);" onclick='CheckSpotAvailability(82, "C");' coords="155,775,177,803" shape="rect">
                </map>

            </div>
            <!--<div id="temporary10">
                <img id="myimage10" src="map_resources/1030_groot_0001_Livello-2.png" usemap="#image-map10">

                <map name="image-map10">
                    <area data-key='G4' target="" alt="campa1" title="campa1" href="javascript:void(0);" coords="337,56,375,81" shape="rect">
                    <area data-key='G5' target="" alt="campa2" title="campa2" href="javascript:void(0);" coords="373,49,401,74" shape="rect">
                    <area data-key='G6' target="" alt="campa3" title="campa3" href="javascript:void(0);" coords="402,51,430,75" shape="rect">
                    <area data-key='G7' target="" alt="campa4" title="campa4" href="javascript:void(0);" coords="431,56,457,82" shape="rect">
                    <area data-key='G8' target="" alt="campa5" title="campa5" href="javascript:void(0);" coords="458,61,483,84" shape="rect">
                    <area data-key='G9' target="" alt="campa6" title="campa6" href="javascript:void(0);" coords="484,66,512,92" shape="rect">
                    <area data-key='G10' target="" alt="campa7" title="campa7" href="javascript:void(0);" coords="513,69,540,97" shape="rect">
                    <area data-key='G11' target="" alt="campa8" title="campa8" href="javascript:void(0);" coords="542,78,571,101" shape="rect">
                    <area data-key='G12' target="" alt="campa9" title="campa9" href="javascript:void(0);" coords="573,82,604,106" shape="rect">
                    <area data-key='G13' target="" alt="campa10" title="campa10" href="javascript:void(0);" coords="604,89,636,115" shape="rect">
                    <area data-key='G14' target="" alt="campa11" title="campa11" href="javascript:void(0);" coords="662,141,631,107" shape="rect">
                    <area data-key='G15' target="" alt="campa12" title="campa12" href="javascript:void(0);" coords="628,162,655,189" shape="rect">
                    <area data-key='G16' target="" alt="campa13" title="campa13" href="javascript:void(0);" coords="610,176,630,198" shape="rect">
                    <area data-key='G17' target="" alt="campa14" title="campa14" href="javascript:void(0);" coords="588,181,611,202" shape="rect">
                    <area data-key='G18' target="" alt="campa15" title="campa15" href="javascript:void(0);" coords="563,178,587,206" shape="rect">
                    <area data-key='G19' target="" alt="campa16" title="campa16" href="javascript:void(0);" coords="540,174,561,198" shape="rect">
                    <area data-key='G20' target="" alt="campa17" title="campa17" href="javascript:void(0);" coords="512,160,540,186" shape="rect">
                    <area data-key='G21' target="" alt="campa18" title="campa18" href="javascript:void(0);" coords="434,155,408,174" shape="rect">
                    <area data-key='G22' target="" alt="campa19" title="campa19" href="javascript:void(0);" coords="381,152,404,174" shape="rect">
                    <area data-key='G23' target="" alt="campa20" title="campa20" href="javascript:void(0);" coords="355,143,378,168" shape="rect">
                    <area data-key='G24' target="" alt="campa21" title="campa21" href="javascript:void(0);" coords="328,126,354,152" shape="rect">
                    <area data-key='G25' target="" alt="campa22" title="campa22" href="javascript:void(0);" coords="319,97,340,127" shape="rect">
                    <area data-key='G26' target="" alt="campb1" title="campb1" href="javascript:void(0);" coords="285,182,313,204" shape="rect">
                    <area data-key='G27' target="" alt="campb2" title="campb2" href="javascript:void(0);" coords="314,172,340,196" shape="rect">
                    <area data-key='G28' target="" alt="campb3" title="campb3" href="javascript:void(0);" coords="343,171,370,193" shape="rect">
                    <area data-key='G29' target="" alt="campb4" title="campb4" href="javascript:void(0);" coords="369,175,397,198" shape="rect">
                    <area data-key='G30' target="" alt="campb5" title="campb5" href="javascript:void(0);" coords="393,186,423,211" shape="rect">
                    <area data-key='G31' target="" alt="campb6" title="campb6" href="javascript:void(0);" coords="500,201,529,220" shape="rect">
                    <area data-key='G32' target="" alt="campb7" title="campb7" href="javascript:void(0);" coords="531,201,558,222" shape="rect">
                    <area data-key='G33' target="" alt="campb8" title="campb8" href="javascript:void(0);" coords="558,208,584,231" shape="rect">
                    <area data-key='G34' target="" alt="campb9" title="campb9" href="javascript:void(0);" coords="578,222,612,249" shape="rect">
                    <area data-key='G35' target="" alt="campb10" title="campb10" href="javascript:void(0);" coords="598,249,617,279" shape="rect">
                    <area data-key='G36' target="" alt="campb11" title="campb11" href="javascript:void(0);" coords="578,284,608,307" shape="rect">
                    <area data-key='G37' target="" alt="campb12" title="campb12" href="javascript:void(0);" coords="553,299,581,320" shape="rect">
                    <area data-key='G38' target="" alt="campb13" title="campb13" href="javascript:void(0);" coords="527,302,550,323" shape="rect">
                    <area data-key='G39' target="" alt="campb14" title="campb14" href="javascript:void(0);" coords="499,295,523,319" shape="rect">
                    <area data-key='G40' target="" alt="campb15" title="campb15" href="javascript:void(0);" coords="468,282,500,307" shape="rect">
                    <area data-key='G41' target="" alt="campb16" title="campb16" href="javascript:void(0);" coords="356,273,385,292" shape="rect">
                    <area data-key='G42' target="" alt="campb17" title="campb17" href="javascript:void(0);" coords="326,266,352,291" shape="rect">
                    <area data-key='G43' target="" alt="campb18" title="campb18" href="javascript:void(0);" coords="297,258,324,279" shape="rect">
                    <area data-key='G44' target="" alt="campb19" title="campb19" href="javascript:void(0);" coords="274,238,300,261" shape="rect">
                    <area data-key='G45' target="" alt="campb20" title="campb20" href="javascript:void(0);" coords="272,211,290,238" shape="rect">
                    <area data-key='G46' target="" alt="campc1" title="campc1" href="javascript:void(0);" coords="239,301,266,323" shape="rect">
                    <area data-key='G47' target="" alt="campc2" title="campc2" href="javascript:void(0);" coords="264,293,293,312" shape="rect">
                    <area data-key='G48' target="" alt="campc3" title="campc3" href="javascript:void(0);" coords="296,290,322,310" shape="rect">
                    <area data-key='G49' target="" alt="campc4" title="campc4" href="javascript:void(0);" coords="323,294,348,317" shape="rect">
                    <area data-key='G50' target="" alt="campc5" title="campc5" href="javascript:void(0);" coords="348,304,375,330" shape="rect">
                    <area data-key='G51' target="" alt="campc6" title="campc6" href="javascript:void(0);" coords="225,333,245,359" shape="rect">
                    <area data-key='G52' target="" alt="campc7" title="campc7" href="javascript:void(0);" coords="231,363,258,383" shape="rect">
                    <area data-key='G53' target="" alt="campc8" title="campc8" href="javascript:void(0);" coords="256,378,283,404" shape="rect">
                    <area data-key='G54' target="" alt="campc9" title="campc9" href="javascript:void(0);" coords="282,390,308,410" shape="rect">
                    <area data-key='G55' target="" alt="campc10" title="campc10" href="javascript:void(0);" coords="310,394,336,414" shape="rect">
                    <area data-key='G56' target="" alt="campc11" title="campc11" href="javascript:void(0);" coords="337,398,366,420" shape="rect">
                    <area data-key='G57' target="" alt="campv1" title="campv1" href="javascript:void(0);" coords="453,318,493,338" shape="rect">
                    <area data-key='G58' target="" alt="campv2" title="campv2" href="javascript:void(0);" coords="496,325,534,347" shape="rect">
                    <area data-key='G59' target="" alt="campv3" title="campv3" href="javascript:void(0);" coords="533,337,561,368" shape="rect">
                    <area data-key='G60' target="" alt="campv4" title="campv4" href="javascript:void(0);" coords="549,367,572,396" shape="rect">
                    <area data-key='G61' target="" alt="campv5" title="campv5" href="javascript:void(0);" coords="532,403,559,423" shape="rect">
                    <area data-key='G62' target="" alt="campv6" title="campv6" href="javascript:void(0);" coords="505,417,536,437" shape="rect">
                    <area data-key='G63' target="" alt="campv7" title="campv7" href="javascript:void(0);" coords="478,418,503,441" shape="rect">
                    <area data-key='G64' target="" alt="campv8" title="campv8" href="javascript:void(0);" coords="449,414,476,438" shape="rect">
                    <area data-key='G65' target="" alt="campv9" title="campv9" href="javascript:void(0);" coords="423,412,447,434" shape="rect">
                    <area data-key='G66' target="" alt="campv10" title="campv10" href="javascript:void(0);" coords="393,407,422,428" shape="rect">
                    <area data-key='G67' target="" alt="campd1" title="campd1" href="javascript:void(0);" coords="273,536,307,559" shape="rect">
                    <area data-key='G68' target="" alt="campd2" title="campd2" href="javascript:void(0);" coords="240,530,268,553" shape="rect">
                    <area data-key='G69' target="" alt="campd3" title="campd3" href="javascript:void(0);" coords="211,517,241,545" shape="rect">
                    <area data-key='G70' target="" alt="campd4" title="campd4" href="javascript:void(0);" coords="183,499,216,522" shape="rect">
                    <area data-key='G71' target="" alt="campd5" title="campd5" href="javascript:void(0);" coords="182,468,202,498" shape="rect">
                    <area data-key='G72' target="" alt="campd6" title="campd6" href="javascript:void(0);" coords="190,446,221,465" shape="rect">
                    <area data-key='G73' target="" alt="campd7" title="campd7" href="javascript:void(0);" coords="217,434,247,456" shape="rect">
                    <area data-key='G74' target="" alt="campd8" title="campd8" href="javascript:void(0);" coords="249,433,280,455" shape="rect">
                    <area data-key='G75' target="" alt="campd9" title="campd9" href="javascript:void(0);" coords="280,436,309,456" shape="rect">
                    <area data-key='G76' target="" alt="campd10" title="campd10" href="javascript:void(0);" coords="310,440,336,462" shape="rect">
                    <area data-key='G77' target="" alt="campd16" title="campd16" href="javascript:void(0);" coords="390,453,416,474" shape="rect">
                    <area data-key='G78' target="" alt="campd17" title="campd17" href="javascript:void(0);" coords="418,457,439,477" shape="rect">
                    <area data-key='G79' target="" alt="campd18" title="campd18" href="javascript:void(0);" coords="441,462,466,481" shape="rect">
                    <area data-key='G80' target="" alt="campd19" title="campd19" href="javascript:void(0);" coords="469,465,491,487" shape="rect">
                    <area data-key='G81' target="" alt="campd20" title="campd20" href="javascript:void(0);" coords="494,475,513,500" shape="rect">
                    <area data-key='G82' target="" alt="campd21" title="campd21" href="javascript:void(0);" coords="512,521,530,495" shape="rect">
                    <area data-key='G83' target="" alt="campd22" title="campd22" href="javascript:void(0);" coords="498,547,528,570" shape="rect">
                    <area data-key='G84' target="" alt="campd23" title="campd23" href="javascript:void(0);" coords="471,560,500,578" shape="rect">
                    <area data-key='G85' target="" alt="campd24" title="campd24" href="javascript:void(0);" coords="444,562,469,584" shape="rect">
                    <area data-key='G86' target="" alt="campd25" title="campd25" href="javascript:void(0);" coords="420,556,443,580" shape="rect">
                    <area data-key='G87' target="" alt="campd26" title="campd26" href="javascript:void(0);" coords="381,544,413,568" shape="rect">
                    <area data-key='G88' target="" alt="campe1" title="campe1" href="javascript:void(0);" coords="212,552,239,575" shape="rect">
                    <area data-key='G89' target="" alt="campe2" title="campe2" href="javascript:void(0);" coords="238,555,270,581" shape="rect">
                    <area data-key='G90' target="" alt="campe3" title="campe3" href="javascript:void(0);" coords="268,569,302,594" shape="rect">
                    <area data-key='G91' target="" alt="campe4" title="campe4" href="javascript:void(0);" coords="376,580,403,602" shape="rect">
                    <area data-key='G92' target="" alt="campe5" title="campe5" href="javascript:void(0);" coords="412,584,437,606" shape="rect">
                    <area data-key='G93' target="" alt="campe6" title="campe6" href="javascript:void(0);" coords="438,590,464,617" shape="rect">
                    <area data-key='G94' target="" alt="campe7" title="campe7" href="javascript:void(0);" coords="460,608,483,633" shape="rect">
                    <area data-key='G95' target="" alt="campe8" title="campe8" href="javascript:void(0);" coords="470,635,492,659" shape="rect">
                    <area data-key='G96' target="" alt="campe9" title="campe9" href="javascript:void(0);" coords="454,667,477,691" shape="rect">
                    <area data-key='G97' target="" alt="campe10" title="campe10" href="javascript:void(0);" coords="427,678,453,700" shape="rect">
                    <area data-key='G98' target="" alt="campe11" title="campe11" href="javascript:void(0);" coords="402,680,426,704" shape="rect">
                    <area data-key='G99' target="" alt="campe12" title="campe12" href="javascript:void(0);" coords="374,677,396,700" shape="rect">
                    <area data-key='G100' target="" alt="campe13" title="campe13" href="javascript:void(0);" coords="334,658,364,686" shape="rect">
                    <area data-key='G101' target="" alt="campe14" title="campe14" href="javascript:void(0);" coords="236,661,273,686" shape="rect">
                    <area data-key='G102' target="" alt="campe15" title="campe15" href="javascript:void(0);" coords="206,653,233,678" shape="rect">
                    <area data-key='G103' target="" alt="campe16" title="campe16" href="javascript:void(0);" coords="173,636,208,665" shape="rect">
                    <area data-key='G104' target="" alt="campe17" title="campe17" href="javascript:void(0);" coords="171,603,195,635" shape="rect">
                    <area data-key='G105' target="" alt="campf1" title="campf1" href="javascript:void(0);" coords="90,699,109,728" shape="rect">
                    <area data-key='G106' target="" alt="campf2" title="campf2" href="javascript:void(0);" coords="101,681,130,699" shape="rect">
                    <area data-key='G107' target="" alt="campf3" title="campf3" href="javascript:void(0);" coords="133,672,164,690" shape="rect">
                    <area data-key='G108' target="" alt="campf4" title="campf4" href="javascript:void(0);" coords="166,670,200,692" shape="rect">
                    <area data-key='G109' target="" alt="campf5" title="campf5" href="javascript:void(0);" coords="199,678,234,704" shape="rect">
                    <area data-key='G110' target="" alt="campf6" title="campf6" href="javascript:void(0);" coords="234,688,263,718" shape="rect">
                    <area data-key='G111' target="" alt="campf7" title="campf7" href="javascript:void(0);" coords="325,700,354,719" shape="rect">
                    <area data-key='G112' target="" alt="campf8" title="campf8" href="javascript:void(0);" coords="368,700,393,728" shape="rect">
                    <area data-key='G113' target="" alt="campf9" title="campf9" href="javascript:void(0);" coords="391,706,419,736" shape="rect">
                    <area data-key='G114' target="" alt="campf10" title="campf10" href="javascript:void(0);" coords="418,720,443,746" shape="rect">
                    <area data-key='G115' target="" alt="campf11" title="campf11" href="javascript:void(0);" coords="414,759,449,778" shape="rect">
                    <area data-key='G116' target="" alt="campf12" title="campf12" href="javascript:void(0);" coords="386,771,419,790" shape="rect">
                    <area data-key='G117' target="" alt="campf13" title="campf13" href="javascript:void(0);" coords="357,775,387,794" shape="rect">
                    <area data-key='G118' target="" alt="campf14" title="campf14" href="javascript:void(0);" coords="326,780,356,797" shape="rect">
                    <area data-key='G119' target="" alt="campf15" title="campf15" href="javascript:void(0);" coords="297,785,326,802" shape="rect">
                    <area data-key='G120' target="" alt="campf16" title="campf16" href="javascript:void(0);" coords="266,787,296,806" shape="rect">
                    <area data-key='G121' target="" alt="campf17" title="campf17" href="javascript:void(0);" coords="197,798,228,820" shape="rect">
                    <area data-key='G122' target="" alt="campf18" title="campf18" href="javascript:void(0);" coords="169,803,197,821" shape="rect">
                    <area data-key='G123' target="" alt="campf19" title="campf19" href="javascript:void(0);" coords="143,805,168,825" shape="rect">
                    <area data-key='G124' target="" alt="campf20" title="campf20" href="javascript:void(0);" coords="113,800,145,826" shape="rect">
                    <area data-key='G125' target="" alt="campf21" title="campf21" href="javascript:void(0);" coords="88,789,111,815" shape="rect">
                    <area data-key='G126' target="" alt="campf22" title="campf22" href="javascript:void(0);" coords="67,771,97,791" shape="rect">
                    <area data-key='G127' target="" alt="campf23" title="campf23" href="javascript:void(0);" coords="74,739,97,770" shape="rect">
                    <area data-key='G128' target="" alt="campg1" title="campg1" href="javascript:void(0);" coords="28,908,54,932" shape="rect">
                    <area data-key='G129' target="" alt="campg2" title="campg2" href="javascript:void(0);" coords="43,885,69,911" shape="rect">
                    <area data-key='G130' target="" alt="campg3" title="campg3" href="javascript:void(0);" coords="65,872,94,891" shape="rect">
                    <area data-key='G131' target="" alt="campg4" title="campg4" href="javascript:void(0);" coords="95,865,117,885" shape="rect">
                    <area data-key='G132' target="" alt="campg5" title="campg5" href="javascript:void(0);" coords="118,864,145,880" shape="rect">
                    <area data-key='G133' target="" alt="campg6" title="campg6" href="javascript:void(0);" coords="145,857,171,881" shape="rect">
                    <area data-key='G134' target="" alt="campg7" title="campg7" href="javascript:void(0);" coords="172,855,196,876" shape="rect">
                    <area data-key='G135' target="" alt="campg8" title="campg8" href="javascript:void(0);" coords="197,854,221,874" shape="rect">
                    <area data-key='G136' target="" alt="campg9" title="campg9" href="javascript:void(0);" coords="248,846,279,866" shape="rect">
                    <area data-key='G137' target="" alt="campg10" title="campg10" href="javascript:void(0);" coords="282,844,313,863" shape="rect">
                    <area data-key='G138' target="" alt="campg11" title="campg11" href="javascript:void(0);" coords="315,838,347,857" shape="rect">
                    <area data-key='G139' target="" alt="campg12" title="campg12" href="javascript:void(0);" coords="350,836,384,856" shape="rect">
                    <area data-key='G140' target="" alt="campg13" title="campg13" href="javascript:void(0);" coords="386,833,420,862" shape="rect">
                    <area data-key='G141' target="" alt="campg14" title="campg14" href="javascript:void(0);" coords="420,846,450,877" shape="rect">
                    <area data-key='G142' target="" alt="campg15" title="campg15" href="javascript:void(0);" coords="426,888,453,919" shape="rect">
                    <area data-key='G143' target="" alt="campg16" title="campg16" href="javascript:void(0);" coords="399,916,429,935" shape="rect">
                    <area data-key='G144' target="" alt="campg17" title="campg17" href="javascript:void(0);" coords="363,925,396,948" shape="rect">
                    <area data-key='G145' target="" alt="campg18" title="campg18" href="javascript:void(0);" coords="332,929,364,949" shape="rect">
                    <area data-key='G146' target="" alt="campg19" title="campg19" href="javascript:void(0);" coords="300,931,329,953" shape="rect">
                    <area data-key='G147' target="" alt="campg20" title="campg20" href="javascript:void(0);" coords="213,938,245,958" shape="rect">
                    <area data-key='G148' target="" alt="campg21" title="campg21" href="javascript:void(0);" coords="183,946,215,969" shape="rect">
                    <area data-key='G149' target="" alt="campg22" title="campg22" href="javascript:void(0);" coords="153,953,185,974" shape="rect">
                    <area data-key='G150' target="" alt="campg23" title="campg23" href="javascript:void(0);" coords="125,961,151,982" shape="rect">
                    <area data-key='G151' target="" alt="campg24" title="campg24" href="javascript:void(0);" coords="92,963,125,984" shape="rect">
                    <area data-key='G152' target="" alt="campg25" title="campg25" href="javascript:void(0);" coords="64,958,91,984" shape="rect">
                    <area data-key='G153' target="" alt="campg26" title="campg26" href="javascript:void(0);" coords="32,942,63,974" shape="rect">
                    <area data-key='G154' target="" alt="campl1" title="campl1" href="javascript:void(0);" coords="69,1013,107,1048" shape="rect">
                    <area data-key='G155' target="" alt="campl2" title="campl2" href="javascript:void(0);" coords="115,991,157,1013" shape="rect">
                    <area data-key='G156' target="" alt="campl3" title="campl3" href="javascript:void(0);" coords="172,980,214,1005" shape="rect">
                    <area data-key='G157' target="" alt="campl4" title="campl4" href="javascript:void(0);" coords="228,979,265,1011" shape="rect">
                    <area data-key='G158' target="" alt="campl5" title="campl5" href="javascript:void(0);" coords="311,967,355,988" shape="rect">
                    <area data-key='G159' target="" alt="campl6" title="campl6" href="javascript:void(0);" coords="366,962,405,982" shape="rect">
                    <area data-key='G160' target="" alt="campl7" title="campl7" href="javascript:void(0);" coords="425,961,463,992" shape="rect">
                    <area data-key='G161' target="" alt="campl8" title="campl8" href="javascript:void(0);" coords="452,1016,488,1056" shape="rect">
                    <area data-key='G162' target="" alt="campl9" title="campl9" href="javascript:void(0);" coords="400,1047,443,1072" shape="rect">
                    <area data-key='G163' target="" alt="campl10" title="campl10" href="javascript:void(0);" coords="343,1056,389,1077" shape="rect">
                    <area data-key='G164' target="" alt="campl11" title="campl11" href="javascript:void(0);" coords="232,1054,268,1088" shape="rect">
                    <area data-key='G165' target="" alt="campl12" title="campl12" href="javascript:void(0);" coords="182,1079,226,1101" shape="rect">
                    <area data-key='G166' target="" alt="campl13" title="campl13" href="javascript:void(0);" coords="130,1088,165,1109" shape="rect">
                    <area data-key='G167' target="" alt="campl14" title="campl14" href="javascript:void(0);" coords="75,1069,114,1107" shape="rect">
                    <area data-key='G168' target="" alt="campl15" title="campl15" href="javascript:void(0);" coords="98,1138,140,1176" shape="rect">
                    <area data-key='G169' target="" alt="campl16" title="campl16" href="javascript:void(0);" coords="147,1118,191,1138" shape="rect">
                    <area data-key='G170' target="" alt="campl17" title="campl17" href="javascript:void(0);" coords="207,1109,243,1133" shape="rect">
                    <area data-key='G171' target="" alt="campl18" title="campl18" href="javascript:void(0);" coords="260,1104,298,1123" shape="rect">
                    <area data-key='G172' target="" alt="campl19" title="campl19" href="javascript:void(0);" coords="348,1095,384,1115" shape="rect">
                    <area data-key='G173' target="" alt="campl20" title="campl20" href="javascript:void(0);" coords="399,1088,435,1109" shape="rect">
                    <area data-key='G174' target="" alt="campl21" title="campl21" href="javascript:void(0);" coords="456,1087,494,1115" shape="rect">
                    <area data-key='G175' target="" alt="campl22" title="campl22" href="javascript:void(0);" coords="483,1135,516,1176" shape="rect">
                    <area data-key='G176' target="" alt="campl23" title="campl23" href="javascript:void(0);" coords="445,1170,482,1191" shape="rect">
                    <area data-key='G177' target="" alt="campl24" title="campl24" href="javascript:void(0);" coords="407,1174,445,1200" shape="rect">
                    <area data-key='G178' target="" alt="campl25" title="campl25" href="javascript:void(0);" coords="365,1180,403,1204" shape="rect">
                    <area data-key='G179' target="" alt="campl26" title="campl26" href="javascript:void(0);" coords="280,1187,316,1215" shape="rect">
                    <area data-key='G180' target="" alt="campl27" title="campl27" href="javascript:void(0);" coords="233,1198,272,1222" shape="rect">
                    <area data-key='G181' target="" alt="campl28" title="campl28" href="javascript:void(0);" coords="191,1207,230,1230" shape="rect">
                    <area data-key='G182' target="" alt="campl29" title="campl29" href="javascript:void(0);" coords="150,1212,187,1238" shape="rect">
                    <area data-key='G183' target="" alt="campl30" title="campl30" href="javascript:void(0);" coords="108,1194,142,1228" shape="rect">
                </map>


            </div>-->




        </div>

        <script type="text/javascript" src="javascript/redist/when.js"></script>
        <script type="text/javascript" src="javascript/core.js"></script>
        <script type="text/javascript" src="javascript/graphics.js"></script>
        <script type="text/javascript" src="javascript/mapimage.js"></script>
        <script type="text/javascript" src="javascript/mapdata.js"></script>
        <script type="text/javascript" src="javascript/areadata.js"></script>
        <script type="text/javascript" src="javascript/areacorners.js"></script>
        <script type="text/javascript" src="javascript/scale.js"></script>
        <script type="text/javascript" src="javascript/tooltip.js"></script>
        <script>
            go_to_final_choice = false;
            selected_area = 0;
            $section = $('#focal0');
            panzoom = $section.find('.panzoom0')
            function show_continue() {
                $("#continue_btn").fadeIn();
            }
            function hide_continue() {
                $("#continue_btn").fadeOut();
            }
            function show_back() {
                $("#back_btn").fadeIn();
            }
            function hide_back() {
                $("#back_btn").fadeOut();
            }
            function increase_counter() {


                if (selected_area >= 0) {
                    hide_continue();
                    show_back();
                    panzoom.panzoom("reset");


                    panzoom.html($("#temporary" + selected_area).clone());
                    panzoom.find("img").mapster({
                        stroke: true,
                        render_highlight: {
                            strokeWidth: 2
                        },

                        singleSelect: true,
                        onClick: function (data) {
                            if (data.selected) {
                                show_continue();

                            }
                            else {
                                hide_continue();
                            }
                            selected_area = data.e.currentTarget.attributes["data-key"].nodeValue;
                        }

                    });

                    panzoom.panzoom("pan", 0, 0);



                }
                else {
                    if (spotNumber != 0) {
                        document.getElementById("final_output").style.display = "block";
                        document.getElementById("map").style.display = "none";
                        document.getElementById("back_btn").style.display = "none";
                        document.getElementById("continue_btn").style.display = "none";
                        document.getElementById("pickYourSpot").innerHTML = "";
                        document.getElementById("campig_spot_number").innerHTML = areaNumber + ", " + spotNumber;
                        document.getElementById("finishedCampingCheckbox").checked = true;
                        document.getElementById("finishedCampingCheckbox").value = areaNumber + "." + spotNumber;
                        document.getElementById("map_enabler").disabled = true;
                        document.getElementById("map_container").style.height = "415px";
                        document.getElementById("campingSubmitButton").style.display = "block";
                    }
                }
            }


            var starting_point;

            function decrease_counter() {

                $('#<%=mapValidation.ClientID%>').html("");
                panzoom.panzoom("reset");
                spotNumber = 0;
                hide_continue();
                hide_back();
                panzoom.html(starting_point);


                panzoom.find("img").mapster({
                    stroke: true,
                    render_highlight: {
                        strokeWidth: 2
                    },
                    singleSelect: true,
                    onClick: function (data) {
                        if (data.selected) {
                            show_continue();

                        }
                        else {
                            hide_continue();
                        }
                        selected_area = data.e.currentTarget.attributes["data-key"].nodeValue;
                    }

                });


                panzoom.panzoom("pan", 0, 0);

            }
            (function () {



                panzoom.panzoom({
                    startTransform: 'scale(0.8)',
                    $reset: $("#reset"),

                });


                panzoom.parent().on('mousewheel.focal', function (e) {
                    e.preventDefault();
                    var delta = e.delta || e.originalEvent.wheelDelta;
                    var zoomOut = delta ? delta < 0 : e.originalEvent.deltaY > 0;
                    panzoom.panzoom('zoom', zoomOut, {
                        animate: false,
                        focal: e
                    });
                });



                instantiate_mapster();



                starting_point = panzoom.clone();

            })();



            function instantiate_mapster() {
                $("#myimage0").mapster({
                    stroke: true,
                    render_highlight: {
                        strokeWidth: 2
                    },
                    singleSelect: true,
                    onClick: function (data) {
                        if (data.selected) {
                            show_continue();

                        }
                        else {
                            hide_continue();
                        }
                        selected_area = data.e.currentTarget.attributes["data-key"].nodeValue;
                    }

                });
                /*
                for (var i = 0; i < 11; i++) {
                    console.log($("#myimage" + i));


                    $("#myimage" + i).mapster({
                        stroke: true,
                        render_highlight: {
                            strokeWidth: 2
                        },
                        singleSelect: true,
                        onClick: function (data) {
                            console.log(data.e.currentTarget.title);
                            selected_area = data.e.currentTarget.title;
                        }

                    });
                }
                */
            }


        </script>


    </div></div>

                                      </div>
                                    </div>
                                  </div>
                                </div>
                                 <!-- Change Ticket Modal -->
                                <div class="modal fade" id="changeTicketModal" tabindex="-1" role="dialog" aria-labelledby="changeTicketModelLabel" aria-hidden="true">
                                  <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                      <div class="modal-header">
                                        <h5 class="modal-title" id="changeTicketModelLabel">Extend Ticket</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                          <span aria-hidden="true">&times;</span>
                                        </button>
                                      </div>
                                      <div class="modal-body">
                                            <input runat="server" autocomplete="off" type="text" class="form-control" id="tbTicketDate" name="tbTicketDate" placeholder="" oninvalid="this.setCustomValidity('Please provide ticket dates!')" oninput="setCustomValidity('')" onchange="setCustomValidity()" />
                                            <div class="col-12" id="ticketTypeValidation" runat="server" style="color: red;"></div>
                                            <button type="submit" onserverclick="btnExtendTicket_ServerClick" onclick="CallCheckAvailability();" class="btn btn-lg btn-primary btn-block mt-3" runat="server">Submit</button>
                                      </div>
                                      <div class="modal-footer">
                                        <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
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
    </header>

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
                                    <input maxLength="64" runat="server" class="w-75" type="email" id="email1" name="email" placeholder="your@email.com">
                                </div>
                                <asp:Button runat="server" Text="Subscribe Now!" class="btn btn-lg btn-primary" OnClientClick="MakeFormSubmittable();" OnClick="Subscribe_Click" />
                            
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
            function CallCheckAvailability() {
                document.getElementById("ticketTypeValidation").innerHTML = "";
                if (!CheckAvailability(document.getElementById("tbTicketDate").value)) {
                    return;
                }
                else {
                    MakeFormSubmittable();
                }
            }
    </script>
            <script>
                function MakeFormSubmittable() {
                    document.getElementById("regForm").onsubmit = "";
                }
                </script>
            <script type="text/javascript">
                function CheckAvailability(ticketDates) {
                    var success = false;
                    var obj = { ticketDates: ticketDates };
                    var pageUrl = '<%=ResolveUrl("~/CheckAvailability.asmx")%>'
                    $.ajax({
                        async: false,
                        type: "GET",
                        url: pageUrl + "/AvailableTickets",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(obj),
                        success: OnSuccessCall,
                        error: OnErrorCall
                    });
                    function OnSuccessCall(response) {
                        var res = ticketDates.split(",");
                        if ((res.includes("01/07/2018") && response.d.includes("01/07/2018")) ||
                            (res.includes("02/07/2018") && response.d.includes("02/07/2018")) ||
                            (res.includes("03/07/2018") && response.d.includes("03/07/2018"))) {
                            $('#<%=ticketTypeValidation.ClientID%>').html(response.d);
                            success = false;
                        }
                        else if (response == "" || response == null || ticketDates == "" || ticketDates == null) {
                            success = false;
                        }
                        else {
                            success = true;
                        }
                    }
                    function OnErrorCall(response) {
                        success = false;
                    }
                    return success;
                }
    </script>

    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
    <script>
        var condition = document.getElementById("ticket").innerHTML;
        if (!condition.includes("01/07/2018") && !condition.includes("02/07/2018") && condition.includes("03/07/2018")) {
            $('#tbTicketDate').datepicker({
                format: "dd/mm/yyyy",
                startDate: "01/07/2018",
                endDate: "02/07/2018",
                multidate: true
            });
        } else if (!condition.includes("01/07/2018") && condition.includes("02/07/2018") && !condition.includes("03/07/2018")) {
            $('#tbTicketDate').datepicker({
                format: "dd/mm/yyyy",
                startDate: "01/07/2018",
                endDate: "03/07/2018",
                daysOfWeekDisabled: "6",
                multidate: true
            });
        } else if (!condition.includes("01/07/2018") && condition.includes("02/07/2018") && condition.includes("03/07/2018")) {
            $('#tbTicketDate').datepicker({
                format: "dd/mm/yyyy",
                startDate: "01/07/2018",
                endDate: "01/07/2018",
                multidate: true
            });
        } else if (condition.includes("01/07/2018") && !condition.includes("02/07/2018") && !condition.includes("03/07/2018")) {
            $('#tbTicketDate').datepicker({
                format: "dd/mm/yyyy",
                startDate: "02/07/2018",
                endDate: "03/07/2018",
                multidate: true
            });
        } else if (condition.includes("01/07/2018") && !condition.includes("02/07/2018") && condition.includes("03/07/2018")) {
            $('#tbTicketDate').datepicker({
                format: "dd/mm/yyyy",
                startDate: "02/07/2018",
                endDate: "02/07/2018",
                multidate: true
            });
        } else if (condition.includes("01/07/2018") && condition.includes("02/07/2018") && !condition.includes("03/07/2018")) {
            $('#tbTicketDate').datepicker({
                format: "dd/mm/yyyy",
                startDate: "03/07/2018",
                endDate: "03/07/2018",
                multidate: true
            });
        }
    </script>

    <!-- Bootstrap core JavaScript -->
    <script src="/jquery/jquery.min.js"></script>
    <script src="/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />

</body>
</html>

